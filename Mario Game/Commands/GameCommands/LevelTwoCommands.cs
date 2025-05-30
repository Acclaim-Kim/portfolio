using Pixel_Plumbers_Fall_2024;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

public class LevelTwoCommand : ICommand
{
    private GameStateMachine gameStateMachine;
    private DisableScreenCommand disableScreenCommand;
    private BlackJackStateMachine blackJackStateMachine;
    private Game1 game;

    public LevelTwoCommand(GameStateMachine gameStateMachine, Dictionary<Rectangle, ICommand> list, MouseController gameMouseController, BlackJackStateMachine blackJackStateMachine, Game1 game)
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
            gameStateMachine.setLevelTwo();
            gameStateMachine.setGameStateRunning();
            disableScreenCommand.Execute();
            disableScreenCommand.Set(blackJackStateMachine, gameStateMachine);
            Console.WriteLine("lvl2Command");
        }
    }
}