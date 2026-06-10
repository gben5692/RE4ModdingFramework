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

        /// <summary>
        /// The damage the player recevied
        /// </summary>
        public int Damaged { get; }

        /// <summary>
        /// The health the player recevied
        /// </summary>
        public int Healed { get; }

        internal OnHealthChangedEventArgs(int health, int damaged, int healed) 
        {
            Health = health;
            Damaged = damaged;
            Healed = healed;
        }
    }
}
