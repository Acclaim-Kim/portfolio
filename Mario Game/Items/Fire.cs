using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pixel_Plumbers_Fall_2024;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Fire : IItem
{
    public Boolean idle;
    public Boolean collected;
    public Boolean roaming;
    
    private Microsoft.Xna.Framework.Vector2 position;
    private int x;
    private int y;
    private FirePower fp;
    private SpriteBatch sB;
    private Texture2D texture;
    private Boolean movingLeft;
    private Boolean movingRight;
    private Boolean falling;
    private int groundPosition;

    public Fire(SpriteBatch sb, Texture2D text, Vector2 pos)
    {
        fp = new FirePower(sb, texture, pos);
        idle = true;
        collected = false;
        roaming = false;
        sB = sb;
        texture = text;
        position = pos;
        movingLeft = false;
        movingRight = false;
    }
    public void idling()
    {
        idle = true;
        roaming = false;
        collected = false;
    }
    public void collect()
    {
        collected = true;
        roaming = false;
        idle = false;
    }
    public void roams()
    {
        roaming = true;
        collected = false;
        idle = false;
    }
    public void draw()
    {
         if (this.collected)
        {

        }
        else if(this.idle) {
            fp = new FirePower(sB, texture, position);
            fp.draw();
        }
        else if (this.roaming)
        {

        }
    }
    public void update(GameTime gameTime)
    {
        if (this.roaming)
        {
            if (movingRight)
            {
                position.X++;
            }
            else if (movingLeft)
            {
                position.X--;
            }
        }
    }
    public void swapDirection()
    {
        if (movingLeft)
        {
            movingLeft = false;
            movingRight = true;
        }
        else if (movingRight)
        {
            movingRight = false;
            movingLeft = true;
        }
    }
    public void destroy()
    {
     
            this.fp = null;
    
    }
    
    

    public Rectangle GetDestination()
    {
        return this.fp.GetDestination();
    }
    public Vector2 currentPosition()
    {
        return this.position;
    }
    public void setGroundPosition(int groundPos)
    {
        this.groundPosition = groundPos;
    }
    public bool isFalling()
    {
        return falling;
    }
    public void NotFalling()
    {
        falling = false;
    }
    public void MakeFalling()
    {
        falling = true;
    }

    public void SetVelocityY(float velocityY)
    {
        //velocity.Y = velocityY;
    }

    public void SetVelocityX(float velocityX)
    {
        //velocity.X = velocityX;
    }
    public void SetPositionY(float positionY)
    {
        position.Y = positionY;
    }
    public void SetPositionX(float positionX)
    {
        position.X = positionX;
    }
    public void SetIsOnGround(bool isGround)
    {
        //isOnGround = isGround;
    }
    public void ApplyGravity(GameTime gameTime)
    {

    }
    public bool GetIsOnGround()
    {
        return true;
    }
}