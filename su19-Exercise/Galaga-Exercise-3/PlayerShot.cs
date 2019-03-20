using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3 {
    public class PlayerShot : Entity{
    private Game game;
    public Shape shape;

    public PlayerShot(Game game, DynamicShape shape, IBaseImage image)
        : base(shape, image) {
        this.game = game;
        this.shape = shape;
        Shape.AsDynamicShape().Direction = new Vec2F(0.0f,0.01f);
    }
    }
}