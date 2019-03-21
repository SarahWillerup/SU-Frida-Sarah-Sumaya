using System;
using System.ComponentModel;

namespace Galaga_Exercise_3 {
    public enum GameStateType { }

    public class StateTransformer {
        public static GameStateType GameRunning;
        public static GameStateType GamePaused;
        public static GameStateType MainMenu;

        public static GameStateType TransformStringToState(string state) {
            if (state == "GAME_RUNNING") {
                return StateTransformer.GameRunning;
            } else if (state == "GAME_PAUSED") {
                return StateTransformer.GamePaused;
            } else {
                if (state == "MAIN_MENU") {
                   
                }
                return StateTransformer.MainMenu;
            }
        }


        public static string TransformStatetoString(GameStateType state) {
            throw new ArgumentException("INVALID_ARGUMENT");
                
            }
        }

}

    

