namespace Galaga_Exercise_2 {
    public class Diamant {
        public void AddEnemies() {
            ISquadron.Enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.2f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.3f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.4f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.5f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.6f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.7f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
            enemies.Add(new Enemy(this,
                new DynamicShape(new Vec2F(0.8f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides)));
        }
        
    }
}