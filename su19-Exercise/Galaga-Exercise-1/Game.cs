using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;


namespace Galaga_Exercise_1 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private DIKUArcade.Timers.GameTimer gameTimer;
        private Player player;
        private GameEventBus<object> eventBus;
        public List<Image> enemyStrides;
        public List<Enemy> enemies;
        public List<Playershot> playershots { get; private set; }
        public Image PlayerShot;
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;
        private Score score;

        public Game() {
            // Add reasonable values
            win = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(50, 50);
            player = new Player(this,
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
            });
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);


            enemyStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            AddEnemies();

            PlayerShot = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            playershots = new List<Playershot>();

            explosionStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "Explosion.png"));


            score = new Score(new Vec2F(0.05f, 0.04f),new Vec2F(0.5f, 0.5f));
            
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

                List<Playershot> newPlayershots = new List<Playershot>();
                foreach (Playershot playershot in playershots) {
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
                    
                    
                    player.RenderEntity();
                    
                    foreach (Playershot shot in playershots) {
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

        public void KeyPress(string key) {
            switch (key) {
            case "KEY_ESCAPE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                break;
            case "KEY_LEFT":
                player.Direction(new Vec2F(-0.01f, 0.0f));
                break;
            case "KEY_RIGHT":
                player.Direction(new Vec2F(0.01f, 0.0f));
                break;
            case "KEY_SPACE":
                player.PlayerShotAdded();
                break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_ESCAPE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                break;
            case "KEY_LEFT":
                player.Direction(new Vec2F(0.0f, 0.0f));
                break;
            case "KEY_RIGHT":
                player.Direction(new Vec2F(0.0f, 0.0f));
                break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSUE_WINDOW":
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
                }
            }

        }

        public void AddExplosion(float posX, float posY, float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));

        }
    }

} 
    

