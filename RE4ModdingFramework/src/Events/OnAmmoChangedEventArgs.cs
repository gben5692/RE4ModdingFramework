using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    public class OnAmmoChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The ammo that has been consumed
        /// </summary>
        public int Ammo { get; }

        internal OnAmmoChangedEventArgs(int ammo)
        {
            Ammo = ammo;
        }
    }
}
