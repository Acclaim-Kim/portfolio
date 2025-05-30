using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;
using static System.Formats.Asn1.AsnWriter;

public class NormalWin
{
    Rectangle sourceRectangle;
    Rectangle destinationRectangle;
    int counter = 0;
    int widthCount = 0;
    int heightCount = 0;

    private const int coordsX = 31;
    private const int coordsY = 57;
    const int height = 244;
    const int width = 116;


    int Hchange = 372;
    int Wchange = 211;

    int cX = coordsX;
    int cY = coordsY;

    private Vector2 destination;

    public NormalWin(Vector2 position)
    {
        this.destination = position;
    }

    public void Updates()
    {
        if (counter == 0)
        {
            sourceRectangle = new Rectangle(coordsX, coordsY, width, height);
            destinationRectangle = new Rectangle(320, 90, width, height);
        }
        if (counter % 4 == 1)
        {
            if (widthCount != 4)
            {
                cX = cX + Wchange;
            }
            if (widthCount == 4)
            {
                cX = coordsX;
                widthCount = 0;
                heightCount++;
                cY = cY + Hchange;
            }
            sourceRectangle = new Rectangle(cX, cY, width, height);
            destinationRectangle = new Rectangle(320, 90, width, height);

            widthCount++;
        }
        if (heightCount % 2 == 1 && heightCount <= 6)
        {
            Hchange = 370;
        }
        if (heightCount % 2 == 0 && heightCount <= 6)
        {
            Hchange = 372;
        }
        if (heightCount > 15)
        {
            Hchange = 374;
        }
        if (heightCount == 26)
        {
            heightCount = 0;
            counter = -1;
            cX = coordsX;
            cY = coordsY;
            widthCount = 0;
            Hchange = 372;
        }
        counter++;
    }
    public Rectangle GetDestination()
    {
        return destinationRectangle;
    }

    public void Draw(SpriteBatch sb, Texture2D Texture)
    {
        sb.Draw(Texture, destination, sourceRectangle, Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
    }
}
