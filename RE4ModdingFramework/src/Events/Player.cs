using RE4ModdingFramework.src.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    public class Player
    {
        public static event EventHandler<OnPlayerDamagedEventArgs>? PlayerDamaged;
        internal static void InvokePlayerDamaged(OnPlayerDamagedEventArgs ev) => PlayerDamaged?.Invoke(ev);


        public static event EventHandler<OnAmmoChangedEventArgs>? AmmoChanged;
        internal static void InvokeAmmoChanged(OnAmmoChangedEventArgs ev) => AmmoChanged?.Invoke(ev);

    }
}
