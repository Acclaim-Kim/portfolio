using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class BlooperSprites
{
    private int posY = 0;
    private int position = 0;
    public BlooperSprites(int _posX, int _posY)
    {
        position = _posX;
        posY = _posY;
    }

    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int counterLR = 0;
    private int counter = 0;

    private const int height = 16;
    private int width = 16;
    private const int scaleUp = 2;
    private const int speed = 1;

    private int minFall = 336;
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
    public void Rise()
    {
        width = 24;
        sourceRectangle = new Rectangle(420, 0, height, width);
        posY -= 1;
        if (counterLR == 1)
        {
            position -= speed;
        }
        else if (counterLR == 2)
        {
            position += speed;
        }

        destinationRectangle = new Rectangle(position, posY, height * scaleUp, width * scaleUp);
    }

    public void Fall()
    {
        width = 16;
        sourceRectangle = new Rectangle(390, 4, height, width);
        posY += 1;
        destinationRectangle = new Rectangle(position, posY, height * scaleUp, width * scaleUp);
    }

    public int YVal()
    {
        return posY;
    }

    public void Idle(int riseMore)
    {
        counter++;
        int posYBalance = 170-(minFall-posY) + riseMore;

        if (counter < posYBalance)
        {
            Rise();
        }
        else
        {
            Fall();
        }
        if (counter >= 170)
        {
            counter = 0;
        }
        destinationRectangle = new Rectangle(position, posY, height * scaleUp, width * scaleUp);
    }

    public void LeftLogic(int riseMore)
    {
        counterLR = 1;
        Idle(riseMore);
        counterLR = 0;
    }
    public void RightLogic(int riseMore)
    {
        counterLR = 2;
        Idle(riseMore);
        counterLR = 0;
    }
    public void StompedLogic() { }
    public void StartLogic()
    {
        sourceRectangle = new Rectangle(420, 0, height, width);
        destinationRectangle = new Rectangle(position, posY, height * scaleUp, width * scaleUp);
    }
    public void FlippedLogic()
    {
        rotation = 3.1415926535f;
        width = 24;
        sourceRectangle = new Rectangle(420, 0, height, width);
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
            destinationRectangle = new Rectangle(position, posY, width * scaleUp, height * scaleUp);
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
