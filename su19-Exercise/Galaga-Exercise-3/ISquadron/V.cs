using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities.Enemy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3 {
    public class V : ISquadron {
        public List<Image> enemies;
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public V() {
            Enemies = new EntityContainer<Enemy>();
        }
      

        public void CreateEnemies(List<Image> enemyStrides) {
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.05f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.15f, 0.8f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.25f, 0.7f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.35f, 0.6f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.45f, 0.5f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.55f, 0.6f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.65f, 0.7f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.75f, 0.8f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.85f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
    }
  }

}
   