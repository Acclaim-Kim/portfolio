using System;
using Pixel_Plumbers_Fall_2024;

public class StartScreeGameCommand2 : ICommand
{
    private GameStateMachine gameStateMachine;
    public StartScreeGameCommand2(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }
    public void Execute()
    {
        if (gameStateMachine.isCurrentStateOver())
        {
            gameStateMachine.setGameStateStart();
            Console.WriteLine("Start Screen Command");
        }
    }
}