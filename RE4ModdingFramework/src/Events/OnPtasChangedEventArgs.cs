using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Events
{
    public class OnPtasChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The money that has been changed
        /// </summary>
        public int Ptas { get; set; }

        /// <summary>
        /// How much money you spent
        /// </summary>
        public int PtasSpent { get; }

        /// <summary>
        /// How much money you received
        /// </summary>
        public int PtasRecieved { get; }

        internal OnPtasChangedEventArgs(int ptas, int ptasSpent, int ptasRecieved)
        {
            Ptas = ptas;
            PtasSpent = ptasSpent;
            PtasRecieved = ptasRecieved;
        }
    }
}
