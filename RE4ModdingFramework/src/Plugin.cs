using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src
{
    public abstract class Plugin
    {
        /// <summary>
        /// Name of the plugin.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Author the plugin.
        /// </summary>
        public abstract string Author { get; }

        /// <summary>
        /// Set the version of the plugin.
        /// </summary>
        public abstract Version Version { get; }


        public virtual void OnEnabled()
        {
        }

        public virtual void OnDisabled()
        {
        }

    }
}
