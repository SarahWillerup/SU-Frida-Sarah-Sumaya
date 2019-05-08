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
using Galaga_Exercise_3_1.GalagaEntities.Enemy;
using Galaga_Exercise_3_1.MovementStrategy;
using Galaga_Exercise_3_1.Squadrons;

namespace Galaga_Exercise_3_1.GalagaStates {
    public class GameRunning : IGameState {
        public static GameRunning instance = null;
        public StateMachine stateMachine;

        private Player player;
        private GameEventBus<object> eventBus;
        private List<Image> enemyStrides;
        private EntityContainer<Enemy> enemies;
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;
        private Score score;
        public ISquadron iSquadron { get; set; }
        public EntityContainer<PlayerShot> playerShots { get; private set; }
        public Image PlayerShot;

        public V_Formation v_Formation;
        public Double_Formation double_formation;
        public Zig_Zag_Formation zig_zag_formation;

        public IMovementStrategy movementStrategy;
        public Down down;
        public ZigZagDown zigzagdown;
        public NoMove noMove;

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());

        }

        public GameRunning() {
           InitializeGameState();

        }

        public void GameLoop() {}

        public void InitializeGameState() {
            stateMachine = new StateMachine();
            player = new Player(this,
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
            playerShots = new EntityContainer<PlayerShot>();

            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(40);

            score = new Score(new Vec2F(0.43f, -0.12f), new Vec2F(0.2f, 0.2f));

            v_Formation = new V_Formation();
            v_Formation.CreateEnemies(enemyStrides);
            enemies = v_Formation.Enemies;

            down = new Down();
            zigzagdown = new ZigZagDown();
            noMove = new NoMove();
        }

        public void UpdateGameLogic() {
            IterateShots();
            player.Move();
            zigzagdown.MoveEnemies(enemies);
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

        public void HandleKeyEvent(string keyValue, string keyAction) { }


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
                            e.Shape.Position.Y - 0.05f, 0.2f, 0.2f);
                        score.AddPoint();
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

        private void KeyPress(string key) {
            switch (key) {
            case "KEY_LEFT":
                player.Direction(new Vec2F(-0.01f, 0.0f));
                break;
            case "KEY_RIGHT":
                player.Direction(new Vec2F(0.01f, 0.0f));
                break;
            case "KEY_SPACE":
                player.AddShot();
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
    }
}
