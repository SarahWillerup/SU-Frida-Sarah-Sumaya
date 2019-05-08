using System.Drawing;
using System.IO;
using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using DIKUArcade.EventBus;
using OpenTK.Input;
using Image = DIKUArcade.Graphics.Image;


namespace Galaga_Exercise_3_1.GalagaStates {
    public class MainMenu : IGameState {
        internal static MainMenu instance = null;

        private Entity backGroundImage;
        private Text[] menuButtons;
        private Text newGameButton, quitButton;
        private int activeMenuButton;
        private int maxMenuButtons;
        private int inactiveMenuButton;

        public MainMenu() {
            backGroundImage =
                new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                    new Image(Path.Combine("Assets", "Images", "TitleImage.png")));

            menuButtons = new[] {
                newGameButton = new Text("New Game", new Vec2F(0.2f, 0.35f),
                    new Vec2F(0.3f, 0.35f)),
                quitButton = new Text("Quit", new Vec2F(0.2f, 0.3f), new Vec2F(0.3f, 0.3f))
            };
            activeMenuButton = 0;
            maxMenuButtons = menuButtons.Length;
        }

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }

        public void GameLoop() { }

        public void InitializeGameState() { }

        public void UpdateGameLogic() { }

        public void RenderState() {
            //Console.WriteLine("Hej");
            backGroundImage.RenderEntity();

            for (int i = 0; i < maxMenuButtons; i++) {
                menuButtons[i].SetColor(activeMenuButton == i ? Color.Blue : Color.Red);
                menuButtons[i].RenderText();
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                switch (keyValue) {
                case "KEY_UP":
                    if (activeMenuButton == 1) {
                        activeMenuButton = 0;
                        inactiveMenuButton = 1;
                        menuButtons[inactiveMenuButton].SetColor(new Vec3F(0.5f, 0.1f, 0.1f));
                        menuButtons[activeMenuButton].SetColor((new Vec3F(0.3f, 0.4f, 0.1f)));
                    }

                    break;
                case "KEY_DOWN":
                    if (activeMenuButton == 0) {
                        activeMenuButton = 1;
                        inactiveMenuButton = 0;
                        menuButtons[inactiveMenuButton].SetColor(new Vec3F(0.5f, 0.1f, 0.1f));
                        menuButtons[activeMenuButton].SetColor((new Vec3F(0.3f, 0.4f, 0.1f)));
                    }

                    break;
                case "KEY_ENTER":
                    if (activeMenuButton == 0) {
                        GameRunning.GetInstance().InitializeGameState();
                        GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent,
                                this,
                                "CHANGE_STATE",
                                "GAME_RUNNING", ""));
                    }

                    if (activeMenuButton == 1) {
                        GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.WindowEvent,
                                this,
                                "CLOSE_WINDOW", "", ""));
                    }

                    break;
                }
            }
        }
    }
}