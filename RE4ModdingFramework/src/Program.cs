using RE4ModdingFramework.src.Handlers;

namespace RE4ModdingFramework.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Memory.Attach("re4");
            Loader.CreatePluginsDirectory();

            while (Memory.IsAttached())
            {
                OnAmmoChangedHandler.Pull();
                OnHealthChangedHandler.Pull();
                OnPtasChangedHandler.Pull();
                Thread.Sleep(50);
            }

            Console.ReadLine();
        }
    }
}
