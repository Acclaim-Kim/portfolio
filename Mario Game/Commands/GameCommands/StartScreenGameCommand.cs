using System;
using Pixel_Plumbers_Fall_2024;

public class StartScreeGameCommand : ICommand
{
    private GameStateMachine gameStateMachine;
    public StartScreeGameCommand(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }
    public void Execute()
    {
        gameStateMachine.setGameStateStart();
        Console.WriteLine("Start Screen Command");
    }
}