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
using Galaga_Exercise_3.GalagaEntities.Enemy;
using Galaga_Exercise_3.GalagaStates;
using Galaga_Exercise_3.Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3 {
    public class Game : IGameEventProcessor<object> {
        public ISquadron isquadron { get; set; }
        public Window win;
        public DIKUArcade.Timers.GameTimer gameTimer;
        private Player player;
        private GameEventBus<object> eventBus;
        public List<Image> enemyStrides;
        private EntityContainer <Enemy> enemies;
        public EntityContainer<PlayerShot> playershots { get; private set; }
        public Image PlayerShot;
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;
        private Score score;
        public Diamant diamants;
        public V v;
        public T t;
        public ZigZagDown zigzagdown;
        public Down down;
        public NoMove nomove;
        public StateMachine statemachine;
        public MainMenu mainmenu;


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
            enemies = new EntityContainer<Enemy>();
    

            PlayerShot = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            playershots = new EntityContainer<PlayerShot>();

            explosionStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "Explosion.png"));


            score = new Score(new Vec2F(0.05f, 0.04f),new Vec2F(0.5f, 0.5f));
            
            explosionStrides =
                ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(10);

            diamants = new Diamant();
            
            diamants.CreateEnemies(enemyStrides);

            foreach (Enemy enemy in diamants.Enemies) {
                enemies.AddDynamicEntity(enemy);
            }
            
            zigzagdown = new ZigZagDown();
            statemachine = new StateMachine();
            mainmenu = new MainMenu();
            
            zigzagdown.MoveEnemies(enemies);

            foreach (Enemy enemy in enemies) {
                zigzagdown.MoveEnemies(enemies);
            }
        }


        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    GalagaBus.GetBus().ProcessEvents();
                    statemachine.ActiveState.UpdateGameLogic();
                }
                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    statemachine.ActiveState.RenderState();
                    win.SwapBuffers();
                }
                if (gameTimer.ShouldReset()) {
                    win.Title = "Galaga | UPS:" + gameTimer.CapturedUpdates + ", FPS: " +
                    gameTimer.CapturedFrames;
 
                } }
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
                            GameEventType.PlayerEvent, this, "NO_MOVE", "", ""));
                }

                break;
            case "KEY_RIGHT":
                if (key.Equals("KEY_RIGHT") || key.Equals("KEY_LEFT")) {
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "NO_MOVE", "", ""));
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



 
    


 

 
    

