using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pixel_Plumbers_Fall_2024;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HudManager
{
    private HudCoins HudCoins;
    private HudLevel HudLevel;
    private HudLives HudLives;
    private HudScore HudScore;
    private HudTime HudTime;

    private SpriteFont _font;
    private Game1 _game;
    private Mario _mario;

    public HudManager(SpriteFont font, Game1 game, Mario mario)
    {
        this._font = font;
        this._game = game;
        this._mario = mario;
        //Initial values for hud elements
        this.HudCoins = new HudCoins(0, _font, _game, _mario);
        this.HudLevel = new HudLevel(1, 1, _font, _game, _mario);
        this.HudLives = new HudLives(3, _font, _game, _mario);
        this.HudScore = new HudScore(0, _font, _game, _mario);
        this.HudTime = new HudTime(400, _font, _game, _mario);
        _mario = mario;
    }

    public int GetTime()
    {
        return this.HudTime.GetTime();
    }

    public void SetTime(int newTime)
    {
        this.HudTime.SetTime(newTime);
    }

    public void AddScore(int scoreAmt)
    {
        bool scoreRollover = this.HudScore.AddScore(scoreAmt);

        if (scoreRollover)
        {
            this.HudLives.AddLife();
        }
    }

    public int GetScore()
    {
        return this.HudScore.GetScore();
    }

    public void SetScore(int scoreAmt)
    {
        this.HudScore.SetScore(scoreAmt);
    }

    public void ChangeWorld(int newWorldNum)
    {
        this.HudLevel.ChangeWorld(newWorldNum);
    }
    public void ChangeLevel(int newLevelNum)
    {
        this.HudLevel.ChangeLevel(newLevelNum);
    }

    public int GetLevel()
    {
        return this.HudLevel.GetLevel();
    }

    public int GetWorld()
    {
        return this.HudLevel.GetWorld();
    }

    public void CollectCoin()
    {
        bool coinRollover = this.HudCoins.CollectCoin();

        if (coinRollover)
        {
            this.HudCoins.SetNumCoins(0);
            this.HudLives.AddLife();
        }
    }

    public int GetNumCoins()
    {
        return this.HudCoins.GetNumCoins();
    }

    public void SetNumCoins(int newNumCoins)
    {
        this.HudCoins.SetNumCoins(newNumCoins);
    }
    public void Update(GameTime gameTime, FollowCamera camera)
    {
        this.HudCoins.Update(gameTime, camera);
        this.HudLevel.Update(gameTime, camera);
        this.HudLives.Update(gameTime, camera);
        this.HudScore.Update(gameTime, camera);
        this.HudTime.Update(gameTime, camera);
    }

    public void AddLife()
    {
        this.HudLives.AddLife();
    }

    public void LoseLife()
    {
        this.HudLives.LoseLife();
    }

    public int GetNumLives()
    {
        return this.HudLives.GetNumLives();
    }

    public void SetNumLives(int newNumLives)
    {
        this.HudLives.SetNumLives(newNumLives);
    }

    public void Draw(SpriteBatch sb)
    {
        this.HudCoins.Draw(sb);
        this.HudLevel.Draw(sb);
        this.HudLives.Draw(sb);
        this.HudScore.Draw(sb);
        this.HudTime.Draw(sb);
    }
}