using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Galaga_Exercise_1 {
    public class Score {
        private int score;
        private Text display;
        private Game game;

        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position,extent);
        }

        public void Addpoint() {
            score += 1;
        } 

        public void RenderScore() {
            display.SetText(string.Format("Score: {0}",score.ToString()));
            display.SetColor(new Vec3I(255,0,0 ));
            display.RenderText();
        }
    }
}