using RE4ModdingFramework.src.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src
{
    public class Loader
    {
        private readonly static string? pluginsDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");


        public static bool PluginsDirectoryExists()
        {
            return pluginsDirectoryPath != null && Directory.Exists(pluginsDirectoryPath);
        }

        public static void LoadPlugins()
        {
            var dllFiles = Directory.GetFiles(pluginsDirectoryPath!, "*.dll");

            foreach (var dllFile in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dllFile);

                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.IsSubclassOf(typeof(Plugin)))
                        {
                            var plugin = (Plugin)Activator.CreateInstance(type)!;
                            plugin.OnEnabled();
                            Log.Info($"Loaded {plugin.Name} by {plugin.Author} v{plugin.Version} ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Failed to load plugin: {Path.GetFileName(dllFile)}. Error: {ex.Message}");
                }
            }
        }   

        public static void CreatePluginsDirectory()
        {

            if (pluginsDirectoryPath == null || !PluginsDirectoryExists())
            {
                Log.Info("Plugins directory not found. Creating...");
                Directory.CreateDirectory(pluginsDirectoryPath!);
            }
            else if (PluginsDirectoryExists() && Memory.IsAttached())
            {
                Log.Info("Plugins directory found. Loading plugins...");
                LoadPlugins();
            }
            else
            {
                Log.Info("Plugins directory was found but the game is not running, please make sure the game is running before launching the program!");
            }
        }
    }
}
