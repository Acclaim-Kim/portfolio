using System;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class CheepsSprites
{
    private int LeftPosX1;
    private int LeftPosX2;
    private int RightPosX1;
    private int RightPosX2;
    private int posY = 0;
    private int position = 0;
    //0 is red, 1 is green, 2 is white
    public CheepsSprites(int color, int _posX, int _posY)
    {
        position = _posX;
        posY = _posY;
        switch (color)
        {
            case 0:
                LeftPosX1 = 0;
                LeftPosX2 = 30;
                RightPosX1 = 60;
                RightPosX2 = 90;
                break;
            case 1:
                LeftPosX1 = 120;
                LeftPosX2 = 150;
                RightPosX1 = 180;
                RightPosX2 = 210;
                break;
            case 2:
                LeftPosX1 = 240;
                LeftPosX2 = 270;
                RightPosX1 = 300;
                RightPosX2 = 330;
                break;
        }
    } 
    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int counter = -1;

    private const int countMod = 10;

    private const int SourceHeight = 184;
    private const int size = 16;
    private const int scaleUp = 2;
    private const int speed = 1;

    private int counter2 = 0;
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
        position = position - speed;
        if (counter % countMod < (countMod / 2))
        {
            sourceRectangle = new Rectangle(LeftPosX1, SourceHeight, size, size);
        }
        else
        {
            sourceRectangle = new Rectangle(LeftPosX2, SourceHeight, size, size);
        }
        destinationRectangle = new Rectangle(position, posY, size * scaleUp, size * scaleUp);
    }
    public void RightLogic()
    {
        counter++;
        position = position + speed;
        if (counter % countMod < (countMod / 2))
        {
            sourceRectangle = new Rectangle(RightPosX1, SourceHeight, size, size);
        }
        else
        {
            sourceRectangle = new Rectangle(RightPosX2, SourceHeight, size, size);
        }
        destinationRectangle = new Rectangle(position, posY, size * scaleUp, size * scaleUp);
    }
	public void StompedLogic()	{  }
    public void StartLogic()
    {
        sourceRectangle = new Rectangle(LeftPosX2, SourceHeight, size, size);
        destinationRectangle = new Rectangle(position, posY, size * scaleUp, size * scaleUp);
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
    public void FlippedLogic()
    {
        rotation = 3.1415926535f;
        counter++;
        if (counter % countMod < (countMod / 2))
        {
            sourceRectangle = new Rectangle(RightPosX1, SourceHeight, size, size);
        }
        else
        {
            sourceRectangle = new Rectangle(RightPosX2, SourceHeight, size, size);
        }
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
