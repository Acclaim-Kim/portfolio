using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public class PlayerBlockInteraction
{
    private IPlayer player;
    private IBlock block;
    private Rectangle playerRect;
    private Rectangle blockRect;
    public Boolean isLuckyBlock;
    private List<IEntity> entitiesRemoved;

    public PlayerBlockInteraction(IPlayer player, IBlock block, List<IEntity> entitiesRemoved, Boolean isLuckyBlock)
    {
        this.player = player;
        this.block = block;
        playerRect = player.GetDestination();
        blockRect = block.GetDestination();
        this.entitiesRemoved = entitiesRemoved;
        this.isLuckyBlock = isLuckyBlock;
    }

    public void update()
    {
        float overlapLeft = playerRect.Right - blockRect.Left;
        float overlapRight = blockRect.Right - playerRect.Left;
        float overlapTop = playerRect.Bottom - blockRect.Top;
        float overlapBottom = blockRect.Bottom - playerRect.Top;
        float minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight), Math.Min(overlapTop, overlapBottom));

        if (minOverlap == overlapTop)
        {
            player.SetPositionY(blockRect.Top - playerRect.Height);
            player.SetVelocityY(0);
            player.SetIsOnGround(true);
            player.JumpStop();
        }
        else if (minOverlap == overlapBottom)
        {
            player.SetPositionY(blockRect.Bottom);
            player.SetVelocityY(0);

            //bump animation
            block.bump();

            //broke block except luckyblock
            if (!player.getStateMachine().IsSmall())
            {
                block.broke();
                if (!isLuckyBlock) 
                {
                    removeFromList();
                }
                
            }

        }
        else if (minOverlap == overlapLeft)
        {
            player.SetPositionX(blockRect.Left - playerRect.Width);
            player.SetVelocityX(0); 
        }
        else if (minOverlap == overlapRight)
        {
            player.SetPositionX(blockRect.Right);
            player.SetVelocityX(0); 
        }
    }
    private void removeFromList()
    {
        entitiesRemoved.Add(block);
    }
}