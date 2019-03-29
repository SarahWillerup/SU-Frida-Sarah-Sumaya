using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaStates;


namespace Galaga_Exercise_3 {
    public class StateMachine : IGameEventProcessor<object> {
        public MainMenu mainmenu;
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            
        }
        private void SwitchState(GameStateType.Enum_GameStateType stateType) {
            switch (stateType) {
            case GameStateType.Enum_GameStateType.MainMenu:
                GameRunning.GetInstance().InitializeGameState();
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.Enum_GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.Enum_GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {  
                case "KEY_PAUSE":
                    if (eventType == (GameEventType) GameStateType.Enum_GameStateType.GamePaused) {
                        SwitchState(GameStateType.Enum_GameStateType.GameRunning);
                    } else {
                        SwitchState(GameStateType.Enum_GameStateType.GamePaused);
                    }
                    break;
                case "KEY_MENU":
                    SwitchState(GameStateType.Enum_GameStateType.MainMenu);
                    break;
                }
            }
        }
    }
}