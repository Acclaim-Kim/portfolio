using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class KoopaSprites
{
    public Rectangle sourceRectangle;
    public Rectangle destinationRectangle;
    public int counter = -1;
    public int counter2 = -1;
    public int counter3 = 0;
    public const int countMod = 10;
    public const int scaleUp = 2;
    public const int speed = 1;
    public const int width = 16;
    public const int height = 24;
    public float rotation = 0f;
    public int posY = 0;
    public int position = 0;
    private bool isOnGround = false;
    private float groundPosition = 385f;
    public KoopaSprites(int _posX, int _posY)
    {
        position = _posX;
        posY = _posY;
    }
    public bool GetIsOnGround()
    {
        return isOnGround;
    }
    public void SetIsOnGround(bool val)
    {
        isOnGround = val;
    }
    public void setGroundPosition(int pos)
    {
        this.groundPosition = pos - 20;
    }
    public void LeftLogic()
	{
        counter2 = -1;
        counter++;
        if (counter == 0)
        {
            sourceRectangle = new Rectangle(180, 0, width, height);
        }
        if (counter >= 10)
        {
            position = position - speed;
            if (counter % countMod < (countMod / 2))
            {
                sourceRectangle = new Rectangle(150, 0, width, height);
            }
            else
            {
                sourceRectangle = new Rectangle(180, 0, width, height);
            }
        }
        destinationRectangle = new Rectangle(position, posY, width * scaleUp, height * scaleUp);
    }
	public void RightLogic()
	{
        counter2 = -1;
        counter++;
        if (counter == 0)
        {
            sourceRectangle = new Rectangle(210, 0, width, height);
        }
        if (counter >= 10)
        {
            position = position + speed;
            if (counter % countMod < (countMod / 2))
            {
                sourceRectangle = new Rectangle(240, 0, width, height);
            }
            else
            {
                sourceRectangle = new Rectangle(210, 0, width, height);
            }
        }
        destinationRectangle = new Rectangle(position, posY, width * scaleUp, height * scaleUp);
    }
	public int StompedLogic()
	{
        counter2++;
        if (counter2 == 0)
        {
            sourceRectangle = new Rectangle(360, 5, 16, 15);
            destinationRectangle = new Rectangle(position, posY+30, 16 * scaleUp, 15 * scaleUp);
        }
        if (counter2 == 200)
        {
            sourceRectangle = new Rectangle(330, 4, 16, 15);
            destinationRectangle = new Rectangle(position, posY+30, 16 * scaleUp, 15 * scaleUp);
        }
        if (counter2 == 350)
        {
            counter = -1;
            return 1;
        }
        return 0;
    }
    public void StompedTwiceLogicLeft()
    {
        counter2 = -1;
        counter++;
        if (counter == 0)
        {
            sourceRectangle = new Rectangle(360, 5, width, 15);
        }
        if (counter >= 10)
        {
            position = position - 5*speed;
            if (counter % countMod < (countMod / 2))
            {
                sourceRectangle = new Rectangle(360, 5, width, 15);
            }
            else
            {
                sourceRectangle = new Rectangle(360, 5, width, 15);
            }
        }
        destinationRectangle = new Rectangle(position, posY + 30, width * scaleUp, 15 * scaleUp);
    }
    public void StompedTwiceLogicRight()
    {
        counter2 = -1;
        counter++;
        if (counter == 0)
        {
            sourceRectangle = new Rectangle(360, 5, width, 15);
        }
        if (counter >= 10)
        {
            position = position + 5*speed;
            if (counter % countMod < (countMod / 2))
            {
                sourceRectangle = new Rectangle(360, 5, width, 15);
            }
            else
            {
                sourceRectangle = new Rectangle(360, 5, width, 15);
            }
        }
        destinationRectangle = new Rectangle(position, posY + 30, width * scaleUp, 15 * scaleUp);
    }
    public void ApplyGravity()
    {
        if (!isOnGround)
        {
            if (counter % 10 == 0)
            {
                counter3++;
            }
            posY += counter3;
            if (posY >= groundPosition)
            {
                posY = (int)groundPosition;
                counter3 = 0;
                isOnGround = true;
            }
            destinationRectangle = new Rectangle(position, posY, width * scaleUp, 15 * scaleUp);
        }
    }
    public void StartLogic()
    {
        sourceRectangle = new Rectangle(180, 0, width, height);
        destinationRectangle = new Rectangle(position, posY, width * scaleUp, height * scaleUp);
    }
    public void FlippedLogic()
	{
        rotation = 3.1415926535f;
        sourceRectangle = new Rectangle(360, 5, width, 15);
        destinationRectangle = new Rectangle(position, posY+30, width * scaleUp, 15 * scaleUp);
    }
    public Rectangle GetDestination()
    {
        return destinationRectangle;
    }
    public void Draw(SpriteBatch sb, Texture2D Texture)
	{
        sb.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
    }
}