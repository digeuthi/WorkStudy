using System.Configuration;
using System.Data;
using System.Windows;
using System.IO.Ports;
using System.Collections.Concurrent;
using System;
using WpfApp_mvvm.Views;

namespace WpfApp_mvvm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            var mainWindow = new MainWindow();
            mainWindow.DataContext = new ViewModels.ViewModel();
            mainWindow.Show();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
        }


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }


    }
}


