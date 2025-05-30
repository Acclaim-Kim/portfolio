using System;
using System.Runtime.CompilerServices;

public class MultiplayerCommand : ICommand
{
    private GameStateMachine gameStateMachine;
    private ICommand levelScreenCommand;

    public MultiplayerCommand(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
        levelScreenCommand = new LevelScreenCommand(gameStateMachine);
    }
    public void Execute()
    {
        if (gameStateMachine.isStartScreen())
        {
            gameStateMachine.setGameMultiplayer();
            levelScreenCommand.Execute();
            Console.WriteLine("MultiplayerCommand");
        }
    }
}