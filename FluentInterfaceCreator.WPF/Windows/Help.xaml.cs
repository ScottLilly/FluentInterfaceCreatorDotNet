﻿using System.Windows;

namespace FluentInterfaceCreator.Windows
{
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}