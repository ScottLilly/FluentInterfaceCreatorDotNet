using FluentInterfaceCreator.Models;
using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Services;

namespace Test.FluentInterfaceCreator.Examples;

public class TestFluentEmailer : BaseTestClass
{
    [Fact]
    public void Test_CreateFluentEmailer()
    {
        Project project = new Project();
        project.OutputLanguage = GetOutputLanguage();
        project.Name = "FluentEmailer";
        project.NamespaceForFactoryClass = "FluentEmailer";
        project.FactoryClassName = "FluentEmailCreator";

        // Add non-standard DataTypes
        DataType mailMessageDataType = new DataType
        {
            ContainingNamespace = "System.Net.Mail",
            Name = "MailMessage",
            IsNative = false
        };
        project.DataTypes.Add(mailMessageDataType);

        DataType mailAddressDataType = new DataType
        {
            ContainingNamespace = "System.Net.Mail",
            Name = "MailAddress",
            IsNative = false
        };
        project.DataTypes.Add(mailAddressDataType);

        // Add Methods
        // Create
        Method createMethod = new Method();
        createMethod.Name = "Create";
        createMethod.Type = Enums.MethodType.Instantiating;

        project.Methods.Add(createMethod);

        // From
        Method fromAddressStringMethod = new Method();
        fromAddressStringMethod.Name = "From";
        fromAddressStringMethod.Type = Enums.MethodType.Chaining;
        fromAddressStringMethod.Parameters.Add(new Parameter
        {
            DataType = GetDataTypeWithName("string"),
            Name = "emailAddress"
        });

        project.Methods.Add(fromAddressStringMethod);

        Method fromAddressMailAddressMethod = new Method();
        fromAddressMailAddressMethod.Name = "From";
        fromAddressMailAddressMethod.Type = Enums.MethodType.Chaining;
        fromAddressMailAddressMethod.Parameters.Add(new Parameter
        {
            DataType = mailAddressDataType,
            Name = "emailAddress"
        });

        project.Methods.Add(fromAddressMailAddressMethod);

        // To
        Method toAddressStringMethod = new Method();
        toAddressStringMethod.Name = "To";
        toAddressStringMethod.Type = Enums.MethodType.Chaining;
        toAddressStringMethod.Parameters.Add(new Parameter
        {
            DataType = GetDataTypeWithName("string"),
            Name = "emailAddress"
        });

        project.Methods.Add(toAddressStringMethod);

        Method toAddressMailAddressMethod = new Method();
        toAddressMailAddressMethod.Name = "To";
        toAddressMailAddressMethod.Type = Enums.MethodType.Chaining;
        toAddressMailAddressMethod.Parameters.Add(new Parameter
        {
            DataType = mailAddressDataType,
            Name = "emailAddress"
        });

        project.Methods.Add(toAddressMailAddressMethod);

        // Build
        Method buildMethod = new Method();
        buildMethod.Name = "Build";
        buildMethod.Type = Enums.MethodType.Executing;
        buildMethod.ReturnDataType = mailMessageDataType;

        project.Methods.Add(buildMethod);

        // Chains
        // Calls to From
        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = createMethod.Id,
            EndingMethodId = fromAddressStringMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = createMethod.Id,
            EndingMethodId = fromAddressMailAddressMethod.Id
        });

        // Calls to To
        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = fromAddressStringMethod.Id,
            EndingMethodId = toAddressStringMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = fromAddressStringMethod.Id,
            EndingMethodId = toAddressMailAddressMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = fromAddressMailAddressMethod.Id,
            EndingMethodId = toAddressStringMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = fromAddressMailAddressMethod.Id,
            EndingMethodId = toAddressMailAddressMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = toAddressStringMethod.Id,
            EndingMethodId = toAddressStringMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = toAddressStringMethod.Id,
            EndingMethodId = toAddressMailAddressMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = toAddressMailAddressMethod.Id,
            EndingMethodId = toAddressStringMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = toAddressMailAddressMethod.Id,
            EndingMethodId = toAddressMailAddressMethod.Id
        });

        // Calls to Build
        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = toAddressStringMethod.Id,
            EndingMethodId = buildMethod.Id
        });

        project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = toAddressMailAddressMethod.Id,
            EndingMethodId = buildMethod.Id
        });

        Assert.True(project.CanCreateOutputFiles);

        // Populate interfaceSpec object names

        var iMustAddFromAddress =
            project.InterfaceSpecs.First(i =>
                i.CalledByMethodId.Count == 1 &&
                i.CallsIntoMethodIds.Count == 2 &&
                i.CalledByMethodId.Contains(createMethod.Id) &&
                i.CallsIntoMethodIds.Contains(fromAddressStringMethod.Id) &&
                i.CallsIntoMethodIds.Contains(fromAddressMailAddressMethod.Id) &&
                i.CallsIntoMethodIds.Contains(fromAddressMailAddressMethod.Id));
        iMustAddFromAddress.Name = "IMustAddFromAddress";

        var iCanAddToAddress =
            project.InterfaceSpecs.First(i =>
                i.CalledByMethodId.Count == 2 &&
                i.CallsIntoMethodIds.Count == 2 &&
                i.CalledByMethodId.Contains(fromAddressStringMethod.Id) &&
                i.CalledByMethodId.Contains(fromAddressMailAddressMethod.Id) &&
                i.CallsIntoMethodIds.Contains(toAddressStringMethod.Id) &&
                i.CallsIntoMethodIds.Contains(toAddressMailAddressMethod.Id));
        iCanAddToAddress.Name = "IMustAddToAddress";

        var iCanAddToAddressOrBuild =
            project.InterfaceSpecs.First(i =>
                i.CalledByMethodId.Count == 2 &&
                i.CallsIntoMethodIds.Count == 3 &&
                i.CalledByMethodId.Contains(toAddressStringMethod.Id) &&
                i.CalledByMethodId.Contains(toAddressMailAddressMethod.Id) &&
                i.CallsIntoMethodIds.Contains(toAddressStringMethod.Id) &&
                i.CallsIntoMethodIds.Contains(toAddressMailAddressMethod.Id) &&
                i.CallsIntoMethodIds.Contains(buildMethod.Id));
        iCanAddToAddressOrBuild.Name = "ICanAddToAddressOrBuild";

        var fluentInterfaceFileCreator = 
            FluentInterfaceCreatorFactory.GetFluentInterfaceFileCreator(project);

        Assert.NotNull(fluentInterfaceFileCreator);

        //File.WriteAllText(
        //    Path.Combine(@"e:\temp\output", 
        //        $"{project.FactoryClassName}.{project.OutputLanguage.FileExtension}"),
        //    fluentInterfaceFileCreator.CreateInSingleFile().FormattedText());

        //PersistenceService.SaveProjectToDisk(project,
        //    @"E:\temp\output\project.json");
    }
}