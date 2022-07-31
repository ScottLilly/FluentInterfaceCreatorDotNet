using System.ComponentModel;

namespace FluentInterfaceCreator.ViewModels;

public class YesNoViewModel : INotifyPropertyChanged
{
    public string Title { get; }
    public string Question { get; }
    public bool ResponseIsYes { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public YesNoViewModel(string title, string question)
    {
        Title = title;
        Question = question;
    }

    public YesNoViewModel(string title, List<string> question)
    {
        Title = title;
        Question = string.Join(Environment.NewLine, question);
    }
}