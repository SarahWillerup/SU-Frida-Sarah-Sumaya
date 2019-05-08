using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3_1.GalagaStates;


namespace Galaga_Exercise_3_1 {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
        }
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
            case GameStateType.MainMenu:
                GameRunning.GetInstance().InitializeGameState();
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
              }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                case "KEY_PAUSE":
                    if (eventType == (GameEventType) GameStateType.GamePaused) {
                        SwitchState(GameStateType.GameRunning);
                    } else {
                        SwitchState(GameStateType.GamePaused);    
                    }
                    break;
                case "KEY_MENU":
                    SwitchState(GameStateType.MainMenu);
                    break;
                }
            } else if (eventType == GameEventType.InputEvent) {
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            }
        }
    }
}