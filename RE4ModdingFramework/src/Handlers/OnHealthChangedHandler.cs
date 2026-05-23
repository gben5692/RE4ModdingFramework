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
            }

            var baseAddress = Memory.GetModuleBase("re4.exe") + 0x0D66E7A0;
            var healthAddress = Memory.ResolvePointer(baseAddress, 0xA0, 0x40, 0x148, 0x14);

            if (healthAddress == IntPtr.Zero)
            {
                Log.Error("Failed to resolve the pointer for health!");
                return;
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
                Memory.Write<int>(healthAddress, healthChangedEv.Health);
                lastHealth = currentHealth;
                write = false;
            }
        }
    }
}
