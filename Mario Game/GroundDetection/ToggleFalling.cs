using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Runtime.InteropServices;


public class ToggleFalling
{
    private List<Rectangle> collisionRects;
    private List<IEntity> entities;
    private List<IEntity> enemies;
    private List<IEntity> items;
    private List<IEntity> fireballs;
    private Mario mario;
    private Luigi luigi;
    private Ground ground;
    private float fallingGroundPosition = 480f;
    private float groundPosition = 385f;

    FilterEntities filterEntities;
    public ToggleFalling(Ground g, List<IEntity> entities, Mario mario, Luigi luigi)
    {
        this.ground = g;
        this.collisionRects = g.allCollisionRectangles();
        this.entities = entities;
        filterEntities = new FilterEntities();
        enemies = filterEntities.FilterEnemies(entities);
        items = filterEntities.FilterItems(entities);
        fireballs = filterEntities.FilterFireballs(entities);
        this.mario = mario;
        this.luigi = luigi;
    }


    public void updates()
    {
        updatePlayerFalling(this.luigi);
        updatePlayerFalling(this.mario);
        updateItemFalling(this.items);
        updateEnemyFalling(this.enemies);
        updateFireBallFalling(this.fireballs);
    }

    public void updateFireBallFalling(List<IEntity> fireBalls)
    {
        for (int i = 0; i < fireBalls.Count; i++)
        {
            Fireball fireBall = (Fireball)fireBalls[i];
            Rectangle fireBallBounds = fireBall.GetDestination();
            bool fireBallColliding = false;
            for (int j = 0; j < collisionRects.Count; j++)
            {
                Rectangle blockBounds = collisionRects[j];
                if (fireBallBounds.Intersects(blockBounds))
                {
                    fireBallColliding = true;
                    if (fireBallBounds.Bottom > blockBounds.Top &&
                        fireBallBounds.Top < blockBounds.Top &&
                        fireBallBounds.Right > blockBounds.Left &&
                        fireBallBounds.Left < blockBounds.Right)
                    {
                        fireBall.Bounce();
                    }
                    else if (fireBallBounds.Right > blockBounds.Left &&
                             fireBallBounds.Left < blockBounds.Left)
                    {
                        fireBall.Remove();
                    }
                    else if (fireBallBounds.Left < blockBounds.Right &&
                             fireBallBounds.Right > blockBounds.Right)
                    {
                        fireBall.Remove();
                    }
                    break;
                }
            }
            if (!fireBallColliding)
            {
                // I guess it would just keep its velocity until collision again.
            }
        }
    }



    public void updateEnemyFalling(List<IEntity> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            ISpriteEnemy currentEnemy = (ISpriteEnemy)enemies[i];

            Rectangle enemyBounds = currentEnemy.GetDestination();

            bool enemyColliding = false;
            for (int j = 0; j < collisionRects.Count; j++)
            {
                Rectangle blockBounds = collisionRects[j];
                if (enemyBounds.Intersects(blockBounds) && !currentEnemy.IsDead())
                {

                    enemyColliding = true;

                    if (enemyBounds.Bottom > blockBounds.Top &&
                        enemyBounds.Top < blockBounds.Top &&
                        enemyBounds.Right > blockBounds.Left &&
                        enemyBounds.Left < blockBounds.Right)
                    {
                        currentEnemy.setGroundPosition(groundPosition);

                    }
                    else if (enemyBounds.Right > blockBounds.Left &&
                             enemyBounds.Left < blockBounds.Left)
                    {
                        currentEnemy.changeDirection();
                    }
                    else if (enemyBounds.Left < blockBounds.Right &&
                             enemyBounds.Right > blockBounds.Right)
                    {
                        currentEnemy.changeDirection();
                    }

                    break;
                }
            }
            if (!enemyColliding)
            {
                // Make it fall when that functionality is added.
                currentEnemy.setGroundPosition(fallingGroundPosition); //set to fall below screen
            }
        }
    }


    public void updateItemFalling(List<IEntity> items)
    {
        for (int j = 0; j < items.Count; j++)
        {
            IItem item = (IItem)items[j];
            Rectangle itemBounds = item.GetDestination();
            bool itemColliding = false;
            for (int i = 0; i < collisionRects.Count; i++)
            {
                Rectangle blockBounds = collisionRects[i];
                if (itemBounds.Intersects(blockBounds))
                {
                    itemColliding = true;
                    if (itemBounds.Bottom > blockBounds.Top &&
                        itemBounds.Top < blockBounds.Top &&
                        itemBounds.Right > blockBounds.Left &&
                        itemBounds.Left < blockBounds.Right)
                    {
                        item.setGroundPosition(blockBounds.Top - 32);
                    }
                    else if (itemBounds.Right > blockBounds.Left &&
                             itemBounds.Left < blockBounds.Left)
                    {
                        item.swapDirection();
                    }
                    else if (itemBounds.Left < blockBounds.Right &&
                             itemBounds.Right > blockBounds.Right)
                    {
                        item.swapDirection();
                    }
                    break;
                }
            }

            if (!itemColliding)
            {
                if (itemBounds.Intersects(new Rectangle(itemBounds.X, 385, 16, 16)))
                {
                    item.setGroundPosition(480);
                }
            }
        }
    }

    public void updatePlayerFalling(IPlayer player)
    {
        PlayerStateMachine playerStateMachine = player.getStateMachine();
        Rectangle playerRect = player.GetDestination();
        Rectangle obstacleRect;
        bool playerIsColliding = false;

        // Track collision flags
        bool topCollision = false;
        bool bottomCollision = false;
        bool leftCollision = false;
        bool rightCollision = false;

        for (int i = 0; i < collisionRects.Count; i++)
        {
            obstacleRect = collisionRects[i];
            if (playerRect.Intersects(obstacleRect))
            {
                float overlapLeft = playerRect.Right - obstacleRect.Left;
                float overlapRight = obstacleRect.Right - playerRect.Left;
                float overlapTop = playerRect.Bottom - obstacleRect.Top;
                float overlapBottom = obstacleRect.Bottom - playerRect.Top;

                float minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight), Math.Min(overlapTop, overlapBottom));

                if (minOverlap == overlapTop && overlapTop > 0)
                {
                    topCollision = true;
                    player.SetPositionY(obstacleRect.Top - playerRect.Height);
                    player.SetVelocityY(0);
                    player.SetIsOnGround(true);
                    player.JumpStop();
                }
                else if (minOverlap == overlapBottom && overlapBottom > 0)
                {
                    bottomCollision = true;
                    player.SetPositionY(obstacleRect.Bottom);
                    player.SetVelocityY(0);
                }
                else if (minOverlap == overlapLeft && overlapLeft > 0)
                {
                    leftCollision = true;
                    player.Stop();
                    player.SetPositionX(obstacleRect.Left - playerRect.Width);
                    player.SetVelocityX(0);
                }
                else if (minOverlap == overlapRight && overlapRight > 0)
                {
                    rightCollision = true;
                    player.Stop();
                    player.SetPositionX(obstacleRect.Right);
                    player.SetVelocityX(0);
                }

                playerIsColliding = true;
            }
        }

        if (!topCollision && !player.GetIsOnGround())
        {
            player.Fall();
        }

        if (!playerIsColliding)
        {
            player.SetIsOnGround(false);
        }
    }

}