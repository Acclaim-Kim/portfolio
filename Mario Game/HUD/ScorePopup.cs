using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pixel_Plumbers_Fall_2024;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class ScorePopup
{
    /*
     * When a fireball hits the ground, it bounces up a little bit
     * Use isBouncing and bounceTimer to track this
    */
    private int timer;
    private Vector2 pos;
    private const float scale = 0.75f;
    private int scoreAmt;
    private Game1 game;
    public ScorePopup(Vector2 marioPosition, Game1 game, int scoreAmt)
    {
        pos = marioPosition;
        timer = 0;
        this.game = game;
        game.scorePopups.Add(this);
        this.scoreAmt = scoreAmt;
    }
    private void Remove()
    {
        game.removedSP.Add(this);
    }

    private void Move()
    {
        pos.Y -= 1f;
    }
    public void Update(GameTime gameTime)
    {
        timer += 1;

        if (timer >= 25)
        {
            this.Remove();
        }
        else
        {
            this.Move();
        }
    }

    public void Draw(SpriteBatch sb, SpriteFont font)
    {
        sb.DrawString(font, scoreAmt.ToString(), new Vector2(pos.X, pos.Y), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
