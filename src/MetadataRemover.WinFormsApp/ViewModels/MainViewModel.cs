using Juniansoft.MvvmReady;
using MetadataRemover.WinFormsApp.Services;
using ReactiveUI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

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


        }

        private string _appTitle;
        public string AppTitle
        {
            get => _appTitle;
            set => this.RaiseAndSetIfChanged(ref _appTitle, value);
        }
    }
}
