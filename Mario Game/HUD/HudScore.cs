using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Formats.Asn1.AsnWriter;
using Pixel_Plumbers_Fall_2024;

public class HudScore : IHudElement
{
    private const float scale = 0.6f;
    private Vector2 screenPos;
    private int numScore;
    private SpriteFont _font;
    private const int maxScoreAmt = 10000;
    public HudScore(int startScore, SpriteFont font, Game1 game, Mario mario)
    {
        numScore = startScore;
        this._font = font;
    }

    public bool AddScore(int scoreAmt)
    {
        if (scoreAmt >= maxScoreAmt)
        {
            return true;
        }
        numScore += scoreAmt;
        return false;
    }

    public int GetScore()
    {
        return numScore;
    }
    public void SetScore(int scoreAmt)
    {
        numScore = scoreAmt;
    }
    public void Update(GameTime gameTime, FollowCamera camera)
    {
        screenPos = camera.position;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.DrawString(_font, "SCORE:", new Vector2(screenPos.X + 490, screenPos.Y + 10), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        sb.DrawString(_font, numScore.ToString("D8"), new Vector2(screenPos.X + 490, screenPos.Y + 40), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
