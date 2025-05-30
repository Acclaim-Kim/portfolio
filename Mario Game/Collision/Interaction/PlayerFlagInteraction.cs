using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerFlagInteraction
{

    private IPlayer player;
    private Flag flag;
    private List<IEntity> entitiesRemoved;
    private List<IEntity> entities;
    private int index;
    private float timeCount;
    DisablePlayerCommand disablePlayerCommand;

    public PlayerFlagInteraction(IPlayer play, Flag flag,List<IEntity> entities, int index,  List<IEntity> entitiesRemoved, DisablePlayerCommand disablePlayerCommand)
    {
        this.flag = flag;
        this.player = play;
        this.entities = entities;
        this.entitiesRemoved = entitiesRemoved;
        this.disablePlayerCommand = disablePlayerCommand;
        this.timeCount = 0;
        this.index = index;
    }

    public void update(GameTime gameTime)
    {
        disablePlayerCommand.Execute();
       Rectangle destination = player.GetDestination();
       flag.makeWinFlag(destination.Y);
        flag.resetFlag();     
       player.SetWin();


        
       
        //player.GetStateMachine().MakeVisible();
        //player.ResetWin();
        //player.GetStateMachine().SetPlayerBig();
        //        WinCutScene wc = new WinCutScene(player, destination);
        //        wc.play();
               
            
        
    }
    private void removeFromList()
    {
        entitiesRemoved.Add(flag);
    }
}

