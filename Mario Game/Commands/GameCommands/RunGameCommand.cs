using System;
using Pixel_Plumbers_Fall_2024;

public class RunGameCommand : ICommand
{
    private GameStateMachine gameStateMachine;
    public RunGameCommand(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }
    public void Execute()
    {
        gameStateMachine.setGameStateRunning();
        Console.WriteLine("Start Game Command");
    }
}