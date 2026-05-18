using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    public class Player
    {
        // Add static events for player actions (Testing)
        public static event EventHandler<OnPlayerJoinedEventArgs>? PlayerJoined;
        public static event EventHandler<OnPlayerLeftEventArgs>? PlayerLeft;


        public static event EventHandler<OnPlayerDamagedEventArgs>? PlayerDamaged;

        public Player()
        {
            var damagedEv = new OnPlayerDamagedEventArgs(31, 20);
            PlayerDamaged?.Invoke(damagedEv);
        }
    }
}
