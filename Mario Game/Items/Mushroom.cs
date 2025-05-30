using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;

public class Mushroom :IItem
{
    public Boolean idle;
    public Boolean collected;
    public Boolean roaming;
    private Boolean spawning;
    private Boolean movingRight;
    private Boolean movingLeft;
    private Vector2 position;
    private MushroomPower mp;
    private SpriteBatch sb;
    private Texture2D texture;
    private bool falling;
    private Vector2 velocity;
    private float gravity = 980f;
    private Rectangle destinationRectangle;
    private int yPositionCount;
    private int groundPosition = 380;   
    


    public  Mushroom(SpriteBatch sB, Texture2D texture, Vector2 m_position)
    {
        mp = new MushroomPower(texture);
        this.spawning = true;
        this.idle = true;
        this.collected = false;
        this.roaming = true;
        this.falling = true;
        this.texture = texture;
        this.sb = sB;
        movingRight = true;
        this.velocity.X = 0.5f;
        this.velocity.Y = 0;
        this.position = m_position;
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
        idle = false;
        roaming = false;
        movingRight = true;
        movingLeft = false;
       
    }
    public void update(GameTime gameTime)
    {
        /*
        if (this.spawning)
        {
            position.Y--;
           yPositionCount++;
            falling = false;
            if (yPositionCount > 16)
            {
                spawning = false;
                roaming = true;
                movingRight = true;
            }
        }
        */
        ApplyGravity(gameTime);

        if (this.roaming)
        {
            position.X += movingRight ? velocity.X : -velocity.X;
        }
        if(this.GetDestination().Y> this.groundPosition)
        {
            falling = false;
        }
        else
        {
            falling = true;
        }

        
        destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 31, 31);

    }
    public void draw()
    {
        if (this.idle || this.roaming)
        {
            this.mp = new MushroomPower(texture);
            
            this.mp.Draw(sb, position);
            
        }
        else if (this.collected)
        {
            position = new Microsoft.Xna.Framework.Vector2(900, 900);
            
        }
    }

    public void swapDirection()
    {
        if (movingLeft){
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
            this.mp = null;
      
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

    public void ApplyGravity(GameTime gameTime)
    {

        if (falling)
        {
            velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.Y >= groundPosition)
            {
                position.Y = groundPosition;
                velocity.Y = 0;
                falling = false;
            }
        }
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
        
    }
    public void ForceGravity(GameTime gameTime)
    {
        velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        position.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public bool GetIsOnGround()
    {
        return true;
    }
}