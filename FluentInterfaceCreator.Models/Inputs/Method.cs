﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using FluentInterfaceCreator.Core;
using PropertyChanged;

namespace FluentInterfaceCreator.Models.Inputs;

[SuppressPropertyChangedWarnings]
public class Method : INotifyPropertyChanged, ITrackChanges
{
    private MethodMemento _memento;

    public Guid Id { get; set; } = Guid.NewGuid();
    public Enums.MethodType Type { get; set; }
    public string Name { get; set; } = "";
    // Only used for Executing methods
    public bool UseIEnumerable { get; set; }
    public DataType? ReturnDataType { get; set; }

    public ObservableCollection<Parameter> Parameters { get; } = new();

    public bool IsValid =>
        Name.IsNotEmpty() &&
        !Name.ContainsInvalidCharacter() &&
        !Name.HasAnInternalSpace() &&
        Parameters.All(p => p.IsValid) &&
        ((Type == Enums.MethodType.Executing && ReturnDataType != null) || 
         Type != Enums.MethodType.Executing);

    public bool CanStartChainPair =>
        Type is Enums.MethodType.Instantiating or Enums.MethodType.Chaining;
    public bool CanEndChainPair =>
        Type is Enums.MethodType.Chaining or Enums.MethodType.Executing;
    public bool RequiresReturnDataType =>
        IsExecutingMethod;
    public bool IsExecutingMethod =>
        Type == Enums.MethodType.Executing;

    public IEnumerable<string> RequiredNamespaces =>
        Parameters.SelectMany(p => p.RequiredNamespaces)
            .Concat(new List<string>
            {
                ReturnDataType?.ContainingNamespace ?? "",
                UseIEnumerable ? "System.Collections.Generic" : ""
            })
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Distinct();

    public string DataTypeOnlySignature =>
        FormattedReturnDataType + "|" + 
        Name + "=" + 
        string.Join(":", Parameters.Select(p => p.FormattedDataType));
    public string FormattedReturnDataType =>
        Type == Enums.MethodType.Executing
            ? UseIEnumerable
                ? $"IEnumerable<{ReturnDataType?.Name}>" 
                : ReturnDataType?.Name ?? ""
            : "";

    public string FullSignature =>
        $"{Name}({string.Join(", ", Parameters.Select(p => p.FormattedDataTypeAndName))})";

    public bool IsDirty =>
        Name != _memento.Name ||
        Type != _memento.Type ||
        UseIEnumerable != _memento.UseIEnumerable ||
        ReturnDataType != _memento.ReturnDataType ||
        Parameters.Any(p => p.IsDirty);

    public event PropertyChangedEventHandler? PropertyChanged;

    public Method(string name, Enums.MethodType type)
    {
        Name = name;
        Type = type;

        SetMementoToCurrentValues();
    }

    // TODO: Find better solution
    // Parameterless constructor for editing ViewModel and deserialization
    public Method()
    {
        SetMementoToCurrentValues();
    }

    // TODO: Find better solution
    // Should only be used for Executing functions
    public Method(string name, Enums.MethodType type, DataType? returnDataType)
    {
        Name = name;
        Type = type;
        ReturnDataType = returnDataType;

        SetMementoToCurrentValues();
    }

    public void MarkAsClean()
    {
        SetMementoToCurrentValues();

        foreach (Parameter parameter in Parameters)
        {
            parameter.MarkAsClean();
        }
    }

    public bool Matches(Method method, bool isCaseSensitive = true) =>
        Name.Matches(method.Name, isCaseSensitive) &&
        FormattedReturnDataType.Matches(method.FormattedReturnDataType, isCaseSensitive) &&
        Parameters.Select(p => p.FormattedDataType)
            .SequenceEqual(method.Parameters.Select(p => p.FormattedDataType));

    private void SetMementoToCurrentValues()
    {
        _memento =
            new MethodMemento(Type, Name, UseIEnumerable, ReturnDataType);
    }

    private class MethodMemento
    {
        public Enums.MethodType Type { get; set; }
        public string Name { get; set; } = "";
        // Only used for Executing methods
        public bool UseIEnumerable { get; set; }
        public DataType? ReturnDataType { get; set; }

        internal MethodMemento(Enums.MethodType type, string name, 
            bool useIEnumerable, DataType? returnDataType)
        {
            Type = type;
            Name = name;
            UseIEnumerable = useIEnumerable;
            ReturnDataType = returnDataType;
        }
    }

}