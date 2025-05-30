using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

public class SoundManager
{
    private Dictionary<string, Song> Songs;
    private Dictionary<string, SoundEffect> Sounds;
    private ContentManager content;

    private Song LionSin;
    private Song mario;
    private Song kirby;
    private Song Doom;
    private Song rumbling;
    private Song erwin;

    private SoundEffect pipeSound;
    private SoundEffect oneUpSound;
    private SoundEffect coinSound;
    private SoundEffect fireBallSound;
    private SoundEffect flagPoleSound;
    private SoundEffect powerUpSound;
    private SoundEffect powerUpSpawnsSound;
    private SoundEffect marioJump;
    private SoundEffect marioDeath;
    private SoundEffect fwip;
    private SoundEffect gameOverSound;

    public SoundManager(ContentManager content)
    {
        this.content = content;

        Songs = new Dictionary<string, Song>();
        Sounds = new Dictionary<string, SoundEffect>();

        LionSin = content.Load<Song>("Audio/LionSin");
        mario = content.Load<Song>("Audio/mario");
        kirby = content.Load<Song>("Audio/kirby");
        Doom = content.Load<Song>("Audio/Doom");
        rumbling = content.Load<Song>("Audio/rumbling");
        erwin = content.Load<Song>("Audio/erwin");

        Songs["LionSin"] = LionSin;
        Songs["mario"] = mario;
        Songs["kirby"] = kirby;
        Songs["Doom"] = Doom;
        Songs["rumbling"] = rumbling;
        Songs["erwin"] = erwin;

        pipeSound = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_pipe");
        oneUpSound = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_1-up");
        coinSound = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_coin");
        fireBallSound = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_fireball");
        flagPoleSound = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_flagpole");
        powerUpSound = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_powerup");
        powerUpSpawnsSound = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_powerup_appears");
        marioJump = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_jump-small");
        marioDeath = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_mariodie");
        fwip = content.Load<SoundEffect>("Audio/flip");
        gameOverSound = content.Load<SoundEffect>("Audio/Sound Effect(s)/smb_gameover");

        Sounds["pipe"] = pipeSound;
        Sounds["1-up"] = oneUpSound;
        Sounds["coin"] = coinSound;
        Sounds["fireball"] = fireBallSound;
        Sounds["flagpole"] = flagPoleSound;
        Sounds["powerup"] = powerUpSound;
        Sounds["spawn"] = powerUpSpawnsSound;
        Sounds["jump"] = marioJump;
        Sounds["death"] = marioDeath;
        Sounds["fwip"] = fwip;
        Sounds["over"] = gameOverSound;
    }

    public Song GetSongs(string songName)
    {
        if (Songs.TryGetValue(songName, out var song))
        {
            return song;
        }
        throw new KeyNotFoundException($"Song '{songName}' not found.");
    }
    public SoundEffect GetSound(string soundName)
    {
        if (Sounds.TryGetValue(soundName, out var sound))
        {
            return sound;
        }
        throw new KeyNotFoundException($"Sound '{soundName}' not found.");
    }
}
