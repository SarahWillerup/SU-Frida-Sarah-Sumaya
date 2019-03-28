using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;
using Galaga_Exercise_3.GalagaEntities.Enemy;
using Galaga_Exercise_3.Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3.GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private GameEventBus<object> gameEventBus;

        private Player player;

        public EntityContainer<PlayerShot> playerShots { get; private set; }
        public Image PlayerShot;

        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;

        private List<Image> enemyStrides;
        private EntityContainer<Enemy> enemies;
        public ISquadron iSquadron { get; set; }
        public Diamant Diamant;
        public IMovementStrategy movementStrategy;
        public Down down;
        public ZigZagDown zigzagdown;

        private Score score;

        public Game game;
        private StateMachine stateMachine;

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        //private Window win;

        // private DIKUArcade.Timers.GameTimer gameTimer;


        public GameRunning() {
            InitializeGameState();
        }

        public void AddExplosion(float posX, float posY, float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

        public void IterateShots() {
            foreach (PlayerShot shot in playerShots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }

                foreach (Enemy e in enemies) {
                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), e.Shape).Collision) {
                        e.DeleteEntity();
                        shot.DeleteEntity();
                        AddExplosion(e.Shape.Position.X - 0.05f,
                            e.Shape.Position.Y - 0.05f, 0.2f,
                            0.2f); //e.Shape.Extent.X, e.Shape.Extent.Y);
                        score.Addpoint();
                    }
                }
            }

            EntityContainer<Enemy> newEnemies = new EntityContainer<Enemy>();
            foreach (Enemy e in enemies) {
                if (!e.IsDeleted()) {
                    newEnemies.AddDynamicEntity(e);
                }
            }

            enemies = newEnemies;

            EntityContainer<PlayerShot> newPlayerShots = new EntityContainer<PlayerShot>();
            foreach (PlayerShot shot in playerShots) {
                if (!shot.IsDeleted()) {
                    newPlayerShots.AddDynamicEntity(shot);
                }
            }

            playerShots = newPlayerShots;
        }


        /*       public void GameLoop() {
                   while (game.win.IsRunning()) {
                       while (game.gameTimer.ShouldUpdate()) {
                           IterateShots();
                           player.Move();
                           zigzagdown.MoveEnemies(enemies);
                           GalagaBus.GetBus().ProcessEvents();
                   
                       }
       
                       if (game.gameTimer.ShouldRender()) {
                          player.player.RenderEntity();
                           foreach (PlayerShot shot in playerShots) {shot.RenderEntity();}
                           enemies.RenderEntities();
                           explosions.RenderAnimations();
                           score.RenderScore();
                           }
                    }
               }*/

        private void KeyPress(string key) {
            switch (key) {
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
            case "KEY_LEFT":
                if (key.Equals("KEY_LEFT") || key.Equals("KEY_RIGHT")) {
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "NO_MOVE", "", ""));
                }

                break;
            case "KEY_RIGHT":
                if (key.Equals("KEY_RIGHT") || key.Equals("KEY_LEFT")) {
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "NO_MOVE", "", ""));
                }

                break;
            }
        }

        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.InputEvent) {
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


        public void GameLoop() {
            throw new System.NotImplementedException();
        }

        public void InitializeGameState() {
            stateMachine = new StateMachine();
            stateMachine.ActiveState.RenderState();
            player = new Player( new Game (), 
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            //win = new Window("WindowName", 500, 500);
            //gameTimer = new GameTimer(50, 50);



            gameEventBus = new GameEventBus<object>();
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent,
                GameEventType.GameStateEvent,
            });

            game.win.RegisterEventBus(GalagaBus.GetBus());
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, game);
            //GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, game);
            //GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, game);

            EntityContainer<Enemy> enemyContainer = new EntityContainer<Enemy>();
            enemyStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new EntityContainer<Enemy>();

            PlayerShot = new Image(Path.Combine("Assets", "Images", "BlueMonster.png"));
            playerShots = new EntityContainer<PlayerShot>();

            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(40);

            score = new Score(new Vec2F(0.43f, -0.12f), new Vec2F(0.2f, 0.2f));

            Diamant = new Diamant();
            Diamant.CreateEnemies(enemyStrides);
            enemies = Diamant.Enemies;

            down = new Down();
            zigzagdown = new ZigZagDown();
        }

        public void UpdateGameLogic() {
            IterateShots();
            player.Move();
            zigzagdown.MoveEnemies(enemies);
            GalagaBus.GetBus().ProcessEvents();
        }

        public void RenderState() {
            player.player.RenderEntity();
            foreach (PlayerShot shot in playerShots) {
                shot.RenderEntity();
            }

            enemies.RenderEntities();
            explosions.RenderAnimations();
            score.RenderScore();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {}

            // public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
                //   throw new System.NotImplementedException();
                //}
            
        
    }
}