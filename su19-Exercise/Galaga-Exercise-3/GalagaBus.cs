using DIKUArcade.EventBus;
using Galaga_Exercise_3;
namespace GalagaGame {
    public static class GalagaBus {
        private static GameEventBus<object> eventBus;

        public static GameEventBus<object> GetBus() {
            return GalagaBus.eventBus ?? (GalagaBus.eventBus =
                       new GameEventBus<object>());

        }
    }
}