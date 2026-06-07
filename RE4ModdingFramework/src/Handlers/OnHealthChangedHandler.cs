using RE4ModdingFramework.src.Events;
using RE4ModdingFramework.src.Logging;


namespace RE4ModdingFramework.src.Handlers
{
    internal class OnHealthChangedHandler
    {
        private static int lastHealth = 0;
        private static bool write = false;
        private static bool isFirstRun = true;
        private static OnHealthChangedEventArgs? healthChangedEv;

        public static void OnHealthChanged(int health)
        {
            healthChangedEv = new OnHealthChangedEventArgs(health);
            Player.InvokePlayerHealthChanged(healthChangedEv);

            if (healthChangedEv.Health != lastHealth)
            {
                write = true;
            }
            else
            {
                write = false;
            }
        }

        public static void Poll()
        {
            if (!Memory.IsAttached())
            {
                Log.Error("Not attached to re4");
                return;
            }

            var baseAddress = Memory.GetModuleBase("re4.exe") + 0x0D66E918;
            var healthAddress = Memory.ResolvePointer(baseAddress, 0x10, 0x40, 0x50, 0x1F4);

            if (healthAddress == IntPtr.Zero)
            {
                // This usally prints when you are in the main menu or loading screens
                Log.Error("Failed to resolve the pointer for health!");
                return;
            }
            else
            {
#if DEBUG
                Log.Debug($"Health address: 0x{healthAddress}");
#endif
            }

            var currentHealth = Memory.Read<int>(healthAddress);

            if (isFirstRun)
            {
                lastHealth = currentHealth;
                isFirstRun = false;
            }

            if (currentHealth != lastHealth)
            {
                lastHealth = currentHealth;
                OnHealthChanged(currentHealth);
            }

            if (write)
            {
                Memory.Write<int>(healthAddress, healthChangedEv!.Health);
                lastHealth = healthChangedEv.Health;
                write = false;
            }
        }
    }
}
