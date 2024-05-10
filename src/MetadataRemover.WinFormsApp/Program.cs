using Juniansoft.MvvmReady;
using MetadataRemover.WinFormsApp.Forms;
using MetadataRemover.WinFormsApp.Services;
using Serilog;
using Serilog.Core;
using Splat;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MetadataRemover.WinFormsApp
{
    internal static class Program
    {
        static Mutex mutex = new Mutex(false, $"{AssemblyService.Current.AssemblyCompany}.{AssemblyService.Current.AssemblyProduct}");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // if you like to wait a few seconds in case that the instance is just 
            // shutting down
            if (!mutex.WaitOne(TimeSpan.FromSeconds(2), false))
            {
                MessageBox.Show(
                    "Application already started!",
                    AssemblyService.Current.AssemblyProduct,
                    MessageBoxButtons.OK);
                return;
            }

            try
            {
                ConfigureServices();
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.ThreadException += Application_ThreadException;
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(Program.MainForm);
            }
            finally { mutex.ReleaseMutex(); } // I find this more explicit
        }

        private static void ConfigureServices()
        {
            ServiceLocator.Current.Register<LoggingLevelSwitch>(() => new LoggingLevelSwitch { });
            ServiceLocator.Current.Register<Serilog.ILogger>(
                () => new LoggerConfiguration()
                        .MinimumLevel.ControlledBy(ServiceLocator.Current.Get<LoggingLevelSwitch>())
                        .ReadFrom
                        .AppSettings()
                        .CreateLogger());
            ServiceLocator.Current.Register<AssemblyService>(() => AssemblyService.Current);
            ServiceLocator.Current.Register<DialogService>();
        }

        private static MainForm s_mainForm = null;
        public static MainForm MainForm => s_mainForm ?? (s_mainForm = new MainForm());

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var errorMessage = e.Exception.ToString();

            var log = ServiceLocator.Current.Get<Serilog.ILogger>();
            log.Error(errorMessage);

            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var errorMessage = e.ExceptionObject.ToString();

            var log = ServiceLocator.Current.Get<Serilog.ILogger>();
            log.Error(errorMessage);

            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}