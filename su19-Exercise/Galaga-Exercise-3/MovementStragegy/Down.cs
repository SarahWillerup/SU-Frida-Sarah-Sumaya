using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities.Enemy;

namespace Galaga_Exercise_3 {
    public class Down {
        public void MoveEnemy(Enemy enemy) {
            enemy.shape.MoveY(-0.002f);
        }


        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);

            }
        }

        
       
    }
}