using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class BlackJackSprites
{
    Texture2D Texture;
    Texture2D TextureTable;
    Texture2D TextureTop;
    Texture2D TextureCards;
    private const float scale = 0.6f;
    Rectangle next = new Rectangle(0, 0, 0, 0);
    private List<Rectangle> cardRectangles = new List<Rectangle>();
    private CardScore player1;
    private CardScore player2;
    private int player1Score;
    private int player2Score;
    private SpriteFont font;
    
    public BlackJackSprites(Texture2D TextureTable, Texture2D TextureTop, Texture2D TextureCards, SpriteFont font)
    {
        cardRectangles.Add(next);
        Texture = TextureTable;
        this.TextureTable = TextureTable;
        this.TextureTop = TextureTop;
        this.TextureCards = TextureCards;
        destinationRectangle = new Rectangle(25, 375, 50, 46);
        player1 = new CardScore(0, font);
        player2 = new CardScore(1, font);
        this.font = font;
    }

    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private float rotation = 0f;
    private const float pi180 = 3.14159265f / 180f;

    //Card Sources
    private List<Rectangle> sourceCard = new List<Rectangle>(new Rectangle[22]);

    //Card Destinations
    private List<Rectangle> destinationCard = new List<Rectangle>(new Rectangle[22]);

    //Rotations
    private List<float> rotations = new List<float>(new float[22]);

    private int Cwidth = 107;
    private int Cheight = 170;
    private int CFinalWidth = 75;
    private int CFinalHeight = 110;
    private int cardCount = 0;

    private Random randomX = new Random();
    private Random randomY = new Random();
    private Random randomRotation = new Random();
    private Random negative = new Random();

    private void nextCard()
    {
        next = new Rectangle(0, 0, 0, 0);
        while (cardRectangles.Contains(next) && (cardRectangles.Count < 53))
        {
            next = new Rectangle(randomX.Next(13) * Cwidth, randomY.Next(4) * Cheight, Cwidth, Cheight);
        }
        rotation = randomRotation.Next(12) * pi180;
        if (negative.Next(2) == 1)
        {
            rotation = -rotation;
        }
        cardRectangles.Add(next);
    }

    public void table()
    {
        Texture = TextureTable;
        sourceRectangle = new Rectangle(0, 0, 248, 231);
        destinationRectangle = new Rectangle(25, 375, 50, 46);
    }
    public void tabletop()
    {
        Texture = TextureTop;
        sourceRectangle = new Rectangle(0, 0, 400, 282);
        destinationRectangle = new Rectangle(0, 0, 800, 480);
    }

    public int cards(int stand)
    {
        if (stand == 1)
        {
            if (cardCount < 11)
            {
                cardCount = 11;
            }
            else
            {
                cardCount = 22;
            }
        }
        nextCard();

        if (cardCount < 6)
        {
            destinationCard[cardCount] = new Rectangle(100 + (20 * cardCount), 150, CFinalWidth, CFinalHeight);
        }
        else if (cardCount >= 6 && cardCount < 11)
        {
            destinationCard[cardCount] = new Rectangle(105 + (20 * (cardCount - 6)), 180, CFinalWidth, CFinalHeight);
        }
        else if (cardCount <= 11 && cardCount < 17)
        {
            destinationCard[cardCount] = new Rectangle(220 + (20 * cardCount), 150, CFinalWidth, CFinalHeight);
        }
        else if (cardCount <= 17 && cardCount < 22)
        {
            destinationCard[cardCount] = new Rectangle(220 + (20 * cardCount - 6), 150, CFinalWidth, CFinalHeight);
        }

        if (cardCount < 22)
        {
            sourceCard[cardCount] = next;
            rotations[cardCount] = rotation;
        }

        if (cardCount < 11)
        {
            player1.scoreCalc(next.X);
            cardCount++;
            player1Score = player1.finalScore();
            return player1Score;
        }
        else if (cardCount >= 11 && cardCount < 22)
        {
            player2.scoreCalc(next.X);
            cardCount++;
            player2Score = player2.finalScore();
            return player2Score;
        }
        return 0;
    }

    public void Reset()
    {
        player1Score = 0;
        player2Score = 0;
    }

    public Rectangle DestinationRectangle()
    {
        return destinationRectangle;
    }

    public void Draw(SpriteBatch sb, int numberOfStands)
    {
        sb.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

        if (destinationRectangle.Width == 800)
        {
            sb.DrawString(font, "HIT", new Vector2(680, 170), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.DrawString(font, "STAND", new Vector2(660, 270), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            for (int i = 0; i < 22; i++)
            {
                sb.Draw(TextureCards, destinationCard[i], sourceCard[i], Color.White, rotations[i], new Vector2(Cwidth / 2, Cheight / 2), SpriteEffects.None, 0f);

            }

            if (cardCount < 11)
            {
                player1.Draw(sb, Color.GreenYellow, Color.PaleVioletRed);
                player2.Draw(sb, Color.GreenYellow, Color.PaleVioletRed);
            }
            else if (numberOfStands > 1)
            {
                if ((player1Score > player2Score && player1Score <= 21) || (player1Score <= 21 && player2Score > 21))
                {
                    sb.DrawString(font, "Loser:", new Vector2(490, 380), Color.PaleVioletRed, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    sb.DrawString(font, "Winner:", new Vector2(10, 380), Color.GreenYellow, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    player1.Draw(sb, Color.GreenYellow, Color.PaleVioletRed);
                    player2.Draw(sb, Color.GreenYellow, Color.PaleVioletRed);
                }
                else if ((player2Score > player1Score && player2Score <= 21) || (player2Score <= 21 && player1Score > 21))
                {
                    sb.DrawString(font, "Winner:", new Vector2(490, 380), Color.GreenYellow, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    sb.DrawString(font, "Loser:", new Vector2(10, 380), Color.PaleVioletRed, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    player1.Draw(sb, Color.PaleVioletRed, Color.GreenYellow);
                    player2.Draw(sb, Color.PaleVioletRed, Color.GreenYellow);
                }
                else if (player1Score == player2Score && player1Score <= 21 && player2Score <= 21)
                {
                    sb.DrawString(font, "Winner:", new Vector2(490, 380), Color.GreenYellow, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    sb.DrawString(font, "Winner:", new Vector2(10, 380), Color.GreenYellow, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    player1.Draw(sb, Color.GreenYellow, Color.GreenYellow);
                    player2.Draw(sb, Color.GreenYellow, Color.GreenYellow);
                }
                else
                {
                    sb.DrawString(font, "Loser:", new Vector2(490, 380), Color.PaleVioletRed, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    sb.DrawString(font, "Loser:", new Vector2(10, 380), Color.PaleVioletRed, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    player1.Draw(sb, Color.PaleVioletRed, Color.PaleVioletRed);
                    player2.Draw(sb, Color.PaleVioletRed, Color.PaleVioletRed);
                }
            }
            else if (numberOfStands == 1 && player1Score >= 21)
            {
                if (player1Score == 21)
                {
                    sb.DrawString(font, "Winner:", new Vector2(10, 380), Color.GreenYellow, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    player1.Draw(sb, Color.GreenYellow, Color.GreenYellow);
                    player2.Draw(sb, Color.GreenYellow, Color.GreenYellow);
                }
                else
                {
                    sb.DrawString(font, "Loser:", new Vector2(10, 380), Color.PaleVioletRed, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    player1.Draw(sb, Color.PaleVioletRed, Color.GreenYellow);
                    player2.Draw(sb, Color.PaleVioletRed, Color.GreenYellow);
                }
            }
            else
            {
                player1.Draw(sb, Color.PaleVioletRed, Color.GreenYellow);
                player2.Draw(sb, Color.PaleVioletRed, Color.GreenYellow);
            }
        }
    }
}