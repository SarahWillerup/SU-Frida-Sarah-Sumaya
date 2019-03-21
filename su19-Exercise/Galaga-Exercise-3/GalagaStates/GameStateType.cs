using System;
using System.ComponentModel;

namespace Galaga_Exercise_3 {
    public enum GameStateType {
    GameRunning,
    GamePaused,
    MainMenu,

    }

    public class StateTransformer {

        public static GameStateType TransformStringToState(string state) {
            switch (state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
                break;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
                break;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
                break;
            default:
                throw new ArgumentException("INVALID_ARGUMENT");
                break;
            }

        }


        public static string TransformStatetoString(GameStateType state) {
            switch (state) {
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
                break;
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
                break;
            case GameStateType.MainMenu:
                return "MAIN_MENU";
                break;
            default:
                throw new ArgumentException("INVALID_ARGUMENT");
                break;
            }

        }
    }
}

    

