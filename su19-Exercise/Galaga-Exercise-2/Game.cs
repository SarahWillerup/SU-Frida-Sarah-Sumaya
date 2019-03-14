using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace Galaga_Exercise_2 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private DIKUArcade.Timers.GameTimer gameTimer;
        private Player player;
        private GameEventBus<object> eventBus;
        public List<Image> enemyStrides;
        public List<Enemy> enemies;
        public List<PlayerShot> PlayerShots;
        public Image PlayerShot;
        private IGameEventProcessor<object> gameEventProcessorImplementation;

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
            });
           

            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);


            enemyStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();

            PlayerShot = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            PlayerShots = new List<PlayerShot>();

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
        }


        public void GameLoop() {
            AddEnemies();
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    eventBus.ProcessEvents();
                    player.Move();
                    //Update game logic here
                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    // Render gameplay entities here
                    player.Entity.RenderEntity();
                    foreach (Enemy enemy in enemies) {
                        enemy.RenderEntity();

                    }

                    win.SwapBuffers();

                }

                if (gameTimer.ShouldReset()) {
                    // 1 second has passed - display last captured ups and fps
                    win.Title = "Galaga | UPS:" + gameTimer.CapturedUpdates + ", FPS: " +
                                gameTimer.CapturedFrames;
                }
            }
        }

        public void IterateShots() {
            foreach (PlayerShot shot in PlayerShots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }

                foreach (var enemy in enemies) { }

            }
        }
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (eventType) {
                case GameEventType.WindowEvent:
                    switch (gameEvent.Message) {
                    case "CLOSUE_WINDOW":
                        win.CloseWindow();
                        break;
                    }

                    break;

                case GameEventType.PlayerEvent:
                    player.ProcessEvent( eventType,  gameEvent);
                    break;
                    


                case GameEventType.InputEvent:
                    switch (gameEvent.Parameter1) {
                    case "KEY_PRESS":
                        player.KeyPress(gameEvent.Message);
                        break;
                    case "KEY_RELEASE":
                        player.KeyRelease(gameEvent.Message);
                        break;
                    }

                    break;
                }
            }
        }

    }

}  
    

