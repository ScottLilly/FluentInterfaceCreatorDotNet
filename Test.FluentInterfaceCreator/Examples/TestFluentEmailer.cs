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

        DataType streamDataType = new DataType
        {
            ContainingNamespace = "System.IO",
            Name = "Stream"
        };
        project.DataTypes.Add(streamDataType);

        DataType contentTypeDataType = new DataType
        {
            ContainingNamespace = "System.Net.Mime",
            Name = "ContentType"
        };
        project.DataTypes.Add(contentTypeDataType);

        #endregion

        #region Add Parameters

        Parameter parameterEmailAddressString = 
            new Parameter("emailAddress", GetDataTypeWithName("string"));

        Parameter parameterEmailAddressIEnumerableString =
            new Parameter("emailAddresses", GetDataTypeWithName("string"));
        parameterEmailAddressIEnumerableString.UseIEnumerable = true;

        Parameter parameterEmailAddressMailAddress =
            new Parameter("emailAddress", mailAddressDataType);

        Parameter parameterEmailAddressIEnumerableMailAddress = 
            new Parameter("emailAddresses", mailAddressDataType);
        parameterEmailAddressIEnumerableMailAddress.UseIEnumerable = true;

        Parameter parameterEmailAddressDisplayNameString =
            new Parameter("displayName", GetDataTypeWithName("string"));

        Parameter parameterMailPriority =
            new Parameter("priority", mailPriorityDataType, "MailPriority.Normal");

        Parameter parameterEncodingType =
            new Parameter("encodingType", encodingDataType);

        Parameter parameterAttachments =
            new Parameter("attachments", GetDataTypeWithName("string"));
        parameterAttachments.UseIEnumerable = true;

        Parameter parameterStream =
            new Parameter("stream", streamDataType);

        Parameter parameterContentType =
            new Parameter("contentType", contentTypeDataType);

        #endregion

        #region Add Instantiating methods

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

        #endregion

        #region Add Chaining methods

        #region From methods

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

        Method fromAddressStringStringEncodingMethod =
            new Method("From", Enums.MethodType.Chaining);
        fromAddressStringStringEncodingMethod.Parameters.Add(parameterEmailAddressString);
        fromAddressStringStringEncodingMethod.Parameters.Add(parameterEmailAddressDisplayNameString);
        fromAddressStringStringEncodingMethod.Parameters.Add(parameterEncodingType);

        project.Methods.Add(fromAddressStringStringEncodingMethod);

        Method fromAddressMailAddressMethod = 
            new Method("From", Enums.MethodType.Chaining);
        fromAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(fromAddressMailAddressMethod);

        var fromMethodIds = new List<Guid>
        {
            fromAddressStringMethod.Id,
            fromAddressStringStringMethod.Id,
            fromAddressStringStringEncodingMethod.Id,
            fromAddressMailAddressMethod.Id
        };

        #endregion

        #region To methods

        // To
        Method toAddressStringMethod = 
            new Method("To", Enums.MethodType.Chaining);
        toAddressStringMethod.Parameters.Add(parameterEmailAddressString);

        project.Methods.Add(toAddressStringMethod);

        Method toAddressIEnumerableStringMethod =
            new Method("To", Enums.MethodType.Chaining);
        toAddressIEnumerableStringMethod.Parameters.Add(parameterEmailAddressIEnumerableString);

        project.Methods.Add(toAddressIEnumerableStringMethod);

        Method toAddressStringStringMethod =
            new Method("To", Enums.MethodType.Chaining);
        toAddressStringStringMethod.Parameters.Add(parameterEmailAddressString);
        toAddressStringStringMethod.Parameters.Add(parameterEmailAddressDisplayNameString);

        project.Methods.Add(toAddressStringStringMethod);

        Method toAddressStringStringEncodingMethod =
            new Method("To", Enums.MethodType.Chaining);
        toAddressStringStringEncodingMethod.Parameters.Add(parameterEmailAddressString);
        toAddressStringStringEncodingMethod.Parameters.Add(parameterEmailAddressDisplayNameString);
        toAddressStringStringEncodingMethod.Parameters.Add(parameterEncodingType);

        project.Methods.Add(toAddressStringStringEncodingMethod);

        Method toAddressMailAddressMethod = 
            new Method("To", Enums.MethodType.Chaining);
        toAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(toAddressMailAddressMethod);

        Method toAddressIEnumerableMailAddressMethod =
            new Method("To", Enums.MethodType.Chaining);
        toAddressIEnumerableMailAddressMethod.Parameters.Add(parameterEmailAddressIEnumerableMailAddress);

        project.Methods.Add(toAddressIEnumerableMailAddressMethod);

        var toMethodIds = new List<Guid>
        {
            toAddressStringMethod.Id,
            toAddressIEnumerableStringMethod.Id,
            toAddressStringStringMethod.Id,
            toAddressStringStringEncodingMethod.Id,
            toAddressMailAddressMethod.Id,
            toAddressIEnumerableMailAddressMethod.Id
        };

        #endregion

        #region CC methods

        // CC
        Method ccAddressStringMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressStringMethod.Parameters.Add(parameterEmailAddressString);

        project.Methods.Add(ccAddressStringMethod);

        Method ccAddressIEnumerableStringMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressIEnumerableStringMethod.Parameters.Add(parameterEmailAddressIEnumerableString);

        project.Methods.Add(ccAddressIEnumerableStringMethod);

        Method ccAddressStringStringMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressStringStringMethod.Parameters.Add(parameterEmailAddressString);
        ccAddressStringStringMethod.Parameters.Add(parameterEmailAddressDisplayNameString);

        project.Methods.Add(ccAddressStringStringMethod);

        Method ccAddressStringStringEncodingMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressStringStringEncodingMethod.Parameters.Add(parameterEmailAddressString);
        ccAddressStringStringEncodingMethod.Parameters.Add(parameterEmailAddressDisplayNameString);
        ccAddressStringStringEncodingMethod.Parameters.Add(parameterEncodingType);

        project.Methods.Add(ccAddressStringStringEncodingMethod);

        Method ccAddressMailAddressMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(ccAddressMailAddressMethod);

        Method ccAddressIEnumerableMailAddressMethod =
            new Method("CC", Enums.MethodType.Chaining);
        ccAddressIEnumerableMailAddressMethod.Parameters.Add(parameterEmailAddressIEnumerableMailAddress);

        project.Methods.Add(ccAddressIEnumerableMailAddressMethod);

        var ccMethodIds = new List<Guid>
        {
            ccAddressStringMethod.Id,
            ccAddressIEnumerableStringMethod.Id,
            ccAddressStringStringMethod.Id,
            ccAddressStringStringEncodingMethod.Id,
            ccAddressMailAddressMethod.Id,
            ccAddressIEnumerableMailAddressMethod.Id
        };

        #endregion

        #region BCC methods

        // BCC
        Method bccAddressStringMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressStringMethod.Parameters.Add(parameterEmailAddressString);

        project.Methods.Add(bccAddressStringMethod);

        Method bccAddressIEnumerableStringMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressIEnumerableStringMethod.Parameters.Add(parameterEmailAddressIEnumerableString);

        project.Methods.Add(bccAddressIEnumerableStringMethod);

        Method bccAddressStringStringMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressStringStringMethod.Parameters.Add(parameterEmailAddressString);
        bccAddressStringStringMethod.Parameters.Add(parameterEmailAddressDisplayNameString);

        project.Methods.Add(bccAddressStringStringMethod);

        Method bccAddressStringStringEncodingMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressStringStringEncodingMethod.Parameters.Add(parameterEmailAddressString);
        bccAddressStringStringEncodingMethod.Parameters.Add(parameterEmailAddressDisplayNameString);
        bccAddressStringStringEncodingMethod.Parameters.Add(parameterEncodingType);

        project.Methods.Add(bccAddressStringStringEncodingMethod);

        Method bccAddressMailAddressMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressMailAddressMethod.Parameters.Add(parameterEmailAddressMailAddress);

        project.Methods.Add(bccAddressMailAddressMethod);

        Method bccAddressIEnumerableMailAddressMethod =
            new Method("BCC", Enums.MethodType.Chaining);
        bccAddressIEnumerableMailAddressMethod.Parameters.Add(parameterEmailAddressIEnumerableMailAddress);

        project.Methods.Add(bccAddressIEnumerableMailAddressMethod);

        var bccMethodIds = new List<Guid>
        {
            bccAddressStringMethod.Id,
            bccAddressIEnumerableStringMethod.Id,
            bccAddressStringStringMethod.Id,
            bccAddressStringStringEncodingMethod.Id,
            bccAddressMailAddressMethod.Id,
            bccAddressIEnumerableMailAddressMethod.Id
        };

        #endregion

        #region Subject methods

        // Subject
        Method subjectMethod =
            new Method("Subject", Enums.MethodType.Chaining);
        subjectMethod.Parameters.Add(new Parameter("subject", GetDataTypeWithName("string")));

        project.Methods.Add(subjectMethod);

        Method subjectWithEncodingMethod =
            new Method("Subject", Enums.MethodType.Chaining);
        subjectWithEncodingMethod.Parameters.Add(new Parameter("subject", GetDataTypeWithName("string")));
        subjectWithEncodingMethod.Parameters.Add(parameterEncodingType);

        project.Methods.Add(subjectWithEncodingMethod);

        var subjectMethodIds = new List<Guid>
        {
            subjectMethod.Id,
            subjectWithEncodingMethod.Id
        };

        #endregion

        #region Body methods

        // Body
        Method bodyMethod =
            new Method("Body", Enums.MethodType.Chaining);
        bodyMethod.Parameters.Add(new Parameter("body", GetDataTypeWithName("string")));

        project.Methods.Add(bodyMethod);

        Method bodyWithEncodingMethod =
            new Method("Body", Enums.MethodType.Chaining);
        bodyWithEncodingMethod.Parameters.Add(new Parameter("body", GetDataTypeWithName("string")));
        bodyWithEncodingMethod.Parameters.Add(parameterEncodingType);

        project.Methods.Add(bodyWithEncodingMethod);

        var bodyMethodIds = new List<Guid>
        {
            bodyMethod.Id,
            bodyWithEncodingMethod.Id
        };

        #endregion

        #region Attachment methods

        // Attachment
        // String filename methods
        Method attachmentMethod =
            new Method("AddAttachment", Enums.MethodType.Chaining);
        attachmentMethod.Parameters.Add(new Parameter("filename", GetDataTypeWithName("string")));

        project.Methods.Add(attachmentMethod);

        Method attachmentStringMimeTypeMethod =
            new Method("AddAttachment", Enums.MethodType.Chaining);
        attachmentStringMimeTypeMethod.Parameters.Add(new Parameter("filename", GetDataTypeWithName("string")));
        attachmentStringMimeTypeMethod.Parameters.Add(new Parameter("mimeType", GetDataTypeWithName("string")));

        project.Methods.Add(attachmentStringMimeTypeMethod);

        Method attachmentStringContentTypeMethod =
            new Method("AddAttachment", Enums.MethodType.Chaining);
        attachmentStringContentTypeMethod.Parameters.Add(new Parameter("filename", GetDataTypeWithName("string")));
        attachmentStringContentTypeMethod.Parameters.Add(parameterContentType);

        project.Methods.Add(attachmentStringContentTypeMethod);

        // Stream methods

        Method attachmentStreamStringMethod =
            new Method("AddAttachment", Enums.MethodType.Chaining);
        attachmentStreamStringMethod.Parameters.Add(parameterStream);
        attachmentStreamStringMethod.Parameters.Add(new Parameter("name", GetDataTypeWithName("string")));

        project.Methods.Add(attachmentStreamStringMethod);

        Method attachmentStreamStringMimeTypeMethod =
            new Method("AddAttachment", Enums.MethodType.Chaining);
        attachmentStreamStringMimeTypeMethod.Parameters.Add(parameterStream);
        attachmentStreamStringMimeTypeMethod.Parameters.Add(new Parameter("name", GetDataTypeWithName("string")));
        attachmentStreamStringMimeTypeMethod.Parameters.Add(new Parameter("mimeType", GetDataTypeWithName("string")));

        project.Methods.Add(attachmentStreamStringMimeTypeMethod);

        Method attachmentStreamContentTypeMethod =
            new Method("AddAttachment", Enums.MethodType.Chaining);
        attachmentStreamContentTypeMethod.Parameters.Add(parameterStream);
        attachmentStreamContentTypeMethod.Parameters.Add(parameterContentType);

        project.Methods.Add(attachmentStreamContentTypeMethod);

        // Multiple file names method
        Method attachmentsMethod =
            new Method("AddAttachments", Enums.MethodType.Chaining);
        attachmentMethod.Parameters.Add(parameterAttachments);

        project.Methods.Add(attachmentsMethod);

        var attachmentMethodIds = new List<Guid>
        {
            attachmentMethod.Id,
            attachmentStringMimeTypeMethod.Id,
            attachmentStringContentTypeMethod.Id,
            attachmentStreamStringMethod.Id,
            attachmentStreamStringMimeTypeMethod.Id,
            attachmentStreamContentTypeMethod.Id,
            attachmentsMethod.Id
        };

        #endregion

        #endregion

        #region Add Executing methods

        // Build
        Method buildMethod = 
            new Method("Build", Enums.MethodType.Executing, mailMessageDataType);

        project.Methods.Add(buildMethod);

        var buildMethodIds = new List<Guid>
        {
            buildMethod.Id
        };

        #endregion

        #region Add MethodLinks

        // Calls into From
        AddMethodLinks(project, 
            createMethodIds, 
            fromMethodIds);

        // Calls into To
        AddMethodLinks(project, 
            fromMethodIds, 
            toMethodIds);

        // Calls into To, CC, BCC, or Subject
        AddMethodLinks(project,
            toMethodIds.Concat(ccMethodIds).Concat(bccMethodIds),
            toMethodIds.Concat(ccMethodIds).Concat(bccMethodIds).Concat(subjectMethodIds));

        // Calls into Body
        AddMethodLinks(project, 
            subjectMethodIds, 
            bodyMethodIds);

        // Calls into Attachment
        AddMethodLinks(project, 
            bodyMethodIds.Concat(attachmentMethodIds), 
            attachmentMethodIds);

        // Calls into Build
        AddMethodLinks(project,
            bodyMethodIds.Concat(attachmentMethodIds), 
            buildMethodIds);

        #endregion

        Assert.False(project.CanCreateOutputFiles);

        #region Set InterfaceSpec Names

        GetInterfaceSpec(project,
            createMethodIds, 
            fromMethodIds)
            .Name = "IMustAddFromAddress";

        GetInterfaceSpec(project,
            fromMethodIds, 
            toMethodIds)
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

        // Body or Attachment to Attachment or Build
        GetInterfaceSpec(project,
            bodyMethodIds.Concat(attachmentMethodIds),
            attachmentMethodIds.Concat(buildMethodIds))
            .Name = "ICanAddAttachmentOrBuild";

        #endregion

        Assert.True(project.CanCreateOutputFiles);

        #region Build output files

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