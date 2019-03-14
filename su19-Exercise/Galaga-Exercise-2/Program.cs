using System;
using System.Runtime.InteropServices;
using DIKUArcade.EventBus;

namespace Galaga_Exercise_2 {
    internal class Program {
        public static void Main(string[] args) {
            Game game = new Game();
            game.GameLoop();

        }
    }
}