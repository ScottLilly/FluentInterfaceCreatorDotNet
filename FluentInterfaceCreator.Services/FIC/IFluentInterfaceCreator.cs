using FluentInterfaceCreator.Models.Outputs;

namespace FluentInterfaceCreator.Services.FIC;

public interface IFluentInterfaceCreator
{
    FluentInterfaceFile CreateInSingleFile();
    IEnumerable<FluentInterfaceFile> CreateInMultipleFiles();
}