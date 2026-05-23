using RE4ModdingFramework.src.Events;
using RE4ModdingFramework.src.Logging;

namespace RE4ModdingFramework.src.Handlers
{
    public class OnAmmoChangedHandler
    {
        private static int lastAmmo = 0;
        private static bool isFirstRun = true;

        public static void OnAmmoChanged(int ammo)
        {
            var ammoChangedEv = new OnAmmoChangedEventArgs(ammo);
            Player.InvokeAmmoChanged(ammoChangedEv);
        }

        public static void Poll()
        {

            if (!Memory.IsAttached())
            {
                Log.Warning("Not attached to RE4");
                return;
            }

            var baseAddress = Memory.GetModuleBase("re4.exe") + 0x0D68AEB8;
            var ammoAdress = Memory.ResolvePointer(baseAddress, 0x30, 0x60, 0x84);

            if (ammoAdress == IntPtr.Zero)
            {
                Log.Error("Failed to resolve ammo address");
                return;
            }

            var currentAmmo = Memory.Read<int>(ammoAdress);

            if (isFirstRun)
            {
                lastAmmo = currentAmmo;
                isFirstRun = false;
            }

            if (currentAmmo != lastAmmo)
            {
                lastAmmo = currentAmmo;
                OnAmmoChanged(currentAmmo);
            }

        }
    }
}
