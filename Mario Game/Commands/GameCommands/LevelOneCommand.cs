using Pixel_Plumbers_Fall_2024;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

public class LevelOneCommand : ICommand
{
    private GameStateMachine gameStateMachine;
    private DisableScreenCommand disableScreenCommand;
    private Game1 game;

    private BlackJackStateMachine blackJackStateMachine;
    public LevelOneCommand(GameStateMachine gameStateMachine, Dictionary<Rectangle, ICommand> list, MouseController gameMouseController, BlackJackStateMachine blackJackStateMachine, Game1 game)
    {
        this.gameStateMachine = gameStateMachine;
        this.blackJackStateMachine = blackJackStateMachine;
        disableScreenCommand = new DisableScreenCommand(list, gameMouseController);
        this.game = game;
    }
    public void Execute()
    {
        if (gameStateMachine.isLevelScreen())
        {
            game.ResetGame();
            gameStateMachine.setLevelOne();
            gameStateMachine.setGameStateRunning();
            disableScreenCommand.Execute();
            disableScreenCommand.Set(blackJackStateMachine, gameStateMachine);
            Console.WriteLine("lvl1Command");
        }
    }
}