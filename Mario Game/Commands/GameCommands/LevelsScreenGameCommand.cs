using System;
using Pixel_Plumbers_Fall_2024;

public class LevelScreenCommand : ICommand
{
    private GameStateMachine gameStateMachine;
    public LevelScreenCommand(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }
    public void Execute()
    {
        gameStateMachine.setGameLevelsScreen();
        Console.WriteLine("lvl1ScreenCommand");
    }
}