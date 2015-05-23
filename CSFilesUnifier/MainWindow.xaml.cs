using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace CSFilesUnifier
{
    public partial class MainWindow : Window
    {    
        public MainWindow()
        {         
            InitializeComponent();          
        }

        private void ClickOnBrowseButton(object sender, RoutedEventArgs e)
        {
            // Open folder
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowDialog();

            List<string> str = Files.FindFilesInDirectory(fb.SelectedPath);
            Files.CleanFiles(str);
            Files.Unify();
        }

        private void ClickOnUniflyButton(object sender, RoutedEventArgs e)
        {
            // Opens the finished file on the desktop
            Process.Start(Environment.ExpandEnvironmentVariables(@"%UserProfile%\Desktop\text.cs"));
            Environment.Exit(1);
        }       
    }
}
