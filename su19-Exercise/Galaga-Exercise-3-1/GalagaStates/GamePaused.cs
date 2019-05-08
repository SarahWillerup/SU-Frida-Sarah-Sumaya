using DIKUArcade.State;

namespace Galaga_Exercise_3_1.GalagaStates {
    public class GamePaused : IGameState {
        public static GamePaused instance = null;

        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }

        public void GameLoop() {}

        public void InitializeGameState() {}

        public void UpdateGameLogic() {}

        public void RenderState() {}

        public void HandleKeyEvent(string keyValue, string keyAction) {}
    }
}