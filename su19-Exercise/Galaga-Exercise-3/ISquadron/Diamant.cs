using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities.Enemy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3 {
    public class Diamant : ISquadron {
        public EntityContainer<Enemy> enemies;
        public EntityContainer<Enemy>Enemies { get; set; }
        public int MaxEnemies { get; }

        public Diamant() {
            Enemies = new EntityContainer<Enemy>();
        }
      

        public void CreateEnemies(List<Image> enemyStrides) {
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.4f, 0.5f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.2f, 0.7f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.3f, 0.8f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.4f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.5f, 0.8f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.6f, 0.7f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.5f, 0.6f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.3f, 0.6f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
    }
  }

}

