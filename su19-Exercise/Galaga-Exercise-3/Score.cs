using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga_Exercise_3 {
    public class Score {
        private int score;
        private Text display;
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
        }
        public void AddPoint() {score += 10;}
        
        public void RenderScore() {
            display.SetText(string.Format("Score: {0}", score.ToString()));
            display.SetColor(new Vec3I(255,0,0));
            display.RenderText();
        }
    }
}