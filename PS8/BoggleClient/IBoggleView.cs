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

        void BuildMainMenu();

        void HideMainMenu();

        void BuildGame();

        void HideGame();

        void BuildEndGame();

        void HideEndGame();

        void startGameData(dynamic data, int playerNumber);

        void updateGameData(dynamic data, int playerNumber);

        void endGameData(dynamic data, int playerNumber);
    }
}
