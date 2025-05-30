using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

public class ItemBlockInteraction
{
    private IItem item;
    private IBlock block;
    private Rectangle itemRect;
    private Rectangle blockRect;
    private bool wasOnTop;
    private GameTime gameTime;
    public ItemBlockInteraction(IItem i, IBlock block, GameTime gameTime)
    {
        this.item = i;
        this.block = block;
        this.gameTime = gameTime;
        itemRect = item.GetDestination();
        blockRect = block.GetDestination();
        wasOnTop = false;
    }
    public void update()
    {
            int overlapTop = itemRect.Bottom - blockRect.Top;
            int overlapBottom = blockRect.Bottom - itemRect.Top;
            int overlapLeft = itemRect.Right - blockRect.Left;
            int overlapRight = blockRect.Right - itemRect.Left;

            float minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight), Math.Min(overlapTop, overlapBottom));
            if (minOverlap == overlapTop)
            {
                item.SetPositionY(blockRect.Top - itemRect.Height);
                item.SetVelocityY(0); 
                item.NotFalling();
                wasOnTop = true; 
            }
            else if (minOverlap == overlapLeft)
            {
                item.swapDirection();
            }
            else if (minOverlap == overlapRight)
            {
                item.swapDirection();
            }
        

        if (!itemRect.Intersects(blockRect))
        {
            if (wasOnTop)
            {
                item.SetIsOnGround(false);
                wasOnTop = false;
            }
        }

        if (!item.GetIsOnGround())
        {
            item.ApplyGravity(gameTime); 
        }
    }
}
