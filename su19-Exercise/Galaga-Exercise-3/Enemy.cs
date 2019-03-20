using System.Collections.Generic;
using DIKUArcade.Entities;
using System.IO.Pipes;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3.GalagaEntities.Enemy {
    public class Enemy :  Entity {
        private Game game;
        public DynamicShape shape;
        private Vec2F vec2F { get; }

        public Enemy(Game game, DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.game = game;
            this.shape = shape;
        }
    }
}
    