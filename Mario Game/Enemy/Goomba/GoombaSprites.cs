using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class GoombaSprites
{
    private int posY = 0;
    private int position = 0;
    public GoombaSprites(int _posX, int _posY)
    {
        position = _posX;
        posY = _posY;
    }

    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int counter = -1;
    private int counter2 = 0;
    private int counter3 = 0;

    private const int countStart = 10;
    private const int countMod = 10;

    private const int size = 16;
    private const int scaleUp = 2;
    private const int speed = 1;
    private bool isOnGround = false;
    private float groundPosition = 385f;

    private float rotation = 0f;

    public bool GetIsOnGround()
    {
        return isOnGround;
    }
    public void SetIsOnGround(bool val)
    {
        isOnGround = val;
    }
    public void SetGroundPosition(float x)
    {
        groundPosition = x;
    }

    public void LeftLogic()
	{
        counter++;
        if (counter == 0)
        {
            sourceRectangle = new Rectangle(0, 4, size, size);
        }
        if (counter >= countStart)
        {
            position = position - speed;
            if (counter % countMod < (countMod / 2))
            {
                sourceRectangle = new Rectangle(30, 4, size, size);
            } else
            {
                sourceRectangle = new Rectangle(0, 4, size, size);
            }
        }
        destinationRectangle = new Rectangle(position, posY, size * scaleUp, size * scaleUp);
    }
	public void RightLogic()
	{
        counter++;
        if (counter >= countStart)
        {
            position += speed;
            if (counter % countMod < (countMod / 2))
            {
                sourceRectangle = new Rectangle(30, 4, size, size);
            }
            else
            {
                sourceRectangle = new Rectangle(0, 4, size, size);
            }
        }
        destinationRectangle = new Rectangle(position, posY, size * scaleUp, size * scaleUp);
    }
	public void StompedLogic()
	{
        counter3++;
        rotation = 0f;
        sourceRectangle = new Rectangle(60, 8, size, size/2);
        destinationRectangle = new Rectangle(position, posY+20, size * scaleUp, (size/2) * scaleUp);
        if (counter3 >= 100)
        {
            sourceRectangle = new Rectangle();
        }
    }
    public void ApplyGravity()
    {
        if (!isOnGround)
        {
            if (counter % 10 == 0)
            {
                counter2++;
            }
            posY += counter2;
            if (posY >= groundPosition)
            {
                posY = (int)groundPosition;
                counter2 = 0;
                isOnGround = true;
            }
            destinationRectangle = new Rectangle(position, posY, size * scaleUp, size * scaleUp);
        }
    }
    public void FlippedLogic(int flipOrStart)
	{
        if (flipOrStart == 0)
        {
            rotation = 3.1415926535f;
        }
        counter++;
        if (counter == 0)
        {
            sourceRectangle = new Rectangle(0, 4, size, size);
        }
        if (counter >= countStart)
        {
            if (counter % countMod < (countMod / 2))
            {
                sourceRectangle = new Rectangle(30, 4, size, size);
            }
            else
            {
                sourceRectangle = new Rectangle(0, 4, size, size);
            }
        }
        destinationRectangle = new Rectangle(position+size*scaleUp, posY+size*scaleUp, size * scaleUp, size * scaleUp);
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
