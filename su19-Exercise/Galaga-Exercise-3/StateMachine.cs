using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;



namespace Galaga_Exercise_3 {
    public class StateMachine : IGameEventProcessor<object> {
        public GameStateType gamestatetype;
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu.GetInstance();
        }


        private void SwitchState (GameStateType stateType) {
            switch (stateType) {
            case :
                
               
            
            
            }

        }

        private void Types(GameEventType eventType, GameEvent<object> gameEvent) {
            ....
        }

    }
}