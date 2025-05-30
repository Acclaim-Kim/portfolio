using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Star:IItem
{
    private Boolean idle;
    private Boolean collected;
    private Boolean roaming;
    private Boolean spawning;
    private Vector2 position;
    private StarPower sp;
    private SpriteBatch sB;
    private Texture2D texture;
    private Boolean movingLeft;
    private Boolean movingRight;
    private Boolean falling;
    private Vector2 velocity;
    private float gravity = 980f;
    private Rectangle destinationRectangle;
    private int yPositionCount;
    private int groundPosition = 380;


    public Star(SpriteBatch sb,Texture2D text, Vector2 position)
    {
        sp = new StarPower(texture);
        this.spawning = true;
        idle = true;
        collected = false;
        roaming = false;
        movingRight = true;
        sB = sb;
        texture = text;
        this.position = position;
        this.velocity = Vector2.Zero;
    }
    public void update(GameTime gameTime)
    {
        if (this.spawning)
        {
            falling = false;
            this.position.Y--;
            this.yPositionCount++;
            if (yPositionCount > 16)
            {
                this.spawning = false;
                this.roaming = true;
                this.movingRight = true;
            }
        }
        
        if (this.roaming)
        {
            falling = true;
            if (movingRight)
            {
                position.X++;
            }
            else if (movingLeft)
            {
                position.X--;
            }
        }
if (this.GetDestination().Y > this.groundPosition)
        {
            falling = false;
        }
        else
        {
            falling = true;
        }
        if (this.falling)
        {
            velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (!this.falling)
        {
            velocity.Y = 0;
        }

        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds; // Update position
        destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 31, 31);

    }
    public void draw()
    {
        if (this.collected)
        {

        }
        else if (this.idle)
        {
            sp = new StarPower(texture);
            sp.Draw(sB, position);
        }
        else if (this.roaming)
        {

        }
    }
    public Rectangle GetDestination()
    {
        return destinationRectangle;
    }
    public Vector2 currentPosition()
    {
        return this.position;
    }
    public void setGroundPosition(int newGroundPosition)
    {
        this.groundPosition = newGroundPosition;
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
    public void idling()
    {
        idle = true;
        collected = false;
        roaming = false;
    }
    public void collect()
    {

        collected = true;
        idle = false;
        roaming = false;

    }
    public void roams()
    {
        roaming = true;
        collected = false;
        idle = false;
    }


    public void destroy()
    {
        this.sp = null;
    }

    public void SetVelocityY(float velocityY)
    {
        velocity.Y = velocityY;
    }

    public void SetVelocityX(float velocityX)
    {
        velocity.X = velocityX;
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