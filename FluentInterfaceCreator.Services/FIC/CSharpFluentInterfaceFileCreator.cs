using FluentInterfaceCreator.Models;
using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Models.Outputs;

namespace FluentInterfaceCreator.Services.FIC;

internal sealed class CSharpFluentInterfaceFileCreator : 
    IFluentInterfaceCreator
{
    private readonly Project _project;

    public CSharpFluentInterfaceFileCreator(Project project)
    {
        _project = project;
    }

    public FluentInterfaceFile CreateFluentInterfaceFile()
    {
        FluentInterfaceFile builder =
            new FluentInterfaceFile($"{_project.FactoryClassName}.{_project.OutputLanguage.FileExtension}");

        AddRequiredUsingStatements(builder, _project.NamespacesNeeded);

        builder.AddLine(0, $"namespace {_project.NamespaceForFactoryClass}");
        builder.AddLine(0, "{");

        builder.AddLine(1, $"public class {_project.FactoryClassName} : {_project.ListOfInterfaces}");
        builder.AddLine(1, "{");

        AddInstantiatingFunctions(builder);
        AddChainingFunctions(builder);
        AddExecutingFunctions(builder);

        AddHideIntelliSense(builder);

        // Close class
        builder.AddLine(1, "}");

        // Append interfaces
        builder.AddLineAfterBlankLine(1, "// Interfaces");

        foreach (FluentInterfaceFile interfaceFile in CreateInterfaceFiles())
        {
            builder.AddLineAfterBlankLine(0, interfaceFile.FormattedText());
        }

        // Close namespace
        builder.AddLine(0, "}");

        return builder;
    }

    private List<FluentInterfaceFile> CreateInterfaceFiles()
    {
        List<FluentInterfaceFile> interfaces = new List<FluentInterfaceFile>();

        foreach (var interfaceSpec in _project.InterfaceSpecs)
        {
            FluentInterfaceFile builder =
                new FluentInterfaceFile($"{interfaceSpec.Name}.{_project.OutputLanguage.FileExtension}");

            builder.AddLine(1, $"public interface {interfaceSpec.Name}");

            builder.AddLine(1, "{");

            foreach (Method method in
                     _project.Methods
                         .Where(m => m.CanEndChainPair &&
                                     interfaceSpec.CallsIntoMethodIds.Contains(m.Id)))
            {
                if (method.Type == Enums.MethodType.Executing)
                {
                    builder.AddLine(2, $"{method.FormattedReturnDataType} {method.FullSignature};");
                }
                else
                {
                    var returnInterfaceSpec =
                        _project.InterfaceSpecs.First(i => i.CalledByMethodId.Contains(method.Id));

                    builder.AddLine(2, $"{returnInterfaceSpec.Name} {method.FullSignature};");
                }
            }

            builder.AddLine(1, "}");

            interfaces.Add(builder);
        }

        return interfaces;
    }

    private void AddInstantiatingFunctions(FluentInterfaceFile builder)
    {
        builder.AddLine(2, "// Instantiating functions");

        foreach (Method method in _project.InstantiatingMethods)
        {
            var interfaceSpec =
                _project.InterfaceSpecs.First(i => i.CalledByMethodId.Contains(method.Id));

            builder.AddLineAfterBlankLine(2, 
                $"public static {interfaceSpec.Name} {method.FullSignature}");
            builder.AddLine(2, "{");
            builder.AddLine(3, $"return new {_project.FactoryClassName}();");
            builder.AddLine(2, "}");
        }
    }

    private void AddChainingFunctions(FluentInterfaceFile builder)
    {
        builder.AddLineAfterBlankLine(2, "// Chaining functions");

        foreach (Method method in _project.ChainingMethods)
        {
            var interfaceSpec =
                _project.InterfaceSpecs.First(i => i.CalledByMethodId.Contains(method.Id));

            builder.AddLineAfterBlankLine(2,
                $"public {interfaceSpec.Name} {method.FullSignature}");
            builder.AddLine(2, "{");
            builder.AddLine(3, "return this;");
            builder.AddLine(2, "}");
        }
    }

    private void AddExecutingFunctions(FluentInterfaceFile builder)
    {
        builder.AddLineAfterBlankLine(2, "// Executing functions");

        foreach (Method method in _project.ExecutingMethods)
        {
            builder.AddLineAfterBlankLine(2, 
                $"public {method.FormattedReturnDataType} {method.FullSignature}");
            builder.AddLine(2, "{");
            builder.AddLine(2, "}");
        }
    }

    private static void AddRequiredUsingStatements(FluentInterfaceFile builder, List<string> namespaces)
    {
        builder.AddLine(0, "using System.ComponentModel;");

        foreach (string ns in namespaces.Distinct().OrderBy(n => n))
        {
            builder.AddLine(0, $"using {ns};");
        }

        if (namespaces.Any())
        {
            builder.AddBlankLine();
        }
    }

    private static void AddHideIntelliSense(FluentInterfaceFile builder)
    {
        builder.AddLine(2, "");
        builder.AddLine(2, "// Hide default functions from appearing with IntelliSense");
        builder.AddLine(2, "");
        builder.AddLine(2, "[EditorBrowsable(EditorBrowsableState.Never)]");
        builder.AddLine(2, "public override bool Equals(object obj)");
        builder.AddLine(2, "{");
        builder.AddLine(3, "return base.Equals(obj);");
        builder.AddLine(2, "}");
        builder.AddLine(2, "");
        builder.AddLine(2, "[EditorBrowsable(EditorBrowsableState.Never)]");
        builder.AddLine(2, "public override int GetHashCode()");
        builder.AddLine(2, "{");
        builder.AddLine(3, "return base.GetHashCode();");
        builder.AddLine(2, "}");
        builder.AddLine(2, "");
        builder.AddLine(2, "[EditorBrowsable(EditorBrowsableState.Never)]");
        builder.AddLine(2, "public override string ToString()");
        builder.AddLine(2, "{");
        builder.AddLine(3, "return base.ToString();");
        builder.AddLine(2, "}");
    }

}