using DIKUArcade.EventBus;

namespace Galaga_Exercise_3_1 {
    public static class GalagaBus {
            private static GameEventBus<object> eventBus;
            public static GameEventBus<object> GetBus() {
                return GalagaBus.eventBus ?? (GalagaBus.eventBus =
                           new GameEventBus<object>());
            }
        }
}
