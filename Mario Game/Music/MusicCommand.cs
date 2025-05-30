using Microsoft.Xna.Framework.Media;
using Pixel_Plumbers_Fall_2024;

public class MusicCommand : ICommand
{
    private MusicMachine MusicStateMachine;
    private Song song;

    public MusicCommand(MusicMachine MusicStateMachine)
    {
        this.MusicStateMachine = MusicStateMachine;
        song = MusicStateMachine.current();
    }

    public void Execute()
    {
        song = MusicStateMachine.NextSong();
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = true;
    }
}
