using System.Collections.Generic;
using System.Windows;
using FluentInterfaceCreator.ViewModels;

namespace FluentInterfaceCreator.Windows
{
    public partial class OK : Window
    {
        public OK(string title, string message)
        {
            InitializeComponent();

            DataContext = new OKViewModel(title, message);
        }

        public OK(string title, List<string> message)
        {
            InitializeComponent();

            DataContext = new OK(title, message);
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}