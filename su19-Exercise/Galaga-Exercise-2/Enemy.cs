using System.Collections.Generic;
using DIKUArcade.Entities;
using System.IO.Pipes;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2.GalagaEntities.Enemy {
    public class Enemy :  Entity {
        public DynamicShape shape;
        private Vec2F vec2F { get; }

        public Enemy( DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.shape = shape;
        }
    }
}
    