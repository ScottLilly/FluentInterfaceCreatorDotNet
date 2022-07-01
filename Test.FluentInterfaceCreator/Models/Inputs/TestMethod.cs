using FluentInterfaceCreator.Models;
using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestMethod : BaseTestClass
{
    [Fact]
    public void Test_Instantiate()
    {
        var method = new Method();

        Assert.NotNull(method);
        Assert.NotEqual(Guid.Empty, method.Id);
    }

    [Fact]
    public void Test_MethodTypes()
    {
        var method = new Method();
        method.Type = Enums.MethodType.Instantiating;

        Assert.True(method.CanStartChainPair);
        Assert.False(method.CanEndChainPair);
        Assert.False(method.RequiresReturnDataType);

        method.Type = Enums.MethodType.Chaining;

        Assert.True(method.CanStartChainPair);
        Assert.True(method.CanEndChainPair);
        Assert.False(method.RequiresReturnDataType);

        method.Type = Enums.MethodType.Executing;

        Assert.False(method.CanStartChainPair);
        Assert.True(method.CanEndChainPair);
        Assert.True(method.RequiresReturnDataType);
    }

    [Theory]
    [MemberData(nameof(DataTypeNames))]
    public void Test_ExecutingMethodReturnValues_NonEnumerable(string dataTypeName)
    {
        var method = new Method();
        method.Type = Enums.MethodType.Executing;
        method.ReturnDataType = GetDataTypeWithName(dataTypeName);

        Assert.False(method.UseIEnumerable);
        Assert.Equal(dataTypeName, method.FormattedReturnDataType);
    }

    [Theory]
    [MemberData(nameof(DataTypeNames))]
    public void Test_ExecutingMethodReturnValues_Enumerable(string dataTypeName)
    {
        var method = new Method();
        method.Type = Enums.MethodType.Executing;
        method.UseIEnumerable = true;
        method.ReturnDataType = GetDataTypeWithName(dataTypeName);

        Assert.Equal($"IEnumerable<{dataTypeName}>", method.FormattedReturnDataType);
        Assert.Equal($"IEnumerable<{dataTypeName}>|=", method.DataTypeOnlySignature);
    }

    [Fact]
    public void Test_NamespacesNeeded_NoParameters()
    {
        var method = new Method();

        Assert.Empty(method.RequiredNamespaces);
    }

    [Fact]
    public void Test_NamespacesNeeded_OneParameter_NoNamespaces()
    {
        var method = new Method();
        method.Parameters.Add(
            new Parameter
            {
                Name = "value", 
                DataType = GetDataTypeWithName("int"), 
                UseIEnumerable = false
            });

        Assert.Empty(method.RequiredNamespaces);
        Assert.Equal("|=int", method.DataTypeOnlySignature);
    }

    [Fact]
    public void Test_NamespacesNeeded_MultipleParameters_NoNamespaces()
    {
        var method = new Method();
        method.Parameters.Add(
            new Parameter
            {
                Name = "value1",
                DataType = GetDataTypeWithName("int"),
                UseIEnumerable = false
            });
        method.Parameters.Add(
            new Parameter
            {
                Name = "value2",
                DataType = GetDataTypeWithName("bool"),
                UseIEnumerable = false
            });

        Assert.Empty(method.RequiredNamespaces);
        Assert.Equal("|=int:bool", method.DataTypeOnlySignature);
    }

    [Fact]
    public void Test_NamespacesNeeded_OneParameter_GenericsNamespace()
    {
        var method = new Method();
        method.Parameters.Add(
            new Parameter
            {
                Name = "value",
                DataType = GetDataTypeWithName("int"),
                UseIEnumerable = true
            });

        Assert.Single(method.RequiredNamespaces);
        Assert.Equal(GENERIC_NAMESPACE_NAME, method.RequiredNamespaces.First());
        Assert.Equal("|=IEnumerable<int>", method.DataTypeOnlySignature);
    }

    [Fact]
    public void Test_NamespacesNeeded_MultipleParameters_GenericsNamespace()
    {
        var method = new Method();
        method.Parameters.Add(
            new Parameter
            {
                Name = "value1",
                DataType = GetDataTypeWithName("int"),
                UseIEnumerable = true
            });
        method.Parameters.Add(
            new Parameter
            {
                Name = "value2",
                DataType = GetDataTypeWithName("bool"),
                UseIEnumerable = false
            });

        Assert.Single(method.RequiredNamespaces);
        Assert.Equal(GENERIC_NAMESPACE_NAME, method.RequiredNamespaces.First());
        Assert.Equal("|=IEnumerable<int>:bool", method.DataTypeOnlySignature);
    }

    [Fact]
    public void Test_NamespacesNeeded_DateTimeParameter_NeedsSystem()
    {
        var method = new Method();
        method.Parameters.Add(
            new Parameter
            {
                Name = "value",
                DataType = GetDataTypeWithName("DateTime"),
                UseIEnumerable = false
            });

        Assert.Single(method.RequiredNamespaces);
        Assert.Equal("System", method.RequiredNamespaces.First());
        Assert.Equal("|=DateTime", method.DataTypeOnlySignature);
    }

    [Fact]
    public void Test_NamespacesNeeded_DateTimeParameters_NeedsSystem()
    {
        var method = new Method();
        method.Parameters.Add(
            new Parameter
            {
                Name = "value1",
                DataType = GetDataTypeWithName("DateTime"),
                UseIEnumerable = false
            });
        method.Parameters.Add(
            new Parameter
            {
                Name = "value2",
                DataType = GetDataTypeWithName("DateTime"),
                UseIEnumerable = false
            });

        Assert.Single(method.RequiredNamespaces);
        Assert.Equal("System", method.RequiredNamespaces.First());
        Assert.Equal("|=DateTime:DateTime", method.DataTypeOnlySignature);
    }

    [Fact]
    public void Test_NamespacesNeeded_OneParameter_MultipleNamespaces()
    {
        var method = new Method();
        method.Parameters.Add(
            new Parameter
            {
                Name = "value",
                DataType = GetDataTypeWithName("DateTime"),
                UseIEnumerable = true
            });

        Assert.Equal(2, method.RequiredNamespaces.Count());
        Assert.Contains("System", method.RequiredNamespaces);
        Assert.Contains(GENERIC_NAMESPACE_NAME, method.RequiredNamespaces);
        Assert.Equal("|=IEnumerable<DateTime>", method.DataTypeOnlySignature);
    }

    [Fact]
    public void Test_NamespacesNeeded_MultipleParameters_MultipleNamespace()
    {
        var method = new Method
        {
            Name = "TestMethod",
            Type = Enums.MethodType.Executing,
            ReturnDataType = GetDataTypeWithName("string"),
            UseIEnumerable = true
        };

        method.Parameters.Add(
            new Parameter
            {
                Name = "value1",
                DataType = GetDataTypeWithName("int"),
                UseIEnumerable = true
            });
        method.Parameters.Add(
            new Parameter
            {
                Name = "value2",
                DataType = GetDataTypeWithName("DateTime"),
                UseIEnumerable = false
            });

        Assert.Equal(2, method.RequiredNamespaces.Count());
        Assert.Contains("System", method.RequiredNamespaces);
        Assert.Contains(GENERIC_NAMESPACE_NAME, method.RequiredNamespaces);
        Assert.Equal("IEnumerable<string>|TestMethod=IEnumerable<int>:DateTime", method.DataTypeOnlySignature);
    }
}