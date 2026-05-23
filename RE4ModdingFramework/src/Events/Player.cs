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
        public static event EventHandler<OnHealthChangedEventArgs>? HealthChanged;
        internal static void InvokePlayerHealthChanged(OnHealthChangedEventArgs ev) => HealthChanged?.Invoke(ev);


        public static event EventHandler<OnAmmoChangedEventArgs>? AmmoChanged;
        internal static void InvokeAmmoChanged(OnAmmoChangedEventArgs ev) => AmmoChanged?.Invoke(ev);

    }
}
