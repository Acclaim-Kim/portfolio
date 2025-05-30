using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pixel_Plumbers_Fall_2024;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

public class HudLives : IHudElement
{
    private const float scale = 0.6f;
    private Vector2 screenPos;
    private int numLives;
    private SpriteFont _font;
    public HudLives(int startLives, SpriteFont font, Game1 game, Mario mario)
    {
        numLives = startLives;
        _font = font;
    }

    public void AddLife()
    {
        numLives += 1;
    }

    public void LoseLife()
    {
        numLives -= 1;
    }

    public int GetNumLives()
    {
        return numLives;
    }

    public void SetNumLives(int newNumLives)
    {
        numLives = newNumLives;
    }
    public void Update(GameTime gameTime, FollowCamera camera)
    {
        screenPos = camera.position;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.DrawString(_font, "LIVES:", new Vector2(screenPos.X + 10, screenPos.Y + 10), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        sb.DrawString(_font, numLives.ToString(), new Vector2(screenPos.X + 10, screenPos.Y + 40), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
