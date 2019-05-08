using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3_1.GalagaEntities.Enemy {
    public class Enemy : Entity {
        public DynamicShape shape;
        public Vec2F StartPosition { get; }
        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            StartPosition = new Vec2F(shape.Position.X, shape.Position.Y);
            this.shape = shape;
        }

      
    }
}