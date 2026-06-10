using RE4ModdingFramework.src.Events;
using RE4ModdingFramework.src.Logging;

namespace RE4ModdingFramework.src.Handlers
{
    internal class OnPtasChangedHandler
    {
        private static int lastPtas = 0;

        private static bool write = false;

        private static bool isFirstRun = true;

        private static OnPtasChangedEventArgs? ptasChangedEv;

        public static void OnPtasChanged(int ptas, int ptasSpent, int ptasRecieved)
        {
            ptasChangedEv = new OnPtasChangedEventArgs(ptas, ptasSpent, ptasRecieved);
            Player.InvokePtasChanged(ptasChangedEv);

            if (ptasChangedEv.Ptas != lastPtas)
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

            var baseAddress = Memory.GetModuleBase("re4.exe") + 0x0D66E1A8;
            var ptasAddress = Memory.ResolvePointer(baseAddress, 0x10);

            if (ptasAddress == IntPtr.Zero)
            {
                Log.Error("Failed to resolve ptas address");
                return;
            }
            else
            {
#if DEBUG
                Log.Debug($"Ptas address: 0x{ptasAddress}");
#endif
            }

            var currentPtas = Memory.Read<int>(ptasAddress);

            if (isFirstRun)
            {
                lastPtas = currentPtas;
                isFirstRun = false;
            }

            var spent = 0;
            var recieved = 0;

            if (currentPtas != lastPtas)
            {
                if (currentPtas < lastPtas)
                {
                    // Money Spent
                    spent = lastPtas - currentPtas;
                }
                else
                {
                    // Money Recieved
                    recieved = currentPtas - lastPtas;
                }

                lastPtas = currentPtas;
                OnPtasChanged(currentPtas, spent, recieved);

            }

            if (write)
            {
                Memory.Write<int>(ptasAddress, ptasChangedEv!.Ptas);
                lastPtas = ptasChangedEv.Ptas;
                write = false;
            }
        }
    }
}
