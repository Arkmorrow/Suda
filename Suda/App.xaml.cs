using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Suda
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Add global exception handlers
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
            try
            {
                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Startup Error: {ex.Message}\n\nDetails: {ex}", "Suda Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            System.Windows.MessageBox.Show($"Unhandled UI Exception: {e.Exception.Message}\n\nDetails: {e.Exception}", "Suda Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            System.Windows.MessageBox.Show($"Unhandled Exception: {ex?.Message}\n\nDetails: {ex}", "Suda Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
