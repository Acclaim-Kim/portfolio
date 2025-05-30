using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using System.Threading.Tasks;


    public class WinCutScene: ICutScene
{
    IPlayer player;
    Rectangle currentPosition;
    PlayerStateMachine stateMachine;
    CutSceneManager manager;
    float time = 0;
    int doorDistance = 160;
    public  WinCutScene(IPlayer player, Rectangle currentPosition)
    {
        this.player = player;
        manager = new CutSceneManager(player);
    }
    public Boolean entersDoor()
    {
        if (doorDistance < 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public void Update(GameTime gameTime)
    {
        time += 1 /*(float)gameTime.ElapsedGameTime.TotalSeconds*/;
        if (time > 1 && doorDistance >= 0)
        {
            player.SetPositionX(player.GetDestination().X + 2);

            doorDistance-=2;
            time = 0;
        }
    }
    public void play(GameTime gameTime)
    {
        stateMachine = player.GetStateMachine();
        manager.setPlayerPosition(6372, 380);

    }
}


