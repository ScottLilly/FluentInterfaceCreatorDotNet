using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using FluentInterfaceCreator.Core;

namespace FluentInterfaceCreator.Models.Inputs;

public class Project : INotifyPropertyChanged
{
    public string Name { get; set; } = "";
    public OutputLanguage? OutputLanguage { get; set; }
    public string NamespaceForFactoryClass { get; set; } = "";
    public string FactoryClassName { get; set; } = "";
    public ObservableCollection<DataType> DataTypes { get; } = new();
    public ObservableCollection<Method> Methods { get; } = new();
    public ObservableCollection<MethodLink> MethodLinks { get; } = new();
    public ObservableCollection<InterfaceSpec> InterfaceSpecs { get; } = new();

    public bool CanCreateOutputFiles =>
        Name.IsNotEmpty() &&
        NamespaceForFactoryClass.IsNotEmpty() &&
        FactoryClassName.IsNotEmpty() &&
        InstantiatingMethods.Any() &&
        ChainingMethods.Any() &&
        ExecutingMethods.Any() &&
        InstantiatingMethods.All(im => 
            MethodLinks.Any(ml => ml.StartingMethodId == im.Id)) &&
        ChainingMethods.All(cm => 
            MethodLinks.Any(ml => ml.StartingMethodId == cm.Id)) &&
        ChainingMethods.All(cm => 
            MethodLinks.Any(ml => ml.EndingMethodId == cm.Id)) &&
        ExecutingMethods.All(cm => 
            MethodLinks.Any(ml => ml.EndingMethodId == cm.Id));

    private IEnumerable<Method> InstantiatingMethods =>
        Methods.Where(m => m.Type == Enums.MethodType.Instantiating);

    private IEnumerable<Method> ChainingMethods =>
        Methods.Where(m => m.Type == Enums.MethodType.Chaining);

    private IEnumerable<Method> ExecutingMethods =>
        Methods.Where(m => m.Type == Enums.MethodType.Executing);

    public event PropertyChangedEventHandler? PropertyChanged;

    public Project()
    {
        // Connect internal handlers, to refresh computed values (when needed)
        PropertyChanged += OnPropertyChanged;
        Methods.CollectionChanged += OnMethodsCollectionChanged;
        MethodLinks.CollectionChanged += OnMethodLinksCollectionChanged;
    }

    #region Public methods

    #endregion

    #region Private methods

    private void OnPropertyChanged(object? sender, 
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OutputLanguage))
        {
            PopulateNativeDataTypes();
        }
    }

    private void OnMethodsCollectionChanged(object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        // When a Method is added, create an InterfaceSpec object
        //if (e.Action == NotifyCollectionChangedAction.Add &&
        //    e.NewItems != null)
        //{
        //    foreach (var item in e.NewItems)
        //    {
        //        if (item is Method method)
        //        {
        //        }
        //    }
        //}

        // When a Method is removed from the Methods property,
        // remove the MethodLink objects that referenced it
        if (e.Action == NotifyCollectionChangedAction.Remove &&
            e.OldItems != null)
        {
            foreach (var item in e.OldItems)
            {
                if (item is Method method)
                {
                    var methodLinksToRemove =
                        MethodLinks.Where(ml =>
                            ml.StartingMethodId == method.Id ||
                            ml.EndingMethodId == method.Id);

                    foreach (MethodLink methodLink in methodLinksToRemove)
                    {
                        MethodLinks.Remove(methodLink);
                    }
                }
            }
        }

        RefreshInterfaceSpecs();
    }

    private void OnMethodLinksCollectionChanged(object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        RefreshInterfaceSpecs();
    }

    private void RefreshInterfaceSpecs()
    {
        InterfaceSpecs.Clear();
        // Remove references to deleted Methods
        //foreach (var interfaceSpec in InterfaceSpecs)
        //{
        //    // Remove missing methods from InterfaceSpecs
        //    interfaceSpec.CalledByMethodId
        //        .RemoveAll(cbm => Methods.All(m => m.Id != cbm));
        //    interfaceSpec.CallsIntoMethodIds
        //        .RemoveAll(cim => Methods.All(m => m.Id != cim));

        //    // Remove InterfaceSpecs with empty calledBy or callsInto values
        //    var interfaceSpecsToRemove =
        //        InterfaceSpecs.Where(i => !i.CalledByMethodId.Any() ||
        //                                  !i.CallsIntoMethodIds.Any());

        //    foreach (InterfaceSpec spec in interfaceSpecsToRemove)
        //    {
        //        InterfaceSpecs.Remove(spec);
        //    }
        //}

        // Add InterfaceSpecs that are missing
        Dictionary<Guid, List<Guid>> chainedMethods =
            new Dictionary<Guid, List<Guid>>();

        var distinctCallingMethods =
            MethodLinks.Select(ml => ml.StartingMethodId).Distinct();

        foreach (Guid callingMethod in distinctCallingMethods)
        {
            var calledMethods =
                MethodLinks.Where(ml => ml.StartingMethodId == callingMethod)
                    .Select(ml => ml.EndingMethodId).ToList();

            chainedMethods.Add(callingMethod, calledMethods);
        }

        foreach (var chainedMethod in chainedMethods)
        {
            InterfaceSpec? matchingInterfaceSpec =
                InterfaceSpecs.FirstOrDefault(i => 
                    i.CallsIntoMethodIds.SequenceEqual(chainedMethod.Value));

            if (matchingInterfaceSpec == null)
            {
                InterfaceSpecs.Add(new InterfaceSpec
                {
                    CalledByMethodId = new List<Guid> { chainedMethod.Key },
                    CallsIntoMethodIds = chainedMethod.Value
                });
            }
            else
            {
                if (matchingInterfaceSpec.CalledByMethodId
                    .All(cbm => cbm != chainedMethod.Key))
                {
                    matchingInterfaceSpec.CalledByMethodId.Add(chainedMethod.Key);
                }
            }
        }
    }

    private void PopulateNativeDataTypes()
    {
        DataTypes.Clear();

        if (OutputLanguage != null)
        {
            foreach (DataType dataType in OutputLanguage.NativeDataTypes)
            {
                DataTypes.Add(dataType);
            }
        }
    }

    #endregion
}