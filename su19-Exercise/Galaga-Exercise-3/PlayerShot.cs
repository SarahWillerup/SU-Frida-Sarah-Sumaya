using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaStates;

namespace Galaga_Exercise_3 {
    public class PlayerShot : Entity {
        private GameRunning gameRunning;
        public Shape shape;

        public PlayerShot(GameRunning gameRunning, DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.gameRunning = gameRunning;
            this.shape = shape;
            Shape.AsDynamicShape().Direction = new Vec2F(0.0f,0.01f);

        }
        
    }
}