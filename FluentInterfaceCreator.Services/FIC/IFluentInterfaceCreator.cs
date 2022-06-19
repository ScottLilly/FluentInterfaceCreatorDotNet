using FluentInterfaceCreator.Models;

namespace FluentInterfaceCreator.Services.FIC;

public interface IFluentInterfaceCreator
{
    FluentInterfaceFile CreateInSingleFile();
    IEnumerable<FluentInterfaceFile> CreateInMultipleFiles();
}