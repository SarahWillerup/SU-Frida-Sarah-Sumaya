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
        public Window win;
        public DIKUArcade.Timers.GameTimer gameTimer;
        public EntityContainer<PlayerShot> playershots { get; private set; }
        public Image PlayerShot;

        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;

        private List<Image> enemyStrides;
        private EntityContainer<Enemy> enemies;
        public ISquadron iSquadron { get; set; }
        public Diamant diamants;
        public IMovementStrategy movementStrategy;
        public Down down;
        public ZigZagDown zigzagdown;
        public NoMove nomove;

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
        public void GameLoop() {}

        public void AddExplosion(float posX, float posY, float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

        public void IterateShots() {
            foreach (PlayerShot shot in playershots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }

                foreach (Enemy enemy in diamants.Enemies) {
                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape)
                        .Collision) {
                        enemy.DeleteEntity();
                        shot.DeleteEntity();
                        AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y, 0.1f, 0.1f);
                        score.Addpoint();
                    }

                }

                EntityContainer<Enemy> newEnemies = new EntityContainer<Enemy>();
                foreach (Enemy enemy in diamants.Enemies) {
                    if (!enemy.IsDeleted()) {
                        newEnemies.AddDynamicEntity(enemy);
                    }
                }

                diamants.Enemies = newEnemies;


                EntityContainer<PlayerShot> newPlayershots = new EntityContainer<PlayerShot>();
                foreach (PlayerShot playershot in playershots) {
                    if (!playershot.IsDeleted()) {
                        newPlayershots.AddDynamicEntity(playershot);
                    }
                }

                playershots = newPlayershots;
            }
        }

        
    





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

        


        public void InitializeGameState() {
            stateMachine = new StateMachine();
            player = new Player(new Game(), 
                shape: new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                image: new Image(Path.Combine("Assets", "Images", "Player.png")));

            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType>() {
                GameEventType.PlayerEvent
            });

            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

            enemyStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new EntityContainer<Enemy>();

            PlayerShot = new Image(Path.Combine("Assets", "Images", "BlueMonster.png"));
            playershots = new EntityContainer<PlayerShot>();

            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(40);

            score = new Score(new Vec2F(0.43f, -0.12f), new Vec2F(0.2f, 0.2f));

            diamants = new Diamant();
            diamants.CreateEnemies(enemyStrides);
            enemies = diamants.Enemies;

            down = new Down();
            zigzagdown = new ZigZagDown();
            nomove = new NoMove();
        }

        public void UpdateGameLogic() {
            IterateShots();
            player.Move();
            zigzagdown.MoveEnemies(enemies);
            


            
        }

        public void RenderState() {
            player.player.RenderEntity();
            foreach (PlayerShot shot in playershots) {
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
