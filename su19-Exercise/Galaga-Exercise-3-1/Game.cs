using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;
using Galaga_Exercise_3_1;
using Galaga_Exercise_3_1.GalagaStates;

public class Game : IGameEventProcessor<object> {
    private Window win;
    private DIKUArcade.Timers.GameTimer gameTimer;
    private StateMachine stateMachine;
    public MainMenu mainmenu;


    public Game() {
        win = new Window("WindowName", 500, 500);
        gameTimer = new GameTimer(50, 50);
        
        //eventBus = new GameEventBus<object>();
        GalagaBus.GetBus().InitializeEventBus(new List<GameEventType>() {
            GameEventType.InputEvent, // key press / key release
            GameEventType.WindowEvent, // messages to the window
            //GameEventType.PlayerEvent
            GameEventType.GameStateEvent
        });
        
        win.RegisterEventBus(GalagaBus.GetBus());
        //GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        
        stateMachine = new StateMachine();
       
        
    }

    public void GameLoop() {
        while (win.IsRunning()) {
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                GalagaBus.GetBus().ProcessEvents();
                stateMachine.ActiveState.UpdateGameLogic();
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                stateMachine.ActiveState.RenderState();
                win.SwapBuffers();
                
            }

            if (gameTimer.ShouldReset()) {
                win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                            ", FPS: " + gameTimer.CapturedFrames;
            }
        }
    }
  
    
    
    private void KeyPress(string key) {
        switch (key) {
        case "KEY_ESCAPE":
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
            break;
        }
    }

    public void KeyRelease(string key) {
        switch (key) {
        case "KEY_ESCAPE":
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
            break;
        }
    }

    public void ProcessEvent(GameEventType eventType,
        GameEvent<object> gameEvent) {
        if (eventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
            case "CLOSE_WINDOW":
                win.CloseWindow();
                break;
            }

        } else if (eventType == GameEventType.InputEvent)
            {
                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                }
            }
            
    }

}
