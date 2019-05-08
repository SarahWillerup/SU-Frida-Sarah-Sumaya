using DIKUArcade.Entities;
using Galaga_Exercise_3_1.GalagaEntities.Enemy;

namespace Galaga_Exercise_3_1.MovementStrategy {
    public class NoMove : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.MoveY(-0.0f);
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy e in enemies) {
                MoveEnemy(e);
            }
        }

    }
}