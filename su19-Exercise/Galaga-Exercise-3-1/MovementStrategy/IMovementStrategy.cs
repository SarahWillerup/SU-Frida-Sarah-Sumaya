using DIKUArcade.Entities;
using Galaga_Exercise_3_1.GalagaEntities.Enemy;

namespace Galaga_Exercise_3_1.MovementStrategy {
    public interface IMovementStrategy  {
            void MoveEnemy(Enemy enemy);
            void MoveEnemies(EntityContainer<Enemy> enemies);
        }

}