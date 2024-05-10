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


                this.Bind(ViewModel, vm => vm.SelectedFile, view => view.textBox1.Text);

                this.BindCommand(ViewModel, vm => vm.SelectFileCommand, view => view.button1);
                this.BindCommand(ViewModel, vm => vm.OpenFileCommand, view => view.button2);

                this.BindCommand(ViewModel, vm => vm.ReadMetadataCommand, view => view.buttonReadMetadata);
                //this.BindCommand(ViewModel, vm => vm.RemoveMetadataCommand, view => view.buttonRemoveMetadata);

                this.Bind(ViewModel, vm => vm.MetadataList, view => view.textBoxMetadata.Text);
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
