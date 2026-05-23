using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    public class OnAmmoChangedEventArgs : EventArgs
    {
        public int Ammo { get; set; }

        internal OnAmmoChangedEventArgs(int ammo)
        {
            Ammo = ammo;
        }
    }
}
