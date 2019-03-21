using System;
using Galaga_Exercise_3;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class Tests {
        [Test]
        public void GAMERUNNING() {
            Assert.AreEqual(StateTransformer.GameRunning, StateTransformer.TransformStringToState("GAME_RUNNING"));
        }
        
        [Test]
        public void GAMEPAUSED() {
            Assert.AreEqual(StateTransformer.GamePaused, StateTransformer.TransformStringToState("GAME_PAUSED"));
        }
        [Test]
        public void MAINMENU() {
            Assert.AreEqual(StateTransformer.MainMenu, StateTransformer.TransformStringToState("MAIN_MENU"));
        }
        [Test]
        public void EXCEPTION() {
            Assert.AreNotEqual("INVALID_ARGUMENT", StateTransformer.TransformStatetoString(StateTransformer.GameRunning));
        }
    }
}