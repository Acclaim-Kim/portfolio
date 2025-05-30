using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CutSceneManager
{
    DisablePlayerCommand disableIPlayer;
    PlayerMovementController marioMovementController;
    PlayerMovementController luigiMovementController;
    IPlayer player;
    PlayerStateMachine playerSM;
    public CutSceneManager(IPlayer player)
    {
        this.player = player;
        marioMovementController = new PlayerMovementController();
        luigiMovementController = new PlayerMovementController();
        disableIPlayer = new DisablePlayerCommand(marioMovementController,luigiMovementController);
        playerSM = player.GetStateMachine();
       
    }
    public void moveRight()
    {
        player.MoveRight();
        
    }
    public void moveLeft()
    {
        player.MoveLeft();
    }
    public void moveDown()
    {
        player.SetPositionY(player.GetDestination().Y +1);
    }
    public void moveUp()
    {
        player.SetPositionY(player.GetDestination().Y - 1);
    }


    public void setPlayerPosition( int x, int y)
    {
        player.SetPositionX(x);
        player.SetPositionY(y);
    }
    
    public void disablePlayer()
    {
        disableIPlayer.Execute();
    }

}



