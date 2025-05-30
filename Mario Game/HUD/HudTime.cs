using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Pixel_Plumbers_Fall_2024;

public class HudTime : IHudElement
{
    private const float scale = 0.6f;
    private Vector2 screenPos;
    private int currTime;
    private float gameTicks;
    private SpriteFont _font;
    private Game1 _game;
    private Mario _mario;
    public HudTime(int startTime, SpriteFont font, Game1 game, Mario mario)
    {
        _game = game;
        currTime = startTime;
        _font = font;
        _mario = mario;
    }

    public void SetTime(int newTime)
    {
        currTime = newTime;
    }

    public int GetTime() 
    {
        return currTime;
    }
    public void Update(GameTime gameTime, FollowCamera camera)
    {
        screenPos = camera.position;

        this.gameTicks += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (currTime <= 0)
        {
            currTime = 0;
            _mario.MarioDeath();
        }

        if (this.gameTicks > 1000)
        {
            currTime -= 1;
            this.gameTicks = 0;
        }
        
    }

    public void Draw(SpriteBatch sb)
    {
        sb.DrawString(_font, "TIME:", new Vector2(screenPos.X + 650, screenPos.Y + 10), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        sb.DrawString(_font, currTime.ToString(), new Vector2(screenPos.X + 650, screenPos.Y + 40), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
