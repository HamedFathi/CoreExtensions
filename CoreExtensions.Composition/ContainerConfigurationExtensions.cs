using CoreUtilities;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace CoreExtensions
{
    public static class ContainerConfigurationExtensions
    {
        private static List<Assembly> ConvertToAssemblies(string[] filesPath)
        {
            var context = new LoadContextUtility();
            var assemblies = new List<Assembly>();
            foreach (var file in filesPath)
            {
                var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                using (stream)
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    var ass = context.LoadFromStream(new MemoryStream(data));

                    var names = assemblies.Select(x => x.FullName);
                    if (!names.Contains(ass.FullName))
                        assemblies.Add(ass);
                }
            }
            return assemblies;
        }

        public static ContainerConfiguration WithAssembliesInFilePath(this ContainerConfiguration configuration, string filePath, AttributedModelProvider conventions, bool customLoadContext = false)
        {
            var assemblies = new List<Assembly>();
            if (customLoadContext)
            {
                assemblies.AddRange(ConvertToAssemblies(new[] { filePath }));
            }
            else
            {
                var ass = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
                var names = assemblies.Select(x => x.FullName);
                if (!names.Contains(ass.FullName))
                    assemblies.Add(ass);
            }
            configuration = configuration.WithAssemblies(assemblies, conventions);
            return configuration;
        }

        public static ContainerConfiguration WithAssembliesInFolderPath(this ContainerConfiguration configuration, string directoryPath, AttributedModelProvider conventions, string searchPattern = "*.dll", SearchOption searchOption = SearchOption.TopDirectoryOnly, bool customLoadContext = false)
        {
            var files = Directory.EnumerateFiles(directoryPath, searchPattern, searchOption);
            var assemblies = new List<Assembly>();
            if (customLoadContext)
            {
                assemblies = ConvertToAssemblies(files.ToArray());
            }
            else
            {
                foreach (var file in files)
                {
                    var ass = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                    var names = assemblies.Select(x => x.FullName);
                    if (!names.Contains(ass.FullName))
                        assemblies.Add(ass);
                }
            }
            configuration = configuration.WithAssemblies(assemblies, conventions);
            return configuration;
        }
    }
}
