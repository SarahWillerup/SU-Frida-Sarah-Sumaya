using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3;


namespace GalagaGame.GalagaState {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu.GetInstance();
        }


        private void SwitchState (GameStateType stateType) {
            switch (stateType) {
            case StateTransformer.TransformStringToState("GAME_RUNNING"):
                
               
            
            
            }

        }

    }
}