using System;
using DIKUArcade.Entities;
using Galaga_Exercise_3_1.GalagaEntities.Enemy;

namespace Galaga_Exercise_3_1.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        public float s = -0.0003f, p = 0.045f, a = 0.05f;
        public void MoveEnemy(Enemy enemy) {
            float yZero = enemy.StartPosition.Y;
            float xZero = enemy.StartPosition.X;
            enemy.Shape.Position.Y += s;
            enemy.Shape.Position.X = (float)(xZero +
                a * Math.Sin((2 * Math.PI * (yZero - enemy.Shape.Position.Y)) / p));
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy e in enemies) {
                MoveEnemy(e);
            }
        }
    }
}