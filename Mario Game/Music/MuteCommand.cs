using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Pixel_Plumbers_Fall_2024;

public class MuteCommand : ICommand
{
    int count = 0;

    public void Execute()
    {
        if (count == 0)
        {
            SoundEffect.MasterVolume = 0;
            MediaPlayer.Pause();
            count++;
        }
        else
        {
            MediaPlayer.Resume();
            SoundEffect.MasterVolume = 1;
            count = 0;
        }
    }
}
