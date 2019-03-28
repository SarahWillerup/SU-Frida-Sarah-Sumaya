using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;
using Galaga_Exercise_2.MovementStrategy;

namespace Galaga_Exercise_2 {
namespace Galaga_Exercise_2.MovementStrategy {

    public class ZigZagDown {
        public Enemy enemy;

        public void MoveEnemy(Enemy enemy) {

            float newY = 0.0f;
            float newX = 0.0f;

            newY = enemy.shape.Position.Y - 0.0003f;
            newX = (float) (enemy.StartPos.X +
                            0.05f * Math.Sin(2 * Math.PI * (enemy.StartPos.Y - newY) / 0.045));

            enemy.shape.Position = new Vec2F(newX, newY);
        }


        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);

            }
        }


    }

}
}