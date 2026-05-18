using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModingFramework.src.Events
{
    public class OnPlayerDamagedEventArgs : EventArgs
    {
        public float Health { get; set; } = 0;
        public int Damage { get; }

        public OnPlayerDamagedEventArgs(float health, int damage)
        {
            Health = health;
            Damage = damage;
        }
    }
}
