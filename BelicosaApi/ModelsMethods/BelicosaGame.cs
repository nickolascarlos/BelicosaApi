using BelicosaApi.Models;

namespace BelicosaApi.Models
{
    public partial class BelicosaGame
    {
        public Color GetRandomAvailableColor()
        {
            Color ChosenColor = AvailableColors.ElementAt(new Random().Next(AvailableColors.Count));
            AvailableColors.Remove(ChosenColor);
            return ChosenColor;
        }

        public bool ReachedMaximumNumberOfPlayers()
        {
            return AvailableColors.Count <= 0;
        }

        public Player GetCurrentPlayer()
        {
            return Players.ElementAt(CurrentPlayerIndex);
        }

        public Player GetNextPlayer()
        {
            return Players.ElementAt((CurrentPlayerIndex + 1) % Players.Count);
        }

        public void HandleTurnToNextPlayer()
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
        }

        public Player GetCurrentPlayerThenHandleTurn()
        {
            Player currentPlayer = GetCurrentPlayer();
            HandleTurnToNextPlayer();
            return currentPlayer;
        }

        public Player HandleTurnThenGetCurrentPlayer()
        {
            HandleTurnToNextPlayer();
            Player currentPlayer = GetCurrentPlayer();
            return currentPlayer;
        }
    }
}
