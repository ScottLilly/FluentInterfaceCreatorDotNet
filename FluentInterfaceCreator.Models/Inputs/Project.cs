using System.Collections.ObjectModel;
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


    public bool CanCreateOutputFiles =>
        Name.IsNotEmpty() &&
        NamespaceForFactoryClass.IsNotEmpty() &&
        FactoryClassName.IsNotEmpty();

    public event PropertyChangedEventHandler? PropertyChanged;

    public Project()
    {
        PropertyChanged += OnPropertyChanged;
    }

    #region Public methods

    #endregion

    #region Private methods

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OutputLanguage))
        {
            PopulateNativeDataTypes();
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