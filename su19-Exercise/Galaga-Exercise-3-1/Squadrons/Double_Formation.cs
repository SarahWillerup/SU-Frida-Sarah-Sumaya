using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3_1.GalagaEntities.Enemy;

namespace Galaga_Exercise_3_1.Squadrons {
    public class Double_Formation : ISquadron {
        public List<Image> enemies;
        public EntityContainer<Enemy>Enemies { get; }
        public int MaxEnemies { get; }

        public Double_Formation() {
            Enemies = new EntityContainer<Enemy>();
        }
      

        public void CreateEnemies(List<Image> enemyStrides) {
            Enemies.AddDynamicEntity(new Enemy(
                new DynamicShape(new Vec2F(0.2f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            Enemies.AddDynamicEntity(new Enemy(
                new DynamicShape(new Vec2F(0.3f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            Enemies.AddDynamicEntity(new Enemy(
                new DynamicShape(new Vec2F(0.4f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            Enemies.AddDynamicEntity(new Enemy(
                new DynamicShape(new Vec2F(0.5f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            Enemies.AddDynamicEntity(new Enemy(
                new DynamicShape(new Vec2F(0.6f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            Enemies.AddDynamicEntity(new Enemy(
                new DynamicShape(new Vec2F(0.3f, 0.8f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            Enemies.AddDynamicEntity(new Enemy(
                new DynamicShape(new Vec2F(0.4f, 0.8f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            Enemies.AddDynamicEntity(new Enemy(
                new DynamicShape(new Vec2F(0.5f, 0.8f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
        }   
    }
}