using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3 {
    public class Player : IGameEventProcessor<object> {
        public Entity player;
        private Game game;
        private Shape shape;
        private IBaseImage image;
        //private GameEventBus<object> eventBus;
        private DIKUArcade.Window win;

        public Player(Game game, DynamicShape shape, IBaseImage image) {
            this.game = game;
            player = new Entity(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
       
       /* eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent
            });
        eventBus.Subscribe(GameEventType.PlayerEvent, this);*/

        }

        public void Direction(Vec2F v1) {
            player.Shape.AsDynamicShape().Direction = v1;
        }

        public void Move() {
            if ((player.Shape.AsDynamicShape().Direction.X > 0 && player.Shape.Position.X < 1 - player.Shape.Extent.X) || 
            (player.Shape.AsDynamicShape().Direction.X < 0 && player.Shape.Position.X > 0)) {
                player.Shape.Move();
            }
        }

        public void PlayerShotAdded() {
            game.playershots.AddDynamicEntity(new PlayerShot(game,
                new DynamicShape(new Vec2F(player.Shape.Position.X + player.Shape.Extent.X / 2, 
                    player.Shape.Position.Y + player.Shape.Extent.Y), new Vec2F(0.008f, 0.027f)),
                game.PlayerShot));
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
                        PlayerShotAdded();
                        break;
                    case "NO_MOVE":
                        Direction(new Vec2F(0.0f,0.0f));
                        break;
                    default:
                        break;
                    }
                    
                }
            }
        }
        
    }

