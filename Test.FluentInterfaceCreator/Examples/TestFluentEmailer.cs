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
        project.NamespaceForFactoryClass = "FluentEmail";
        project.FactoryClassName = "FluentMailMessage";

        #endregion

        #region Add DataTypes

        // Add non-standard DataTypes
        DataType mailPriorityDataType = new DataType
        {
            ContainingNamespace = "System.Net.Mail",
            Name = "MailPriority",
            IsNative = false
        };

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

        #region Add Parameters

        Parameter parameterEmailAddressString = 
            new Parameter("emailAddress", GetDataTypeWithName("string"));

        Parameter parameterEmailAddressMailAddress = 
            new Parameter("emailAddress", mailAddressDataType);

        Parameter parameterEmailAddressDisplayNameString =
            new Parameter("displayName", GetDataTypeWithName("string"));

        Parameter parameterMailPriority =
            new Parameter("priority", mailPriorityDataType, "MailPriority.Normal");

        #endregion

        #region Add Instanatiating methods

        // Create
        Method createMailMessageMethod = 
            new Method("CreateMailMessage", Enums.MethodType.Instantiating);
        createMailMessageMethod.Parameters.Add(parameterMailPriority);

        project.Methods.Add(createMailMessageMethod);

        Method createHtmlMailMessageMethod = 
            new Method("CreateHtmlMailMessage", Enums.MethodType.Instantiating);
        createHtmlMailMessageMethod.Parameters.Add(parameterMailPriority);

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

        Method toAddressStringStringMethod =
            new Method("To", Enums.MethodType.Chaining);
        toAddressStringStringMethod.Parameters.Add(parameterEmailAddressString);
        toAddressStringStringMethod.Parameters.Add(parameterEmailAddressDisplayNameString);

        project.Methods.Add(toAddressStringStringMethod);

        Method toAddressMailAddressMethod = 
            new Method("To", Enums.MethodType.Chaining);
        toAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(toAddressMailAddressMethod);

        // Build
        Method buildMethod = 
            new Method("Build", Enums.MethodType.Executing, mailMessageDataType);

        project.Methods.Add(buildMethod);

        #region Add MethodLinks

        #region Add MethodLinks that call into From

        project.AddMethodLink(
            createMailMessageMethod.Id, 
            fromAddressStringMethod.Id);

        project.AddMethodLink(
            createMailMessageMethod.Id, 
            fromAddressStringStringMethod.Id);

        project.AddMethodLink(
            createMailMessageMethod.Id,
            fromAddressMailAddressMethod.Id);

        project.AddMethodLink(
            createHtmlMailMessageMethod.Id,
            fromAddressStringMethod.Id);

        project.AddMethodLink(
            createHtmlMailMessageMethod.Id,
            fromAddressStringStringMethod.Id);

        project.AddMethodLink(
            createHtmlMailMessageMethod.Id,
            fromAddressMailAddressMethod.Id);

        #endregion

        #region Add MethodLinks that only call into To

        // fromAddressStringMethod
        project.AddMethodLink(
            fromAddressStringMethod.Id,
            toAddressStringMethod.Id);

        project.AddMethodLink(
            fromAddressStringMethod.Id,
            toAddressStringStringMethod.Id);

        project.AddMethodLink(
            fromAddressStringMethod.Id,
            toAddressMailAddressMethod.Id);

        // fromAddressStringStringMethod
        project.AddMethodLink(
            fromAddressStringStringMethod.Id,
            toAddressStringMethod.Id);

        project.AddMethodLink(
            fromAddressStringStringMethod.Id,
            toAddressStringStringMethod.Id);

        project.AddMethodLink(
            fromAddressStringStringMethod.Id,
            toAddressMailAddressMethod.Id);

        // fromAddressMailAddressMethod
        project.AddMethodLink(
            fromAddressMailAddressMethod.Id,
            toAddressStringMethod.Id);

        project.AddMethodLink(
            fromAddressMailAddressMethod.Id,
            toAddressStringStringMethod.Id);

        project.AddMethodLink(
            fromAddressMailAddressMethod.Id,
            toAddressMailAddressMethod.Id);

        #endregion

        #region Add MethodLinks that call into To or Build

        // Calls into TO
        // toAddressStringMethod
        project.AddMethodLink(
            toAddressStringStringMethod.Id,
            toAddressStringMethod.Id);

        project.AddMethodLink(
            toAddressStringStringMethod.Id,
            toAddressStringStringMethod.Id);

        project.AddMethodLink(
            toAddressStringStringMethod.Id,
            toAddressMailAddressMethod.Id);

        // toAddressStringStringMethod
        project.AddMethodLink(
            toAddressStringMethod.Id,
            toAddressStringMethod.Id);

        project.AddMethodLink(
            toAddressStringMethod.Id,
            toAddressStringStringMethod.Id);

        project.AddMethodLink(
            toAddressStringMethod.Id,
            toAddressMailAddressMethod.Id);

        // toAddressMailAddressMethod
        project.AddMethodLink(
            toAddressMailAddressMethod.Id,
            toAddressStringMethod.Id);

        project.AddMethodLink(
            toAddressMailAddressMethod.Id,
            toAddressStringStringMethod.Id);

        project.AddMethodLink(
            toAddressMailAddressMethod.Id,
            toAddressMailAddressMethod.Id);

        // Calls into BUILD
        project.AddMethodLink(
            toAddressStringMethod.Id,
            buildMethod.Id);

        project.AddMethodLink(
            toAddressStringStringMethod.Id,
            buildMethod.Id);

        project.AddMethodLink(
            toAddressMailAddressMethod.Id,
            buildMethod.Id);

        #endregion

        #endregion

        Assert.False(project.CanCreateOutputFiles);

        #region Set InterfaceSpecs Names

        GetInterfaceSpec(project,
            new List<Guid>
            {
                createMailMessageMethod.Id,
                createHtmlMailMessageMethod.Id
            },
            new List<Guid>
            {
                fromAddressStringMethod.Id,
                fromAddressStringStringMethod.Id,
                fromAddressMailAddressMethod.Id
            }).Name = "IMustAddFromAddress";

        GetInterfaceSpec(project,
            new List<Guid>
            {
                fromAddressStringMethod.Id,
                fromAddressStringStringMethod.Id,
                fromAddressMailAddressMethod.Id
            },
            new List<Guid>
            {
                toAddressStringMethod.Id,
                toAddressStringStringMethod.Id,
                toAddressMailAddressMethod.Id
            }).Name = "IMustAddToAddress";

        GetInterfaceSpec(project,
            new List<Guid>
            {
                toAddressStringMethod.Id,
                toAddressStringStringMethod.Id,
                toAddressMailAddressMethod.Id
            },
            new List<Guid>
            {
                toAddressStringMethod.Id,
                toAddressStringStringMethod.Id,
                toAddressMailAddressMethod.Id,
                buildMethod.Id
            }).Name = "ICanAddToAddressOrBuild";

        #endregion

        #region Build output files

        Assert.True(project.CanCreateOutputFiles);

        var fluentInterfaceFileCreator =
            FluentInterfaceCreatorFactory.GetFluentInterfaceFileCreator(project);

        Assert.NotNull(fluentInterfaceFileCreator);

        //File.WriteAllText(
        //    Path.Combine(@"e:\temp\output",
        //        $"{project.FactoryClassName}.{project.OutputLanguage.FileExtension}"),
        //    fluentInterfaceFileCreator.CreateFluentInterfaceFile().FormattedText());

        //PersistenceService.SaveProjectToDisk(project,
        //    @"E:\temp\output\project.json");

        #endregion
    }

    private static InterfaceSpec GetInterfaceSpec(
        Project project, 
        IReadOnlyCollection<Guid> calledByMethodIds,
        IReadOnlyCollection<Guid> callsIntoMethodIds) =>
        project.InterfaceSpecs
            .First(i => i.CalledByMethodId.OrderBy(id => id)
                            .SequenceEqual(calledByMethodIds.OrderBy(id => id)) &&
                        i.CallsIntoMethodIds.OrderBy(id => id)
                            .SequenceEqual(callsIntoMethodIds.OrderBy(id => id)));
}