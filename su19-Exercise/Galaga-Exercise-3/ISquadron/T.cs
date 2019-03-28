using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities.Enemy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3 {
    public class T : ISquadron {
        public List<Image> enemies;
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public T() {
            Enemies = new EntityContainer<Enemy>();
        }
      

        public void CreateEnemies(List<Image> enemyStrides) {
            float initValue = 0.1f;
            for (int i = 0; i < 4; i++) {
                initValue += 0.1f;
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(initValue, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }

            for (int n = 0; n < 3; n++) {
                initValue += 0.1f;
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.35f, initValue), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }

        }
    }

}
  