using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class PlayerItemInteraction
{
    private IPlayer player;
    private IItem item;
    private List<IEntity> entitiesRemoved;
    
    public PlayerItemInteraction(IItem item, IPlayer player, List<IEntity> entitiesRemoved)
    {
        this.item = item;
        this.player = player;
        this.entitiesRemoved = entitiesRemoved;
    }

    public void update()
    {
        player.PowerUp();
        removeFromList();
    }
    
    private void removeFromList()
    {
        entitiesRemoved.Add(item);
    }
}
