using System;

namespace An01malia.FirstPerson.Core
{
    public enum GameState
    {
        Gameplay,
        Paused,
        Inventory
    }

    public class GameStateManager : Singleton<GameStateManager>
    {
        #region Delegates

        public event Action<GameState> OnGameStateChanged = delegate { };

        #endregion

        #region Properties

        public GameState CurrentGameState { get; private set; }

        #endregion

        #region Public Methods

        public void ChangeState(GameState gameState)
        {
            if (gameState == GameState.Gameplay && gameState == CurrentGameState) return;

            GameState newGameState = gameState;

            if (gameState == CurrentGameState)
            {
                newGameState = GameState.Gameplay;
            }

            CurrentGameState = newGameState;
            OnGameStateChanged(newGameState);
        }

        #endregion

        #region Protected Methods

        protected override void Initialize()
        {
        }

        #endregion
    }
}