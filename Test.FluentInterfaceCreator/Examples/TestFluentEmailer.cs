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
            Name = "MailPriority"
        };

        DataType mailMessageDataType = new DataType
        {
            ContainingNamespace = "System.Net.Mail",
            Name = "MailMessage"
        };
        project.DataTypes.Add(mailMessageDataType);

        DataType mailAddressDataType = new DataType
        {
            ContainingNamespace = "System.Net.Mail",
            Name = "MailAddress"
        };
        project.DataTypes.Add(mailAddressDataType);

        DataType encodingDataType = new DataType
        {
            ContainingNamespace = "System.Text",
            Name = "Encoding"
        };
        project.DataTypes.Add(encodingDataType);

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

        Parameter parameterEncodingType =
            new Parameter("encodingType", encodingDataType);

        #endregion

        // Create
        Method createMailMessageMethod = 
            new Method("CreateMailMessage", Enums.MethodType.Instantiating);
        createMailMessageMethod.Parameters.Add(parameterMailPriority);

        project.Methods.Add(createMailMessageMethod);

        Method createHtmlMailMessageMethod = 
            new Method("CreateHtmlMailMessage", Enums.MethodType.Instantiating);
        createHtmlMailMessageMethod.Parameters.Add(parameterMailPriority);

        project.Methods.Add(createHtmlMailMessageMethod);

        var createMethodIds = new List<Guid>
        {
            createMailMessageMethod.Id,
            createHtmlMailMessageMethod.Id
        };

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

        var fromMethodIds = new List<Guid>
        {
            fromAddressStringMethod.Id,
            fromAddressStringStringMethod.Id,
            fromAddressMailAddressMethod.Id
        };

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

        var toMethodIds = new List<Guid>
        {
            toAddressStringMethod.Id,
            toAddressStringStringMethod.Id,
            toAddressMailAddressMethod.Id
        };

        // CC
        Method ccAddressStringMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressStringMethod.Parameters.Add(parameterEmailAddressString);

        project.Methods.Add(ccAddressStringMethod);

        Method ccAddressStringStringMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressStringStringMethod.Parameters.Add(parameterEmailAddressString);
        ccAddressStringStringMethod.Parameters.Add(parameterEmailAddressDisplayNameString);

        project.Methods.Add(ccAddressStringStringMethod);

        Method ccAddressMailAddressMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(ccAddressMailAddressMethod);

        var ccMethodIds = new List<Guid>
        {
            ccAddressStringMethod.Id,
            ccAddressStringStringMethod.Id,
            ccAddressMailAddressMethod.Id
        };

        // BCC
        Method bccAddressStringMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressStringMethod.Parameters.Add(parameterEmailAddressString);

        project.Methods.Add(bccAddressStringMethod);

        Method bccAddressStringStringMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressStringStringMethod.Parameters.Add(parameterEmailAddressString);
        bccAddressStringStringMethod.Parameters.Add(parameterEmailAddressDisplayNameString);

        project.Methods.Add(bccAddressStringStringMethod);

        Method bccAddressMailAddressMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(bccAddressMailAddressMethod);

        var bccMethodIds = new List<Guid>
        {
            bccAddressStringMethod.Id,
            bccAddressStringStringMethod.Id,
            bccAddressMailAddressMethod.Id
        };

        // Subject
        Method subjectMethod =
            new Method("Subject", Enums.MethodType.Chaining);
        subjectMethod.Parameters.Add(new Parameter("subject", GetDataTypeWithName("string")));

        project.Methods.Add(subjectMethod);

        var subjectMethodIds = new List<Guid>
        {
            subjectMethod.Id
        };

        // Body
        Method bodyMethod =
            new Method("Body", Enums.MethodType.Chaining);
        bodyMethod.Parameters.Add(new Parameter("body", GetDataTypeWithName("string")));

        project.Methods.Add(bodyMethod);

        var bodyMethodIds = new List<Guid>
        {
            bodyMethod.Id
        };

        // Attachment
        Method attachmentMethod =
            new Method("AddAttachment", Enums.MethodType.Chaining);
        attachmentMethod.Parameters.Add(new Parameter("filename", GetDataTypeWithName("string")));

        project.Methods.Add(attachmentMethod);

        var attachmentMethodIds = new List<Guid>
        {
            attachmentMethod.Id
        };

        // Build
        Method buildMethod = 
            new Method("Build", Enums.MethodType.Executing, mailMessageDataType);

        project.Methods.Add(buildMethod);

        var buildMethodIds = new List<Guid>
        {
            buildMethod.Id
        };

        #region Add MethodLinks

        // Calls into From
        AddMethodLinks(project, createMethodIds, fromMethodIds);

        // Calls into To
        AddMethodLinks(project, fromMethodIds, toMethodIds);

        // Calls into To, CC, BCC, or Subject
        AddMethodLinks(project,
            toMethodIds.Concat(ccMethodIds).Concat(bccMethodIds),
            toMethodIds.Concat(ccMethodIds).Concat(bccMethodIds).Concat(subjectMethodIds));

        // Calls into Body
        AddMethodLinks(project, subjectMethodIds, bodyMethodIds);

        // Calls into Attachment
        AddMethodLinks(project, 
            bodyMethodIds.Concat(attachmentMethodIds), attachmentMethodIds);

        // Calls into Build
        AddMethodLinks(project,
            bodyMethodIds.Concat(attachmentMethodIds), buildMethodIds);

        #endregion

        Assert.False(project.CanCreateOutputFiles);

        #region Set InterfaceSpecs Names

        GetInterfaceSpec(project,
            createMethodIds, fromMethodIds)
            .Name = "IMustAddFromAddress";

        GetInterfaceSpec(project,
            fromMethodIds, toMethodIds)
            .Name = "IMustAddToAddress";

        // To, CC, BCC to To, CC, BCC, or Subject
        GetInterfaceSpec(
            project,
            toMethodIds.Concat(ccMethodIds).Concat(bccMethodIds),
            toMethodIds.Concat(ccMethodIds).Concat(bccMethodIds).Concat(subjectMethodIds))
            .Name = "ICanAddToCcBccOrSubject";

        // Subject to Body
        GetInterfaceSpec(project,
            subjectMethodIds,
            bodyMethodIds)
            .Name = "IMustAddBody";

        // Body or Attachment to Build
        GetInterfaceSpec(project,
            bodyMethodIds.Concat(attachmentMethodIds),
            attachmentMethodIds.Concat(buildMethodIds))
            .Name = "ICanAddAttachmentOrBuild";

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
}