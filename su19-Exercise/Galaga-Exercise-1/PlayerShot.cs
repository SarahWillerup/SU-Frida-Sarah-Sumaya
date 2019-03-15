using System.IO.Pipes;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Galaga_Exercise_1 {
    public class Playershot : Entity {
        private Game game;

        public Playershot(Game game, DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.game = game; 
            Shape.AsDynamicShape().Direction = new Vec2F(0.0f, 0.01f);
        }
    }
}