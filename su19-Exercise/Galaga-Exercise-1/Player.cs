using System.IO;
using System.IO.Pipes;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Player : Entity {
        private Game game;

        public Player(Game game, DynamicShape shape, IBaseImage image) 
            : base(shape, image) {
            this.game = game;

        }

        public void Direction(Vec2F v1) {
            Shape.AsDynamicShape().Direction = v1;
        }

        public void Move() {
            if (Shape.Position.X >= 0 && Shape.Position.X <= 1 - Shape.Extent.X + Shape.AsDynamicShape().Direction.X) {
                Shape.Move();
            }     
        }

        public void AddPlayerShot() {
            game.PlayerShots.Add(new PlayerShot(game,
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.008f, 0.027f)),
                game.PlayerShot));
        }



    }
    }
