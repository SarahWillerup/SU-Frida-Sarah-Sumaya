using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2 {
    public class Player : IGameEventProcessor<object> {
        public Entity entity { get;}
        private Game game;
        private GameEventBus<object> eventBus;
        private DIKUArcade.Window win;

        public Player(Game game, DynamicShape shape, IBaseImage image)
            : base() {
            this.game = game;

            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent
            });
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent, this, "ATTACK", "SWORD", "5"));

        }

        private void Direction(Vec2F v1) {
            entity.Shape.AsDynamicShape().Direction = v1;
        }

        public void Move() {
            if ((entity.Shape.AsDynamicShape().Direction.X > 0 && entity.Shape.Position.X < 1 - entity.Shape.Extent.X) || 
            (entity.Shape.AsDynamicShape().Direction.X < 0 && entity.Shape.Position.X > 0)) {
                entity.Shape.Move();
            }
        }

        public void PlayerShotAdded() {
            game.playershots.Add(new PlayerShot(game,
                new DynamicShape(new Vec2F(entity.Shape.Position.X + entity.Shape.Extent.X / 2, entity.Shape.Position.Y + entity.Shape.Extent.Y), new Vec2F(0.008f, 0.027f)),
                game.PlayerShot));
        }

        public void KeyPress(string key) {
            switch (key) {
            case "KEY_ESCAPE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                break;
            case "KEY_LEFT":
                Direction(new Vec2F(-0.01f, 0.0f));
                break;
            case "KEY_RIGHT":
                Direction(new Vec2F(0.01f, 0.0f));
                break;
            case "KEY_SPACE":
                PlayerShotAdded();
                break;
            default:
                break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_LEFT":
                Direction(new Vec2F(0.0f, 0.00f));
                break;
            case "KEY_RIGHT":
                Direction(new Vec2F(0.0f, 0.00f));
                break;
            default:
                break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (eventType) {
                case GameEventType.InputEvent:
                    switch (gameEvent.Parameter1) {
                    case "KEY_PRESS":
                        KeyPress(gameEvent.Message);
                        break;
                    case "KEY_RELEASE":
                        KeyRelease(gameEvent.Message);
                        break;
                    }

                    break;
                }
            }
        }
        
    }
}
