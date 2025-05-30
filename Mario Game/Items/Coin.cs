using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


    public class Coin: IEntity
{
    SpriteBatch spriteBatch;
    CoinInstance coinInstance;
    Microsoft.Xna.Framework.Vector2 position;
    Texture2D texture;
    Boolean isRemoved;
    private int ticks;
    public Coin(SpriteBatch sB, Texture2D texture, Microsoft.Xna.Framework.Vector2 position)
    {
        this.spriteBatch = sB;
        this.texture = texture;
        this.position = position;
        coinInstance = new CoinInstance(texture, position);
        isRemoved = false;

    }
    public void Draw()
    {
        if (!isRemoved)
        {
            coinInstance.Draw(spriteBatch, new Microsoft.Xna.Framework.Vector2(position.X, position.Y));
        }
       
    }
    public void Update(GameTime gameTime)
    {
        if (ticks < 10)
        {
            ticks++;
        }else if (ticks == 10)
        {
            isRemoved = true;
        }
        coinInstance.Update(gameTime);
    }
    public Rectangle GetDestination()
    {
        return coinInstance.GetDestination();
    }
}

