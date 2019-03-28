using System;

namespace Galaga_Exercise_3.GalagaStates {
    public class GameStateType {
        public enum Enum_GameStateType {
            MainMenu, 
            GameRunning, 
            GamePaused, 
            Terminated
        }

        public class StateTransformer {
            //public static Enum_GameStateType emum_GameStateType;
            //readonly Enum_GameStateType enum_GameStateType;

            public static Enum_GameStateType TransformStringToState(string state) {
                switch (state) {
                case "GAME_RUNNING":
                    return Enum_GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return Enum_GameStateType.GamePaused;
                case "MAIN_MENU":
                    return Enum_GameStateType.MainMenu;
                default:
                    throw new ArgumentException();
                }
            }


            public static string TransformStateToString(Enum_GameStateType state) {
                switch (state) {
                case Enum_GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case Enum_GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case Enum_GameStateType.MainMenu:
                    return "MAIN_MENU";
                default:
                    throw new ArgumentException();
                }
            }
        }
    }


}
    

