using System;
using System.Drawing;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Image = DIKUArcade.Graphics.Image;


namespace Galaga_Exercise_3.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;

        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }

        public void RenderState() {
            backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.1f, 1.1f)),
                new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            menuButtons = new[] {
                new Text("NEW_GAME", new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)),
                new Text("QUIT", new Vec2F(0.3f, 0.3f), new Vec2F(0.3f, 0.3f))
            };



        }

        public void GameLoop() { }

        public void InitializeGameState() { }

        public void UpdateGameLogic() { }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
            case "KEY_UP":
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this, "KEY_UP", "", "");
                break;
            case "KEY_DOWN":
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this, "KEY_DOWN", "", "");
                break;
            case "KEY_ENTER":
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this, "KEY_ENTER", "NEW_GAME", "QUIT");
                if (keyAction == "NEW_GAME") {
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent,
                        this,
                        "CHANGE_STATE",
                        "GAME_RUNNING", "");
                    
                } else if (keyAction == "QUIT") {
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent,
                        this,
                        "CLOSE_WINDOW",
                        "", ""); 
                }
                break;
                default:
                break;
            }

            }

        }
    }
}