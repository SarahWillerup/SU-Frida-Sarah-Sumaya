using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaStates;


namespace Galaga_Exercise_3 {
    public class StateMachine : IGameEventProcessor<object> {
        public GameStateType gamestatetype;
        private IGameEventProcessor<object> gameEventProcessorImplementation;
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu.GetInstance();
        }


        private void SwitchState (GameStateType stateType) {
            switch (stateType) {
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstanc();
                break;
            default:
                break;
            
               
            
            
            }

        }

        private void Types(GameEventType eventType, GameEvent<object> gameEvent) {
            ....
        }

    }
}