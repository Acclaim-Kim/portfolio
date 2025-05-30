using System;
using Pixel_Plumbers_Fall_2024;

public class ResetGameCommand : ICommand
{
    private Game1 game;

    public ResetGameCommand(Game1 game)
    {
        this.game = game;
    }

    public void Execute()
    {
        game.ResetGame();
        Console.WriteLine("Reset Command");
    }
}
