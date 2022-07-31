using System.Collections.Generic;
using System.Windows;
using FluentInterfaceCreator.ViewModels;

namespace FluentInterfaceCreator.Windows
{
    public partial class YesNo : Window
    {
        private YesNoViewModel VM => DataContext as YesNoViewModel;

        public bool ResponseIsYes => VM.ResponseIsYes;

        public YesNo(string title, string question)
        {
            InitializeComponent();

            DataContext = new YesNoViewModel(title, question);
        }

        public YesNo(string title, List<string> question)
        {
            InitializeComponent();

            DataContext = new YesNoViewModel(title, question);
        }

        private void No_OnClick(object sender, RoutedEventArgs e)
        {
            VM.ResponseIsYes = false;

            Close();
        }

        private void Yes_OnClick(object sender, RoutedEventArgs e)
        {
            VM.ResponseIsYes = true;

            Close();
        }
    }
}