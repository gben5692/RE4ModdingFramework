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

        public static void OnHealthChanged(int health, int damaged, int healed)
        {
            healthChangedEv = new OnHealthChangedEventArgs(health, damaged, healed);
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

        public static void Pull()
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

            var damaged = 0;
            var healed = 0;

            if (currentHealth != lastHealth)
            {
                if (currentHealth < lastHealth)
                {
                    // Player Damaged
                    damaged = lastHealth - currentHealth;
                }
                else
                {
                    // Player Healed
                    healed = currentHealth - lastHealth;
                }

                lastHealth = currentHealth;
                OnHealthChanged(currentHealth, damaged, healed);
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
