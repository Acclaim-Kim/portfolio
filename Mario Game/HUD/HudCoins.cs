using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Pixel_Plumbers_Fall_2024;

public class HudCoins : IHudElement
{
    private const float scale = 0.6f;
    private Vector2 screenPos;
    private int numCoins;
    private SpriteFont _font;
    public HudCoins(int startCoins, SpriteFont font, Game1 game, Mario mario)
    {
        numCoins = startCoins;
        _font = font;
    }

    public bool CollectCoin()
    {
        numCoins += 1;
        //Rollover number of coins from 100 to 0
        //If 100 coins collected, return true
        //HudManager will read this to update HudLives
        if (numCoins >= 100)
        {
            numCoins = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetNumCoins()
    {
        return numCoins;
    }

    public void SetNumCoins(int newNumCoins)
    {
        numCoins = newNumCoins;
    }
    public void Update(GameTime gameTime, FollowCamera camera)
    {
        screenPos = camera.position;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.DrawString(_font, "COINS:", new Vector2(screenPos.X + 170, screenPos.Y + 10), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        sb.DrawString(_font, numCoins.ToString("D2"), new Vector2(screenPos.X + 170, screenPos.Y + 40), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
