using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MarioStarInteraction
{
    private IPlayer player;
    private Star star;
    private List<IEntity> entitiesRemoved;
    int count = 0;


    public MarioStarInteraction(IPlayer play, Star s, List<IEntity> entitiesRemoved)
    {
        this.star = s;
        this.player = play;
        this.entitiesRemoved = entitiesRemoved;

    }
    public void update()
    {
      
        star.collect();
        player.CollectStar();
        removeFromList();

        player.AddScore(100);
        
    }
    private void removeFromList()
    {
        entitiesRemoved.Add(star);
    }
}
