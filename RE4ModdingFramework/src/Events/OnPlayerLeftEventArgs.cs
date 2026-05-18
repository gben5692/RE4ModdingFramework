using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    public class OnPlayerLeftEventArgs : EventArgs
    {
        public string PlayerName { get; } = "NoPlayerNameFound";

        public OnPlayerLeftEventArgs(string playerName) 
        {
            PlayerName = playerName;
        }
    }
}
