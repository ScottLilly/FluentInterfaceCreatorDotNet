using FluentInterfaceCreator.Models;
using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Services;

namespace Test.FluentInterfaceCreator.Examples;

public class TestFluentEmailer : BaseTestClass
{
    [Fact]
    public void Test_CreateFluentEmailer()
    {
        #region Project setup

        Project project = new Project();
        project.OutputLanguage = GetOutputLanguage();
        project.Name = "FluentMailMessage";
        project.NamespaceForFactoryClass = "FluentMailMessage";
        project.FactoryClassName = "FluentMailMessageCreator";

        #endregion

        #region Add DataTypes

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

        #endregion

        Parameter parameterEmailAddressString = 
            new Parameter
        {
            DataType = GetDataTypeWithName("string"),
            Name = "emailAddress"
        };

        Parameter parameterEmailAddressMailAddress = new Parameter
        {
            DataType = mailAddressDataType,
            Name = "emailAddress"
        };

        Parameter parameterEmailAddressDisplayNameString =
            new Parameter
            {
                DataType = GetDataTypeWithName("string"),
                Name = "displayName"
            };

        #region Add Instanatiating methods

        // Create
        Method createMailMessageMethod = 
            new Method("CreateMailMessage", Enums.MethodType.Instantiating);

        project.Methods.Add(createMailMessageMethod);

        Method createHtmlMailMessageMethod = 
            new Method("CreateHtmlMailMessage", Enums.MethodType.Instantiating);

        project.Methods.Add(createHtmlMailMessageMethod);

        #endregion

        // From
        Method fromAddressStringMethod = 
            new Method("From", Enums.MethodType.Chaining);
        fromAddressStringMethod.Parameters.Add(parameterEmailAddressString);

        project.Methods.Add(fromAddressStringMethod);

        Method fromAddressStringStringMethod =
            new Method("From", Enums.MethodType.Chaining);
        fromAddressStringStringMethod.Parameters.Add(parameterEmailAddressString);
        fromAddressStringStringMethod.Parameters.Add(parameterEmailAddressDisplayNameString);

        project.Methods.Add(fromAddressStringStringMethod);

        Method fromAddressMailAddressMethod = 
            new Method("From", Enums.MethodType.Chaining);
        fromAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(fromAddressMailAddressMethod);

        // To
        Method toAddressStringMethod = 
            new Method("To", Enums.MethodType.Chaining);
        toAddressStringMethod.Parameters.Add(parameterEmailAddressString);

        project.Methods.Add(toAddressStringMethod);

        Method toAddressMailAddressMethod = 
            new Method("To", Enums.MethodType.Chaining);
        toAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(toAddressMailAddressMethod);

        // Build
        Method buildMethod = 
            new Method("Build", Enums.MethodType.Executing, mailMessageDataType);

        project.Methods.Add(buildMethod);

        // Chains
        #region Add MethodLinks

        #region Add MethodLinks that call into From

        project.MethodLinks.Add(new MethodLink(
            createMailMessageMethod.Id,
            fromAddressStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            createMailMessageMethod.Id,
            fromAddressStringStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            createMailMessageMethod.Id,
            fromAddressMailAddressMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            createHtmlMailMessageMethod.Id,
            fromAddressStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            createHtmlMailMessageMethod.Id,
            fromAddressStringStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            createHtmlMailMessageMethod.Id,
            fromAddressMailAddressMethod.Id));

        #endregion

        // Calls to To
        // fromAddressStringMethod
        project.MethodLinks.Add(new MethodLink(
            fromAddressStringMethod.Id,
            toAddressStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            fromAddressStringMethod.Id,
            toAddressMailAddressMethod.Id));

        // fromAddressStringStringMethod
        project.MethodLinks.Add(new MethodLink(
            fromAddressStringStringMethod.Id,
            toAddressStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            fromAddressStringStringMethod.Id,
            toAddressMailAddressMethod.Id));

        // fromAddressMailAddressMethod
        project.MethodLinks.Add(new MethodLink(
            fromAddressMailAddressMethod.Id,
            toAddressStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            fromAddressMailAddressMethod.Id,
            toAddressMailAddressMethod.Id));

        // toAddressStringMethod
        project.MethodLinks.Add(new MethodLink(
            toAddressStringMethod.Id,
            toAddressStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            toAddressStringMethod.Id,
            toAddressMailAddressMethod.Id));

        // toAddressMailAddressMethod
        project.MethodLinks.Add(new MethodLink(
            toAddressMailAddressMethod.Id,
            toAddressStringMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            toAddressMailAddressMethod.Id,
            toAddressMailAddressMethod.Id));

        #region Calls into Build method

        project.MethodLinks.Add(new MethodLink(
            toAddressStringMethod.Id,
            buildMethod.Id));

        project.MethodLinks.Add(new MethodLink(
            toAddressMailAddressMethod.Id,
            buildMethod.Id));

        #endregion

        #endregion

        Assert.False(project.CanCreateOutputFiles);

        #region Update InterfaceSpecs

        // Populate interfaceSpec object names
        var iMustAddFromAddress =
            project.InterfaceSpecs.First(i =>
                i.CalledByMethodId.Count == 2 &&
                i.CallsIntoMethodIds.Count == 3 &&
                i.CalledByMethodId.Contains(createMailMessageMethod.Id) &&
                i.CalledByMethodId.Contains(createHtmlMailMessageMethod.Id) &&
                i.CallsIntoMethodIds.Contains(fromAddressStringMethod.Id) &&
                i.CallsIntoMethodIds.Contains(fromAddressStringStringMethod.Id) &&
                i.CallsIntoMethodIds.Contains(fromAddressMailAddressMethod.Id));
        iMustAddFromAddress.Name = "IMustAddFromAddress";

        var iCanAddToAddress =
            project.InterfaceSpecs.First(i =>
                i.CalledByMethodId.Count == 3 &&
                i.CallsIntoMethodIds.Count == 2 &&
                i.CalledByMethodId.Contains(fromAddressStringMethod.Id) &&
                i.CalledByMethodId.Contains(fromAddressStringStringMethod.Id) &&
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

        #endregion

        #region Build output files

        Assert.True(project.CanCreateOutputFiles);

        var fluentInterfaceFileCreator =
            FluentInterfaceCreatorFactory.GetFluentInterfaceFileCreator(project);

        Assert.NotNull(fluentInterfaceFileCreator);

        File.WriteAllText(
            Path.Combine(@"e:\temp\output",
                $"{project.FactoryClassName}.{project.OutputLanguage.FileExtension}"),
            fluentInterfaceFileCreator.CreateFluentInterfaceFile().FormattedText());

        PersistenceService.SaveProjectToDisk(project,
            @"E:\temp\output\project.json");

        #endregion
    }
}