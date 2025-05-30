using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class ItemObstacleInteraction
{
    private IItem item;
    private IObstacle obstacle;
    private Rectangle itemRectangle;
    private Rectangle obstacleRect;
   
    public ItemObstacleInteraction(IItem i, IObstacle obstacle)
    {
        this.item = i;
        this.obstacle = obstacle;
      
    }
    
    public void update()
    {
        Rectangle o_rectangle = obstacle.GetDestination();
        Rectangle i_rectangle = item.GetDestination();

        Rectangle intersection = Rectangle.Intersect(i_rectangle, o_rectangle);
        if (intersection.Width < intersection.Height)
        {
            item.swapDirection();
        }
        else
        {
            if (i_rectangle.Bottom <= o_rectangle.Top + intersection.Height)
            {
                item.NotFalling();
            }
            else
            {
                
            }
        }
        item.swapDirection();
    }
    
}
