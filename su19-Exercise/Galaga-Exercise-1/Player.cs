using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Player : Entity {
        private Game game;

        public Player(Game game, DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.game = game;

        }

        public void Direction(Vec2F vec2F) {
            Shape.AsDynamicShape().Direction = vec2F;
        }

        public void Move() {
            Vec2F newPos = Shape.Position + Shape.AsDynamicShape().Direction;
            if (newPos.X > 0||1 < newPos.X)
                Shape.Move();

        }

        public void PlayerShotAdded() {
            game.playershots.Add(new Playershot(game,
                new DynamicShape(new Vec2F(Shape.Position.X + Shape.Extent.X / 2, Shape.Position.Y + Shape.Extent.Y), new Vec2F(0.008f, 0.027f)),
                game.PlayerShot));
        }
    }



}
