using System;
using Microsoft.Xna.Framework.Media;
using Pixel_Plumbers_Fall_2024;

public class PauseGameCommand : ICommand
{
    private GameStateMachine gameStateMachine;
    private MusicMachine MusicStateMachine;

    public PauseGameCommand(GameStateMachine gameStateMachine, MusicMachine musicStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
        MusicStateMachine = musicStateMachine;
    }

    public void Execute()
    {
        if (gameStateMachine.isCurrentStatePaused())
        {
            gameStateMachine.setGameStateRunning();
            MediaPlayer.Resume();
        }
        else
        {
            gameStateMachine.setGameStatePaused();
            MediaPlayer.Pause();
        }
        Console.WriteLine("Pause Command");
    }
}
