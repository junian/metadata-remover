using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MetadataRemover.WinFormsApp.Services
{
    public class AssemblyService
    {
        private static readonly AssemblyService _current = new AssemblyService();

        public static AssemblyService Current => _current;

        private readonly Assembly _assembly;
        public AssemblyService(Assembly assembly = null)
        {
            _assembly = Assembly.GetExecutingAssembly();
            if (assembly != null)
                _assembly = assembly;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get => GetStringAttribute<AssemblyTitleAttribute>(
                x => x?.Title,
                System.IO.Path.GetFileNameWithoutExtension(_assembly.CodeBase));
        }

        public Version AssemblyFullVersion
        {
            get => _assembly.GetName().Version;
        }

        public string AssemblyVersion
        {
            get => _assembly.GetName().Version.ToString(3);
        }

        public string AssemblyDescription
        {
            get => GetStringAttribute<AssemblyDescriptionAttribute>(x => x?.Description);
        }

        public string AssemblyProduct
        {
            get => GetStringAttribute<AssemblyProductAttribute>(x => x?.Product);
        }

        public string AssemblyCopyright
        {
            get => GetStringAttribute<AssemblyCopyrightAttribute>(x => x?.Copyright);
        }

        public string AssemblyCompany
        {
            get => GetStringAttribute<AssemblyCompanyAttribute>(x => x?.Company);
        }

        public string AssemblyGuid
        {
            get => GetStringAttribute<GuidAttribute>(x => x?.Value);
        }

        public string AssemblyBuildConfiguration
        {
            get => GetStringAttribute<AssemblyConfigurationAttribute>(x => x?.Configuration);
        }

        private string GetStringAttribute<T>(Func<T, string> getStringFunc, string defaultValue = "")
        {
            var attr = (T)_assembly.GetCustomAttributes(typeof(T), false)?.FirstOrDefault();
            return getStringFunc?.Invoke(attr) ?? defaultValue;
        }

        #endregion
    }
}
