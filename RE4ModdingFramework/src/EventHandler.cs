using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src
{
    public delegate void EventHandler(EventArgs ev);
    public delegate void EventHandler<T>(T ev);

}
