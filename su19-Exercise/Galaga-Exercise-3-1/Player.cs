using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3_1.GalagaStates;

namespace Galaga_Exercise_3_1 {
    public class Player : IGameEventProcessor<object> {
        public Entity player;
        private GameRunning gameRunning;
        private Shape shape;
        private DIKUArcade.Window win;
        private IBaseImage image;

        public Player(GameRunning gameRunning, DynamicShape shape, IBaseImage image) {
            this.gameRunning = gameRunning;
            player = new Entity(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
            }

        public void Move() {
            if ((player.Shape.AsDynamicShape().Direction.X > 0 && player.Shape.Position.X < 1 - player.Shape.Extent.X) || 
                (player.Shape.AsDynamicShape().Direction.X < 0 && player.Shape.Position.X > 0)) {
                player.Shape.Move();
            }    
        }

        public void Direction(Vec2F direction) {
            player.Shape.AsDynamicShape().Direction = direction;
        }

        public void AddShot() {
            gameRunning.playerShots.AddDynamicEntity(new PlayerShot(gameRunning,
                new DynamicShape(
                    new Vec2F(player.Shape.Position.X + player.Shape.Extent.X / 2, player.Shape.Position.Y + player.Shape.Extent.Y),
                    new Vec2F(0.008f, 0.027f)),
                gameRunning.PlayerShot));
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                case "KEY_LEFT":
                    Direction(new Vec2F(-0.01f, 0.0f));
                    break;
                case "KEY_RIGHT":
                    Direction(new Vec2F(0.01f, 0.0f));
                    break;
                case "KEY_SPACE":
                    AddShot();
                    break;
                case "NO_MOVE":
                    Direction(new Vec2F(0.0f, 0.0f));
                    break;
               }
            }

        }
    }
}
