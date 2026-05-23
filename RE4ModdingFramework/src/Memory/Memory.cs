using RE4ModdingFramework.src.Logging;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RE4ModdingFramework.src
{
    public static class Memory
    {
        [DllImport("Kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBufferm, int nSize, out int lpNumberOfBytesRead);
       
        [DllImport("Kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

        private static Process? process;

        public static bool Attach(string processName)
        {
            process = Process.GetProcessesByName(processName).FirstOrDefault();

            if (process != null)
            {
                Log.Info($"Attached to process: {processName} and process ID: {process.Id}");
                return true;
            }
            else
            {
                Log.Error("The process was not found");
                return false;
            }
        }

        public static bool IsAttached()
        {
            return process != null && !process.HasExited;
        }

        public static IntPtr GetModuleBase(string moduleName)
        {
            if (process == null)
            {
                Log.Warning("Not attached to any procces.");
                return IntPtr.Zero;
            }

            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName!.Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    return module.BaseAddress;
                }
            }

            Log.Error($"Module not found: {moduleName}");
            return IntPtr.Zero;
        }

        public static T Read<T>(IntPtr address) where T : struct
        {
            if (!IsAttached())
            {
                Log.Error("Not attached");
                return default;
            }

            var size = Marshal.SizeOf<T>();
            var buffer = new byte[size];
            ReadProcessMemory(process.Handle, address, buffer, size, out _);
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var result = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();
            return result;
        } 

        public static T Write<T>(IntPtr address, T value) where T : struct
        {
            if (!IsAttached())
            {
                Log.Error("Not attached");
                return default;
            }

            var size = Marshal.SizeOf<T>();
            var buffer = new byte[size];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
            WriteProcessMemory(process.Handle, address, buffer, size, out _);
            handle.Free();
            return value;
        }

        public static IntPtr ResolvePointer(IntPtr baseAdress, params int[] offsets)
        {
            if (!IsAttached())
            {
                Log.Error("Not Attached");
                return IntPtr.Zero;
            }

            IntPtr currentAddress = Read<IntPtr>(baseAdress);

            for (int i = 0; i < offsets.Length - 1; i++)
            {
                currentAddress = Read<IntPtr>(currentAddress + offsets[i]);

                if (currentAddress == IntPtr.Zero)
                {
                    Log.Error($"Null pointer at offset: {i}: 0x{offsets[i]}");
                    return IntPtr.Zero;
                }
            }

            return currentAddress + offsets[offsets.Length - 1];
        }

    }
}
