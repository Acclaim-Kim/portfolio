using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class PlayerMushroomInteraction
{
    private IPlayer player;
    private Mushroom mushroom;
    private List<IEntity> entitiesRemoved;
    int mushroomIndex;
    int marioIndex;

    public PlayerMushroomInteraction(IPlayer play, Mushroom mush, List<IEntity> entitiesRemoved)
    {
        player = play;
        mushroom = mush;
        this.entitiesRemoved = entitiesRemoved;
    }

    public void update()
    {
        PlayerStateMachine playerStateMachine = player.getStateMachine();
        mushroom.collect();
        if (playerStateMachine.IsSmall())
        {
            player.PowerUp();
        }

        player.AddScore(100);

        removeFromList();
    }
    
    private void removeFromList()
    {
        entitiesRemoved.Add(mushroom);


    }

}



