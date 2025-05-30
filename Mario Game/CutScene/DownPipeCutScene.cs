using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


    public class DownPipeCutScene: ICutScene
{
    IPlayer player1;
    IPlayer player2;
    Rectangle currentPosition;
    PlayerStateMachine stateMachine;
    CutSceneManager manager;

    public DownPipeCutScene(IPlayer player)
    {

    }
    public void play(GameTime gameTime)
    {

    }
}

