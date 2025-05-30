using Microsoft.Xna.Framework.Media;
using Pixel_Plumbers_Fall_2024;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BlackJackCommand : ICommand
{
    private Game1 game;
    private BlackJackStateMachine blackJackStateMachine;
    private GameStateMachine gameStateMachine;

    public BlackJackCommand(BlackJackStateMachine blackJackStateMachine, GameStateMachine gameStateMachine)
    {
        this.blackJackStateMachine = blackJackStateMachine;
        this.gameStateMachine = gameStateMachine;
    }

    public void Execute()
    {
        if (!gameStateMachine.isCurrentStatePaused())
        {
            blackJackStateMachine.play();
            gameStateMachine.setGameStatePaused();
        } else
        {
            blackJackStateMachine.stop();
            gameStateMachine.setGameStateRunning();
        }
    }
}
