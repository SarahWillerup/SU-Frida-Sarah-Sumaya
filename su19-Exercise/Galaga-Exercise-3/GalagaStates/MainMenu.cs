using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;



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
            backGroundImage = ImageStride.CreateStrides(1, Path.Combine(("TitleImage.png")));
            backGroundImage.RenderEntity();


        }

        public void GameLoop() { }
        
        public void InitializeGameState() {}
        
        public void UpdateGameLogic() {}
        
        public void HandleKeyEvent(string keyValue, string keyAction) {}
    }