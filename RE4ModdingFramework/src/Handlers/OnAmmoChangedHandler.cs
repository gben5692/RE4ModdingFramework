using RE4ModdingFramework.src.Events;
using RE4ModdingFramework.src.Logging;

namespace RE4ModdingFramework.src.Handlers
{
    internal class OnAmmoChangedHandler
    {
        private static int lastAmmo = 0;
        private static bool isFirstRun = true;
        private static bool write = false;
        private static OnAmmoChangedEventArgs? ammoChangedEv;

        public static void OnAmmoChanged(int ammo)
        {
            ammoChangedEv = new OnAmmoChangedEventArgs(ammo);
            Player.InvokeAmmoChanged(ammoChangedEv);

            if (ammoChangedEv.Ammo != lastAmmo)
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
                Log.Warning("Not attached to RE4");
                return;
            }

            // BUG: This pointer does not work at the moment :(
            var baseAddress = Memory.GetModuleBase("re4.exe") + 0x0D66E1A8;
            var ammoAdress = Memory.ResolvePointer(baseAddress, 0x88, 0x18, 0xA0, 0x170, 0x28, 0x84);
            //---------------------------------------------------------------------------------------

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

            if (write)
            {
                Memory.Write<int>(ammoAdress, ammoChangedEv!.Ammo);
                lastAmmo = ammoChangedEv.Ammo;
                write = false;
            }

        }
    }
}
