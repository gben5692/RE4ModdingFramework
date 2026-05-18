using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    public class OnPlayerJoinedEventArgs : EventArgs
    {
        public string PlayerName { get; } = "NoPlayerNameFound";

        public OnPlayerJoinedEventArgs(string playerName) 
        {
            PlayerName = playerName;
        }
    }
}
