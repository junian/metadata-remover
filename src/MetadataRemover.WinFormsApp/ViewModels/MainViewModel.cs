using Juniansoft.MvvmReady;
using MetadataRemover.WinFormsApp.Properties;
using MetadataRemover.WinFormsApp.Services;
using ReactiveUI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MetadataRemover.WinFormsApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly AssemblyService _assembly;
        private readonly DialogService _dialogService;
        private readonly ILogger _log;

        public Action<Action> SafeAction { get; set; }

        public MainViewModel(
            AssemblyService assembly = null, 
            DialogService dialogService = null, 
            ILogger log = null)
        {
            _assembly = ServiceLocator.Current.Get<AssemblyService>();
            _dialogService = ServiceLocator.Current.Get<DialogService>();
            _log = ServiceLocator.Current.Get<ILogger>();

            AppTitle = $"{_assembly.AssemblyProduct} - v{_assembly.AssemblyVersion}";
            //AppIcon = Resources.Favicon;

            SelectFileCommand = ReactiveCommand.CreateFromTask(SelectFileAsync);
            OpenFileCommand = ReactiveCommand.CreateFromTask(() => OpenFileAsync(SelectedFile));
            ReadMetadataCommand = ReactiveCommand.CreateFromTask(ReadMetadataAsync);
            RemoveMetadataCommand = ReactiveCommand.CreateFromTask(RemoveMetadataAsync);
        }

        private string _appTitle;
        public string AppTitle
        {
            get => _appTitle;
            set => this.RaiseAndSetIfChanged(ref _appTitle, value);
        }

        public ICommand OpenFileCommand { get; private set; }
        private async Task OpenFileAsync(string path)
        {
            try
            {
                Process.Start($"start \"{path}\"");
            }
            catch(Exception ex)
            {
                _log.Error(ex.ToString());
            }

            await Task.CompletedTask;
        }

        public string SelectedFile
        {
            get => Settings.Default.SelectedFile;
            set
            {
                Settings.Default.SelectedFile = value;
                this.RaisePropertyChanged();
            }
        }
        public ICommand SelectFileCommand { get; private set; }
        private async Task SelectFileAsync()
        {
            var selectedPath = await _dialogService.ShowFileBrowserAsync();
            if (!string.IsNullOrWhiteSpace(selectedPath))
            {
                SelectedFile = selectedPath;
            }
        }

        private string _metadataList = "";
        public string MetadataList
        {
            get => _metadataList;
            set => this.RaiseAndSetIfChanged(ref _metadataList, value);
        }

        public ICommand ReadMetadataCommand { get;private set; }
        private async Task ReadMetadataAsync()
        {
            var result = new StringBuilder();
            var dict = new Dictionary<string, string>();
            using(var exif = new ExifTool())
            {
                exif.GetProperties(SelectedFile, dict);
            }
            foreach(var kv in dict)
            {
                result.AppendLine($"{kv.Key}: {kv.Value}");
            }
            MetadataList = result.ToString();
            await Task.CompletedTask;
        }

        public ICommand RemoveMetadataCommand { get; private set; }
        public async Task RemoveMetadataAsync()
        {
            using (var exif = new ExifTool())
            {
                exif.RemoveAllProperties(SelectedFile);
            }
            await Task.CompletedTask; 
        }
    }
}
