using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    /// <summary>
    /// The event args for the ammo changed event
    /// </summary>
    public class OnAmmoChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The ammo that has been changed
        /// </summary>
        public int Ammo { get; set; }

        internal OnAmmoChangedEventArgs(int ammo)
        {
            Ammo = ammo;
        }
    }
}
