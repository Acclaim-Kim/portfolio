using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;
using static System.Formats.Asn1.AsnWriter;

public class CardScore
{
    private SpriteFont _font;
    private const float scale = 0.6f;
    private int player;
    public CardScore(int player, SpriteFont font)
    {
        this.player = player;
        this._font = font;
    }

    private int playerScoreOne = 0;
    private int playerScoreTwo = 0;
    private int displayScore = 0;
    private int Cwidth = 107;

    public void scoreCalc(int Xval)
    {
        Xval = (Xval / Cwidth);
        switch (Xval)
        {
            case 0:
                if (playerScoreOne != playerScoreTwo)
                {
                    playerScoreTwo = playerScoreOne;
                }
                playerScoreOne++;
                playerScoreTwo += 11;
                break;
            case 1:
                playerScoreOne += 2;
                playerScoreTwo += 2;
                break;
            case 2:
                playerScoreOne += 3;
                playerScoreTwo += 3;
                break;
            case 3:
                playerScoreOne += 4;
                playerScoreTwo += 4;
                break;
            case 4:
                playerScoreOne += 5;
                playerScoreTwo += 5;
                break;
            case 5:
                playerScoreOne += 6;
                playerScoreTwo += 6;
                break;
            case 6:
                playerScoreOne += 7;
                playerScoreTwo += 7;
                break;
            case 7:
                playerScoreOne += 8;
                playerScoreTwo += 8;
                break;
            case 8:
                playerScoreOne += 9;
                playerScoreTwo += 9;
                break;
            case 9:
                playerScoreOne += 10;
                playerScoreTwo += 10;
                break;
            case 10:
                playerScoreOne += 10;
                playerScoreTwo += 10;
                break;
            case 11:
                playerScoreOne += 10;
                playerScoreTwo += 10;
                break;
            case 12:
                playerScoreOne += 10;
                playerScoreTwo += 10;
                break;
        }
        if (playerScoreTwo >= playerScoreOne && playerScoreTwo <= 21)
        {
            displayScore = playerScoreTwo;
        } else
        {
            displayScore = playerScoreOne;
        }
    }

    public int finalScore()
    {
        return displayScore;
    }

    public void Draw(SpriteBatch sb, Color colorP1, Color colorP2)
    {
        if (player == 0)
        {
            sb.DrawString(_font, "Player 1 SCORE:", new Vector2(10, 400), colorP1, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.DrawString(_font, displayScore.ToString(), new Vector2(10, 420), colorP1, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
        else
        {
            sb.DrawString(_font, "Player 2 SCORE:", new Vector2(490, 400), colorP2, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.DrawString(_font, displayScore.ToString(), new Vector2(490, 420), colorP2, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }


}
