using MetadataRemover.WinFormsApp.Properties;
using MetadataRemover.WinFormsApp.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetadataRemover.WinFormsApp.Forms
{
    public partial class MainForm : Form, IViewFor<MainViewModel>
    {
        public MainForm()
        {
            InitializeComponent();

            this.WhenActivated(block =>
            {
                var configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                if (!File.Exists(configPath))
                {
                    //Existing user config does not exist, so load settings from previous assembly
                    Settings.Default.Upgrade();
                    Settings.Default.Reload();
                    Settings.Default.Save();
                }

                ViewModel.SafeAction = (action) =>
                {
                    if (this.IsDisposed)
                        return;

                    if (this.InvokeRequired)
                    {
                        this.Invoke(action);
                    }
                    else
                    {
                        action?.Invoke();
                    }
                };

                this.OneWayBind(ViewModel, vm => vm.AppTitle, view => view.Text);
                //this.OneWayBind(ViewModel, vm => vm.AppIcon, view => view.Icon);


                this.FormClosing += async (_, e) =>
                {
                    //var result = await ViewModel.IsSavingSettingsAsync();
                   // if (result == true)
                    //{
                        Settings.Default.Save();
                    //}
                    //else if (result == null)
                    //{
                        e.Cancel = false;
                    //}

                    await Task.CompletedTask;
                };

            });

            ViewModel = new MainViewModel();
        }

        public MainViewModel ViewModel { get; set; }
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel)ViewModel;
        }
    }
}
