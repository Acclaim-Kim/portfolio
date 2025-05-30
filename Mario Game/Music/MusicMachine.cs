using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pixel_Plumbers_Fall_2024;


public class MusicMachine
{

    private List<Song> songList;
    private Song currentSong;

    public MusicMachine(SoundManager musics)
    {
        songList = new List<Song>();
        currentSong = musics.GetSongs("mario");
        songList.Add(currentSong);
        MediaPlayer.Play(currentSong);
        currentSong = musics.GetSongs("kirby");
        songList.Add(currentSong);
        currentSong = musics.GetSongs("Doom");
        songList.Add(currentSong);
        currentSong = musics.GetSongs("rumbling");
        songList.Add(currentSong);
        currentSong = musics.GetSongs("erwin");
        songList.Add(currentSong);
        currentSong = musics.GetSongs("LionSin");
        songList.Add(currentSong);
    }

    public Song current()
    {
        return songList[0];
    }

    public Song NextSong()
    {
        Song save = songList[0];
        songList.RemoveAt(0);
        songList.Add(save);
        return songList[0];
    }

}
