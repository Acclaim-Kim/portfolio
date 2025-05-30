using Microsoft.Xna.Framework;
using Pixel_Plumbers_Fall_2024;
using System;

public class PlayerObstacleInteraction
{
    private IPlayer player;
    private IObstacle obstacle;
    private Rectangle marioRect;
    private Rectangle obstacleRect;
    private GameTime gameTime; 

    private bool wasOnTop = false;

    public PlayerObstacleInteraction(IPlayer player, IObstacle obstacle, GameTime gameTime)
    {
        this.player = player;
        this.obstacle = obstacle;
        this.gameTime = gameTime;
        marioRect = player.GetDestination();
        obstacleRect = obstacle.GetDestination();
    }

    public void update() 
    {
        float overlapLeft = marioRect.Right - obstacleRect.Left;
        float overlapRight = obstacleRect.Right - marioRect.Left;
        float overlapTop = marioRect.Bottom - obstacleRect.Top;
        float overlapBottom = obstacleRect.Bottom - marioRect.Top;

        float minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight), Math.Min(overlapTop, overlapBottom));

        if (minOverlap == overlapTop)
        {
            player.SetPositionY(obstacleRect.Top - marioRect.Height);
            player.SetVelocityY(0); 
            player.SetIsOnGround(true);
            wasOnTop = true; 
            player.JumpStop();
        }
        else if (minOverlap == overlapBottom)
        {
            player.SetPositionY(obstacleRect.Bottom);
            player.SetVelocityX(0); 
        }
        else if (minOverlap == overlapLeft)
        {
            player.SetPositionX(obstacleRect.Left - marioRect.Width);
            player.SetVelocityX(0); 
        }
        else if (minOverlap == overlapRight)
        {
            player.SetPositionX(obstacleRect.Right);
            player.SetVelocityX(0); 
        }
        
        if (!marioRect.Intersects(obstacleRect))
        {
            if (wasOnTop)
            {
                player.SetIsOnGround(false); 
                wasOnTop = false;
            }
        }
        if (!player.GetIsOnGround())
        {
            player.ApplyGravity(gameTime); 
        }
    }
}