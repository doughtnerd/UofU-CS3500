using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    public interface IBoggleView
    {
        event Action<string> PlayWordEvent;

        /// <summary>
        /// Notifies subscribers of the selected domain and the nickname chosen.
        /// </summary>
        event Action<string, string> RegisterEvent;

        /// <summary>
        /// Notifies subscribers of the entered game length.
        /// </summary>
        event Action<long> JoinGameEvent;

        event Action CancelJoinEvent;

        event Action CancelEvent;

        void EnableControls(bool enabled);
    }
}
