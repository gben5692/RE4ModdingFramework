using RE4ModdingFramework.src.Logging;
using System.Reflection;

namespace RE4ModdingFramework.src
{
    internal class Loader
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
                            Log.Info($"Loaded {plugin.Name} by {plugin.Author} v{plugin.Version} ");
                            plugin.OnEnabled();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Failed to load plugin: {Path.GetFileName(dllFile)}. Error: {ex.Message}");
                }
            }
        }   

        public static void UnloadPlugins()
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
                            // Unload Here
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Failed to unload plugin: {Path.GetFileName(dllFile)}. Error: {ex.Message}");
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
