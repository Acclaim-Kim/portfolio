using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ObstacleFireballInteraction
{
    private Fireball fireBall;
    private IObstacle obstacle;
    public ObstacleFireballInteraction(Fireball fb, IObstacle obst)
    {
        this.fireBall = fb;
        this.obstacle = obst;
    }

    public void Update()
    {
        fireBall.Remove();
    }
}

