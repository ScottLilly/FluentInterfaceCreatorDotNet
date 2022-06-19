using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models;
using FluentInterfaceCreator.ViewModels;
using FluentInterfaceCreator.WPF.Resources;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace FluentInterfaceCreator.WPF
{
    public partial class MainWindow : Window
    {
        private const string FILE_NAME_EXTENSION = ".ficp";

        private ProjectEditor VM =>
            DataContext as ProjectEditor;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ProjectEditor();
        }

        #region "File" menu options

        private void CreateNewProject_OnClick(object sender, RoutedEventArgs e)
        {
            string selectedLanguage = "C#"; // Currently, only support C#
            OutputLanguage outputLanguage = 
                VM.OutputLanguages.First(ol => ol.Name == selectedLanguage);

            VM.StartNewProject(outputLanguage);
        }

        private void LoadProject_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog =
                new OpenFileDialog
                {
                    DefaultExt = FILE_NAME_EXTENSION,
                    Filter = "Fluent Interface Creator projects (*.ficp)|*.ficp"
                };

            bool? result = dialog.ShowDialog(this);

            if (result == true)
            {
                VM.LoadProjectFromXML(File.ReadAllText(dialog.FileName));
            }
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Add Project.IsDirty and warn if there are unsaved changes
            Close();
        }

        #endregion

        #region "Help" menu options

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            About about = new About {Owner = this};

            about.ShowDialog();
        }

        #endregion

        #region Workspace button click handlers

        private void AddNewDataType_OnClick(object sender, RoutedEventArgs e)
        {
            VM.AddNewDataType();
        }

        private void DeleteDataType_OnClick(object sender, RoutedEventArgs e)
        {
            DataType selectedDataType = ((FrameworkElement)sender).DataContext as DataType;

            VM.DeleteDataType(selectedDataType);
        }

        private void AddMethodParameter_OnClick(object sender, RoutedEventArgs e)
        {
            VM.AddParameterToMethod();
        }

        private void DeleteMethodParameter_OnClick(object sender, RoutedEventArgs e)
        {
            Parameter selectedParameter = ((FrameworkElement)sender).DataContext as Parameter;

            VM.DeleteParameterFromMethod(selectedParameter);
        }

        private void AddMethod_OnClick(object sender, RoutedEventArgs e)
        {
            VM.AddNewMethod();
        }

        private void DeleteMethod_OnClick(object sender, RoutedEventArgs e)
        {
            Method selectedMethod = ((FrameworkElement)sender).DataContext as Method;

            VM.DeleteMethod(selectedMethod);
        }

        private void SelectMethodsCallableNext_OnClick(object sender, RoutedEventArgs e)
        {
            Method selectedMethod = ((FrameworkElement)sender).DataContext as Method;

            VM.SelectMethodsCallableNextFor(selectedMethod);
        }

        private void MethodCallableNext_OnChecked(object sender, RoutedEventArgs e)
        {
            VM.RefreshInterfaces();
        }

        private void SelectInterfaceToName_OnClick(object sender, RoutedEventArgs e)
        {
            InterfaceData selectedInterface = ((FrameworkElement)sender).DataContext as InterfaceData;

            VM.SelectedInterface = selectedInterface;
        }

        private void SaveInterfaceName_OnClick(object sender, RoutedEventArgs e)
        {
            VM.UpdateSelectedInterfaceName();
        }

        #endregion

        private void SaveFluentInterfaceInSingleFile_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog =
                new FolderBrowserDialog
                {
                    Description = Literals.SaveFluentInterfaceInSingleFile,
                    ShowNewFolderButton = true
                };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FluentInterfaceFile fluentInterfaceFile =
                    VM.FluentInterfaceInSingleFile();

                File.WriteAllText(Path.Combine(dialog.SelectedPath, fluentInterfaceFile.Name), fluentInterfaceFile.FormattedText());
            }
        }

        private void SaveFluentInterfaceInMultipleFiles_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog =
                new FolderBrowserDialog
                {
                    Description = Literals.SaveFluentInterfaceInMultipleFiles,
                    ShowNewFolderButton = true
                };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IEnumerable<FluentInterfaceFile> fluentInterfaceFiles =
                    VM.FluentInterfaceInMultipleFiles();

                foreach (FluentInterfaceFile file in fluentInterfaceFiles)
                {
                    File.WriteAllText(Path.Combine(dialog.SelectedPath, file.Name), file.FormattedText());
                }
            }
        }

        private void SaveProject_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog =
                new SaveFileDialog
                {
                    FileName = VM.CurrentProject.Name,
                    DefaultExt = FILE_NAME_EXTENSION,
                    Filter = "Fluent Interface Creator projects (*.ficp)|*.ficp"
                };

            bool? result = dialog.ShowDialog(this);

            if (result == true)
            {
                File.WriteAllText(dialog.FileName,
                                  Serialization.Serialize(VM.CurrentProject));

                // TODO: Reset Project.IsDirty flag
            }
        }
    }
}