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
    public List<MethodLink> MethodsLink { get; } = new();

    public bool CanCreateOutputFiles =>
        Name.IsNotEmpty() &&
        NamespaceForFactoryClass.IsNotEmpty() &&
        FactoryClassName.IsNotEmpty() &&
        InstantiatingMethods.Any() &&
        ChainingMethods.Any() &&
        ExecutingMethods.Any();

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
        if (e.Action != NotifyCollectionChangedAction.Remove ||
            e.OldItems == null)
        {
            return;
        }

        // When a Method is removed from the Methods property,
        // remove the MethodLink objects that referenced it
        foreach (var item in e.OldItems)
        {
            if (item is Method method)
            {
                MethodsLink.RemoveAll(ml =>
                    ml.StartingMethodId == method.Id ||
                    ml.EndingMethodId == method.Id);
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