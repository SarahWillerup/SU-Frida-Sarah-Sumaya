using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Physics;
using Galaga_Exercise_2.GalagaEntities.Enemy;

namespace Galaga_Exercise_2 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private DIKUArcade.Timers.GameTimer gameTimer;
        private Player player;
        private GameEventBus<object> eventBus;
        public List<Image> enemyStrides;
        public List<Enemy> enemies;
        public List<PlayerShot> playershots { get; private set; }
        public Image PlayerShot;
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;
        private Score score;

        public Game() {
            // Add reasonable values
            win = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(40, 40);
            player = new Player(this,
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent
            });
           

            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);


            enemyStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            AddEnemies();

            PlayerShot = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            playershots = new List<PlayerShot>();

            explosionStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "Explosion.png"));

            score = new Score(new Vec2F(0.05f,0.04f),new Vec2F(0.5f,0.5f));
            
            explosionStrides =
                ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(10);
        }

        public void AddEnemies() {
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.2f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.3f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.4f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.5f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.6f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.7f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.8f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
        }

        public void IterateShots() {
            foreach (var shot in playershots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }

                foreach (var enemy in enemies) {
                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape)
                        .Collision) {
                        enemy.DeleteEntity();
                        shot.DeleteEntity();
                        AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y, 0.1f, 0.1f);
                        score.Addpoint();
                    }

                }

                List<Enemy> newEnemies = new List<Enemy>();
                foreach (Enemy enemy in enemies) {
                    if (!enemy.IsDeleted()) {
                        newEnemies.Add(enemy);
                    }
                }

                enemies = newEnemies;

                List<PlayerShot> newPlayershots = new List<PlayerShot>();
                foreach (PlayerShot playershot in playershots) {
                    if (!playershot.IsDeleted()) {
                        newPlayershots.Add(playershot);
                    }
                }

                playershots = newPlayershots;
            }
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    IterateShots();
                    player.Move();
                    eventBus.ProcessEvents();
                    //Update game logic here
                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    // Render gameplay entities here
                    
                    
                    player.player.RenderEntity();
                    
                    foreach (PlayerShot shot in playershots) {
                        shot.RenderEntity();
                    }
                    foreach (Enemy enemy in enemies) {
                        enemy.RenderEntity();

                    }
                    explosions.RenderAnimations();

                    score.RenderScore();

                    win.SwapBuffers();

                }

                if (gameTimer.ShouldReset()) {
                    // 1 second has passed - display last captured ups and fps
                    win.Title = "Galaga | UPS:" + gameTimer.CapturedUpdates + ", FPS: " +
                                gameTimer.CapturedFrames;
                }
            }
        }

        
        public void AddExplosion(float posX, float posY, float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));

        }
        public void KeyPress(string key) {
            switch (key) {
            case "KEY_ESCAPE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                break;
            case "KEY_LEFT":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_LEFT", "", ""));
                break;
            case "KEY_RIGHT":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_RIGHT", "", ""));
                break;
            case "KEY_SPACE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_SPACE", "", ""));
                break;
            default:
                break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_LEFT":
                if (key.Equals("KEY_LEFT") || key.Equals("KEY_RIGHT")) {
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,  "NO_MOVE", "", ""));
                }
                break;
            case "KEY_RIGHT":
                if (key.Equals("KEY_RIGHT") || key.Equals("KEY_LEFT")) {
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,  "NO_MOVE", "", ""));
                }
                break;
            default:
                break;
            }
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                default:
                   break;
                    }
                
            } else if (eventType == GameEventType.InputEvent) {
                        switch (gameEvent.Parameter1) {
                        case "KEY_PRESS":
                            KeyPress(gameEvent.Message);
                            break;
                        case "KEY_RELEASE":
                            KeyRelease(gameEvent.Message);
                            break;
                        default:
                            break;
                        }

                    }
                 
                    
                }
            }
 }

 
    

