using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    public class OnHealthChangedEventArgs : EventArgs
    {
        public int Health { get; set; }

        public OnHealthChangedEventArgs(int health) 
        {
            Health = health;
        }
    }
}
