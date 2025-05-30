using System;
using Pixel_Plumbers_Fall_2024;

public class ResetGameCommand2 : ICommand
{
    private Game1 game;
    private GameStateMachine gameStateMachine;

    public ResetGameCommand2(Game1 game, GameStateMachine gameStateMachine)
    {
        this.game = game;
        this.gameStateMachine = gameStateMachine;
    }

    public void Execute()
    {
        if (gameStateMachine.isCurrentStateOver())
        {
            gameStateMachine.setGameStateRunning();
            game.ResetGame();
            Console.WriteLine("Reset Game Command");
        }
    }
}
