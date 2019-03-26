using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;

namespace Galaga_Exercise_2 {
    public class NoMove {
        public void MoveEnemy(Enemy enemy) {
            float newY = 0.0f;
            float newX = 0.0f;

            newY = enemy.shape.Position.Y - 0.0003f;
            newX = (float)((0.5f + 0.05f * Math.Sin(2*Math.PI) * (0.9f - newY)/ 0.045f));
            
            enemy.shape.Position = new Vec2F(newX, newY);
        }


        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (var enem in enemies) {
                ((Enemy) enem).shape.MoveY(-0.005f);

            }
        }

        public void NM() {
            MoveEnemy(null);
        }

    }
}