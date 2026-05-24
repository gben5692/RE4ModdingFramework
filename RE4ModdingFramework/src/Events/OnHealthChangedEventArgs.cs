using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    /// <summary>
    /// The event args for the health changed event
    /// </summary>
    public class OnHealthChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The health that has been changed
        /// </summary>
        public int Health { get; set; }

        internal OnHealthChangedEventArgs(int health) 
        {
            Health = health;
        }
    }
}
