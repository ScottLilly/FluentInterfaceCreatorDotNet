using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Services;
using FluentInterfaceCreator.ViewModels;
using FluentInterfaceCreator.Windows;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace FluentInterfaceCreator;

public partial class MainWindow : Window
{
    private ProjectEditor VM =>
        DataContext as ProjectEditor;

    public MainWindow()
    {
        InitializeComponent();
        this.Closing += OnClosing;

        DataContext = new ProjectEditor();

        ApplySortingToControls();
    }

    #region "File" menu options

    private void CreateNewProject_OnClick(object sender, RoutedEventArgs e)
    {
        string selectedLanguage = "C#"; // Currently, only support C#
        OutputLanguage outputLanguage = 
            VM.OutputLanguages.First(ol => ol.Name == selectedLanguage);

        VM.StartNewProject(outputLanguage);
        ApplySortingToControls();
    }

    private void LoadProject_OnClick(object sender, RoutedEventArgs e)
    {
        if (!OkToDiscardAnyChanges())
        {
            return;
        }

        OpenFileDialog dialog =
            new OpenFileDialog
            {
                DefaultExt = PersistenceService.FILE_NAME_EXTENSION,
                Filter = $"Fluent Interface Creator projects (*{PersistenceService.FILE_NAME_EXTENSION})|*{PersistenceService.FILE_NAME_EXTENSION}"
            };

        bool? result = dialog.ShowDialog(this);

        if (result == true)
        {
            VM.LoadProjectFromFile(dialog.FileName);
            ApplySortingToControls();
        }
    }

    private void SaveProject_OnClick(object sender, RoutedEventArgs e)
    {
        SaveFileDialog dialog =
            new SaveFileDialog
            {
                FileName = VM.Project?.Name,
                DefaultExt = PersistenceService.FILE_NAME_EXTENSION,
                Filter = $"Fluent Interface Creator projects (*{PersistenceService.FILE_NAME_EXTENSION})|*{PersistenceService.FILE_NAME_EXTENSION}"
            };

        bool? result = dialog.ShowDialog(this);

        if (result == true)
        {
            VM.SaveProjectToFile(dialog.FileName);
        }
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (!OkToDiscardAnyChanges())
        {
            e.Cancel = true;
        }
    }

    private bool OkToDiscardAnyChanges()
    {
        if (VM.Project == null)
        {
            return true;
        }

        if (VM.Project.IsDirty)
        {
            YesNo yesNoWindow = new YesNo("Exit?", "Lose unsaved changes?");
            yesNoWindow.Owner = this;

            yesNoWindow.ShowDialog();

            return yesNoWindow.ResponseIsYes;
        }

        return true;
    }

    #endregion

    #region "Help" menu options

    private void Help_OnClick(object sender, RoutedEventArgs e)
    {
        Help help = new Help { Owner = this };
        help.ShowDialog();
    }

    private void About_OnClick(object sender, RoutedEventArgs e)
    {
        About about = new About {Owner = this};
        about.ShowDialog();
    }

    #endregion

    private void Expander_OnExpanded(object sender, RoutedEventArgs e)
    {
        var expander = sender as Expander;

        expander.Header = "Hide\nDetails";
    }

    private void Expander_OnCollapsed(object sender, RoutedEventArgs e)
    {
        var expander = sender as Expander;

        expander.Header = "Show\nDetails";
    }

    private void ApplySortingToControls()
    {
        sortedDataTypes.Items.SortDescriptions
            .Add(new SortDescription("ContainingNamespace", ListSortDirection.Ascending));
        sortedDataTypes.Items.SortDescriptions
            .Add(new SortDescription("Name", ListSortDirection.Ascending));
    }
}