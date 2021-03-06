using System.Drawing;
using System.IO;
using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using DIKUArcade.EventBus;
using Image = DIKUArcade.Graphics.Image;


namespace Galaga_Exercise_3.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;

        private Entity backGroundImage;
        private Text[] menuButtons;
        private Text newGameButton, quitButton;
        private int activeMenuButton;
        private int maxMenuButtons;

        public MainMenu() {
            InitializeGameState();
            
            backGroundImage =
                new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                    new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            
            menuButtons = new[]
            {
                newGameButton = new Text("New Game", new Vec2F(0.38f, 0.2f), new Vec2F(0.3f, 0.4f)),
                quitButton = new Text("Quit", new Vec2F(0.45f, 0.1f), new Vec2F(0.3f, 0.4f))
            };
            maxMenuButtons = menuButtons.Length;
            
            foreach (var button in menuButtons) {
                button.SetColor(new Vec3F(0f,0f,1f));
                //newGameButton.SetColor(new Vec3F(255f,255f,255f));
                //quitButton.SetColor(new Vec3F(255f,255f,255f));
            }
        }
        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }

        

        public void GameLoop() {}

        public void InitializeGameState() {}

        public void UpdateGameLogic() {
        }

        public void RenderState() {
               
                backGroundImage.RenderEntity();
                foreach (var button in menuButtons) {
                    button.RenderText();
                }
            
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                switch (keyValue) {
                    case "KEY_UP":
                        activeMenuButton = Math.Max(activeMenuButton-1, 0);
                        menuButtons[activeMenuButton].SetColor((new Vec3F(0.3f, 0.4f, 0.1f)));
                        break;
                    case "KEY_DOWN":
                        activeMenuButton = Math.Min(activeMenuButton+1, maxMenuButtons);
                        menuButtons[activeMenuButton].SetColor((new Vec3F(0.3f, 0.4f, 0.1f)));
                        break;
                    case "KEY_ENTER":
                        if (menuButtons[activeMenuButton].Equals(newGameButton)) {
                            GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent,
                                this,
                                "CHANGE_STATE",
                                "GAME_RUNNING", ""));
                        }
                        if (menuButtons[activeMenuButton].Equals(quitButton)) {
                            GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.WindowEvent,
                                this,
                                "CLOSE WINDOW", "",""));
                        }
                        break;
                }
                
            }
        }
    }
}