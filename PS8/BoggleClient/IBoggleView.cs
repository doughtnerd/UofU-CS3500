using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BoggleClient
{
    public interface IBoggleView
    {
        /// <summary>
        /// Notifies subscribers of the word played.
        /// </summary>
        event Action<string> PlayWordEvent;

        /// <summary>
        /// Notifies subscribers of the selected domain and the nickname chosen.
        /// </summary>
        event Action<string, string> RegisterEvent;

        /// <summary>
        /// Notifies subscribers of the entered game length.
        /// </summary>
        event Action<long> JoinGameEvent;

        /// <summary>
        /// Notifies that a cancel join event has fired.
        /// </summary>
        event Action CancelJoinEvent;

        /// <summary>
        /// Notifies the user wants to exit the game
        /// </summary>
        event Action ExitGameEvent;

        /// <summary>
        /// Sets the join button text so it can decide which event to fire.
        /// </summary>
        /// <param name="s"></param>
        void SetJoinButtonText(string s);

        /// <summary>
        /// Sets the words in the listboxes for each player.
        /// </summary>
        void SetWords(string[] playerOne, string[] playerTwo);

        /// <summary>
        /// Sets the score text for each player.
        /// </summary>
        void SetScores(int playerOne, int playerTwo);

        /// <summary>
        /// Sets up the gameboard to use the given string for its pieces.
        /// </summary>
        void SetGameBoard(string s);

        /// <summary>
        /// Sets the domain of the view.
        /// </summary>
        void SetDomain(string s);

        /// <summary>
        /// Sets the current game status
        /// </summary>
        void SetGameStatus(string s);

        /// <summary>
        /// Sets the time left to the given string.
        /// </summary>
        /// <param name="s"></param>
        void SetTimeLeft(string s);

        /// <summary>
        /// Whether or not the join game button is active.
        /// </summary>
        void SetJoinGameActive(bool enabled);

        /// <summary>
        /// Whether or not the registration button is active.
        /// </summary>
        /// <param name="enabled"></param>
        void SetRegistrationActive(bool enabled);

        /// <summary>
        /// Whether or not the main menu is enabled
        /// </summary>
        void SetMainMenuEnabled(bool enabled);

        /// <summary>
        /// Whether or not the game view is enabled.
        /// </summary>
        /// <param name="enabled"></param>
        void SetGameViewEnabled(bool enabled);
    }
}
