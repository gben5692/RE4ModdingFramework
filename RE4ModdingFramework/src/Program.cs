using RE4ModdingFramework.src.Events;
using RE4ModdingFramework.src.Logging;
using RE4ModdingFramework.src.Enums;
using RE4ModdingFramework.src.Handlers;

namespace RE4ModdingFramework.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Memory.Attach("re4");

            Player.AmmoChanged += OnAmmoChanged;

            while (Memory.IsAttached())
            {
                OnAmmoChangedHandler.Poll();
                Thread.Sleep(50);
            }
        }

        private static void OnAmmoChanged(OnAmmoChangedEventArgs ev)
        {
            Log.Info($"Ammo has changed and now it is: {ev.Ammo}");
        }
    }
}
