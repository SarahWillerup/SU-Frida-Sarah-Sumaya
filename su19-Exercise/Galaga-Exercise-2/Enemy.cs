using System.Collections.Generic;
using DIKUArcade.Entities;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2.GalagaEntities.Enemy {
    public class Enemy :  Entity {
        public DynamicShape shape;
        private Vec2F StartPos { get; }

        public Enemy( DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.shape = shape;
            StartPos = new Vec2F(shape.Position.X, shape.Position.Y);
            
        }
    }
}
    