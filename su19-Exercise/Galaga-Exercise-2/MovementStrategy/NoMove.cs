using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;

namespace Galaga_Exercise_2 {
    public class NoMove {
        public void MoveEnemy(Enemy enemy) { }

        public void MoveEnemies(EntityContainer<Enemy> enemies) { }
    }
}
