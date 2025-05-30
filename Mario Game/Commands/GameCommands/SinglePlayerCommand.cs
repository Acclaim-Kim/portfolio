using System;
using System.Runtime.CompilerServices;

public class SingleplayerCommand : ICommand
{
    private GameStateMachine gameStateMachine;
    private ICommand levelScreenCommand;

    public SingleplayerCommand(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
        levelScreenCommand = new LevelScreenCommand(gameStateMachine);
    }
    public void Execute()
    {
        if (gameStateMachine.isStartScreen())
        {
            gameStateMachine.setGameSinglePlayer();
            levelScreenCommand.Execute();
            Console.WriteLine("SinglePlayer Command");
        }
    }
}