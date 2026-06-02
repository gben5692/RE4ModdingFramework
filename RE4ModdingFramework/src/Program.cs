using RE4ModdingFramework.src.Events;
using RE4ModdingFramework.src.Logging;
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
                OnAmmoChangedHandler.Poll();
                OnHealthChangedHandler.Poll();
                Thread.Sleep(50);
            }

            Console.ReadLine();
        }
    }
}
