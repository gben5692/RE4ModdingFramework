using RE4ModdingFramework.src.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    /// <summary>
    /// The base class of player actions
    /// </summary>
    public class Player
    {
        /// <summary>
        /// HealthChanged event is called when the player takes damage or heals
        /// </summary>
        public static event EventHandler<OnHealthChangedEventArgs>? HealthChanged;
        internal static void InvokePlayerHealthChanged(OnHealthChangedEventArgs ev) => HealthChanged?.Invoke(ev);

        /// <summary>
        /// AmmoChanged event is called when the player shoots or reloads
        /// </summary>
        internal static event EventHandler<OnAmmoChangedEventArgs>?  AmmoChanged; // Disabled because of the pointer not working :(
        internal static void InvokeAmmoChanged(OnAmmoChangedEventArgs ev) => AmmoChanged?.Invoke(ev);

        /// <summary>
        /// PtasChanged event is called when the player receives or spends ptas
        /// </summary>
        public static event EventHandler<OnPtasChangedEventArgs>? PtasChanged;
        internal static void InvokePtasChanged(OnPtasChangedEventArgs ev) => PtasChanged?.Invoke(ev);

    }
}
