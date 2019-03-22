using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;
using Galaga_Exercise_2.Squadrons;

namespace Galaga_Exercise_2 {
    public class Diamant : ISquadron {
        public List<Image> enemies;
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public Diamant(List<Image> enemyImage) {
            enemies = enemyImage;
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            float initValue = 0.1f;
            for (int i = 0; i < 8; i++) {
                initValue += 0.1f;
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(initValue, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }


            }
        }

        }

