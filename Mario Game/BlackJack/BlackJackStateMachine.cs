using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;
using static System.Net.Mime.MediaTypeNames;

public class BlackJackStateMachine
{
    private enum BlackJackState { table, top };
    private BlackJackState _currentState = BlackJackState.table;
    private TextureManager textureManager;
    private BlackJackSprites _sprite;
    private SoundEffect fwip;
    private int stand = 0;
    private int numberOfStands = 0;

    private Texture2D TextureTable;
    private Texture2D TextureTop;
    private Texture2D TextureCards;
    private SpriteFont font;
    private int p1Score = 0;
    private int p2Score = 0;
    
    public BlackJackStateMachine(TextureManager textureManager, SoundEffect fwip, SpriteFont font)
    {

        this.textureManager = textureManager;

        this.TextureTable = textureManager.GetTexture("Table");
        this.TextureTop = textureManager.GetTexture("TableTop");
        this.TextureCards = textureManager.GetTexture("Cards");

        _sprite = new BlackJackSprites(TextureTable, TextureTop, TextureCards, font);
        this.fwip = fwip;

        this.font = font;
    }

    public void play()
    {
        _currentState = BlackJackState.top;
        Reset();
    }

    public void stop()
    {
        _currentState = BlackJackState.table;
    }

    public void Stand()
    {
        stand = 1;
        numberOfStands++;
        if (numberOfStands == 1)
        {
            fwip.Play();
            Thread.Sleep(200);
            playACard();
        }
    }
    public int StandNumber()
    {
        return numberOfStands;
    }
    public SoundEffect effect()
    {
        return fwip;
    }

    public void Reset()
    {
        numberOfStands = 0;
        stand = 0;
        _sprite = new BlackJackSprites(TextureTable, TextureTop, TextureCards, font);
    }

    public void playACard()
    {
        if (numberOfStands == 0)
        {
            p1Score = _sprite.cards(stand);
        }
        else
        {
            p2Score = _sprite.cards(stand);
        }
    }

    public void Update()
    {
        switch (_currentState)
        {
            case BlackJackState.table:
                _sprite.table();
                break;
            case BlackJackState.top:
                _sprite.tabletop();
                break;
        }
        if (stand == 1)
        {
            stand = 0;
        }
    }
    public Rectangle DestinationRectangle()
    {
        return _sprite.DestinationRectangle();
    }
    public void Draw(SpriteBatch sb)
    {
        _sprite.Draw(sb, numberOfStands);
    }
    public int Player1Score()
    {
        return p1Score;
    }

    public int Player2Score()
    {
        return p2Score;
    }
}
