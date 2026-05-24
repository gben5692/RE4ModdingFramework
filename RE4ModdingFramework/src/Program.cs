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

            Player.AmmoChanged += OnAmmoChanged;
            Player.HealthChanged += OnHealthChanged;

            while (Memory.IsAttached())
            {
                OnAmmoChangedHandler.Poll();
                OnHealthChangedHandler.Poll();
                Thread.Sleep(50);
            }
        }

        private static void OnHealthChanged(OnHealthChangedEventArgs ev)
        {
            Log.Info($"Health has changed and now it is: {ev.Health}");

            ev.Health += 50;

            if (ev.Health == 0)
            {
                Log.Info("Player is dead");
            }
        }

        private static int ammoCount = 0;
        private static void OnAmmoChanged(OnAmmoChangedEventArgs ev)
        {
            ammoCount++;

            if (ammoCount == 5)
            {
                ev.Ammo += 10;
                ammoCount = 0;
            }

            if (ev.Ammo == 0)
            {
                Log.Info("Player is out of ammo");
            }
        }
    }
}
