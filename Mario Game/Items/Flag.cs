using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


    public class Flag: IEntity
{
    FlagSprite flagSprite;
    Vector2 position;
    Rectangle destinationRectangle;
    //DancePole dance;
    Boolean isWinFlag = false;
    Boolean MarioTop = false;

    public Flag(Vector2 position, Texture2D flagTexture, Texture2D danceTexture, SpriteBatch spriteBatch)
    {
        this.position = position;
    
        flagSprite = new FlagSprite(spriteBatch, flagTexture,danceTexture, position);
    }
    public void Update()
    {
        if (this.isWinFlag)
        {
            flagSprite.update();
        }
        
    }
   public void Draw()
    {
        if (isWinFlag && MarioTop)
        {
            flagSprite.drawDanceFlag();
        }
        else if (isWinFlag && !MarioTop)
        {
            flagSprite.drawIdleFlag();
        }
        else
        {
            flagSprite.drawIdleFlag();
        }  
        
    }
    public Rectangle GetDestination()
    {
        return flagSprite.GetDestination();
    }
    public void makeWinFlag(int MarioPositionY)
    {
        Rectangle flagDest = GetDestination();
        isWinFlag = true;
        if (MarioPositionY < (flagDest.Y + 50))
        {
            MarioTop = true;
            
        }
    }
    public void resetFlag()
    {
        isWinFlag = false;
    }
}

