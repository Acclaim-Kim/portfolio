using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Audio;
using System.Net.NetworkInformation;
using System.Diagnostics;
using Pixel_Plumbers_Fall_2024;

public class LevelOne : ILevel
{
    private Game1 game;
    private Mario mario;
    private Luigi luigi;
    private TextureManager textureManager;
    private GameStateMachine gameStateMachine;
    private ContentManager Content;

    private List<IEntity> entities = new List<IEntity>();
    private List<IEntity> entitiesRemoved;

    private SpriteBatch spriteBatch;
    private Texture2D EnemyTexture;
    private Texture2D blockTexture;
    private Texture2D ItemsTexture;
    private Texture2D overworldTiles;

    private Texture2D danceTexture;
    // Box was used to test where destination rectangles are
    private Texture2D box;

    public Point mapSize = new(211 * 32, 30 * 32);
    private Layer lvl1backdrop;
    private Layer lvl1greenery;
    private Layer lvl1foreground;

    private List<ISpriteEnemy> Enemy = new List<ISpriteEnemy>();
    private List<IBlock> LuckyBlocks = new List<IBlock>();
    private List<IBlock> BrickBlocks = new List<IBlock>();
    private IBlock OWBrokenBrickSprite;

    private Flag flag;
    private GameTime gameTime;

    public LevelOne(
        Game1 game,
        Mario mario,
        Luigi luigi,
        List<IEntity> entities,
        List<IEntity> entitiesRemoved,
        SpriteBatch spriteBatch,
        GameTime gameTime,
        ContentManager Content,
        TextureManager textureManager,
        GameStateMachine gameStateMachine)
    {
        this.textureManager = textureManager;
        this.EnemyTexture = textureManager.GetTexture("Enemy");
        this.blockTexture = textureManager.GetTexture("Block");
        this.ItemsTexture = textureManager.GetTexture("Items");
        this.overworldTiles = textureManager.GetTexture("OverworldTiles");
        this.danceTexture = textureManager.GetTexture("dance");
        box = textureManager.GetTexture("box");

        this.gameStateMachine = gameStateMachine;
        this.entities = entities;
        this.mario = mario;
        this.luigi = luigi;
        this.spriteBatch = spriteBatch;
        this.game = game;
        this.gameTime = gameTime;
        this.entitiesRemoved = entitiesRemoved;
        this.Content = Content;
    }

    public void InitializeLevel()
    {
        flag = new Flag(new Vector2(6335, 354), overworldTiles, danceTexture, spriteBatch);

        Enemy = new List<ISpriteEnemy>();
        LuckyBlocks = new List<IBlock>();
        BrickBlocks = new List<IBlock>();
        

        InitializeEnemies();
        InitializeLuckyBlocks();
        InitializeBrickBlocks();
        AddEntitiesToGame();
        mario.SetSwimmingLevel(true);
        luigi.SetSwimmingLevel(false);
        LoadLevelLayers();
    }

    private void InitializeEnemies()
    {
        Enemy.Add(new Koopa(3350, 365, mario, luigi));

        int[] enemyXPositions = { 535, 1400, 1700, 1750, 2490, 2600, 3000, 3150, 3520, 3720, 4000, 4050, 4110, 4160, 5400, 5550 };
        int[] enemyYPositions = { 385, 385, 385, 385, 215, 105, 385, 385, 385, 385, 385, 385, 385, 385, 385, 385 };

        for (int i = 0; i < enemyXPositions.Length; i++)
        {
            Enemy.Add(new Goomba(enemyXPositions[i], enemyYPositions[i], (IPlayer)mario, (IPlayer)luigi));
        }
    }

    private void InitializeLuckyBlocks()
    {
        Vector2[] luckyBlockPositions = {
            new Vector2(736, 288), new Vector2(672, 288), new Vector2(704, 160),
            new Vector2(512, 288), new Vector2(2468, 288), new Vector2(3396, 288),
            new Vector2(3492, 288), new Vector2(3492, 160), new Vector2(3588, 288),
            new Vector2(4132, 160), new Vector2(4164, 160), new Vector2(3012, 160),
            new Vector2(5444, 288)
        };

        foreach (var position in luckyBlockPositions)
        {
            LuckyBlocks.Add(new LuckyBlockSprite(
                blockTexture,
                spriteBatch,
                ItemsTexture,
                game,
                mario,
                position
            ));
        }
    }

    private void InitializeBrickBlocks()
    {
        Vector2[] brickBlockPositions = {
            new Vector2(768, 288), new Vector2(704, 288), new Vector2(640, 288),
            new Vector2(2436, 288), new Vector2(2500, 288), new Vector2(2532, 160),
            new Vector2(2564, 160), new Vector2(2596, 160), new Vector2(2628, 160),
            new Vector2(2660, 160), new Vector2(2692, 160), new Vector2(2724, 160),
            new Vector2(2756, 160), new Vector2(2788, 160), new Vector2(2916, 160),
            new Vector2(2948, 160), new Vector2(2980, 160), new Vector2(3012, 288),
            new Vector2(3204, 288), new Vector2(3236, 288), new Vector2(3780, 288),
            new Vector2(3876, 160), new Vector2(3908, 160), new Vector2(3940, 160),
            new Vector2(4100, 160), new Vector2(4192, 160), new Vector2(4132, 288),
            new Vector2(4164, 288), new Vector2(3876, 160), new Vector2(3908, 160),
            new Vector2(5380, 288), new Vector2(5412, 288), new Vector2(5476, 288)
        };

        for (int i = 0; i < brickBlockPositions.Length; i++)
        {
            BrickBlocks.Add(new BrokenBrickSprite(blockTexture, 4, 0.1));
        }
    }

    private void AddEntitiesToGame()
    {
        foreach (var e in Enemy)
        {
            entities.Add(e);
        }
        foreach (var lucky in LuckyBlocks)
        {
            entities.Add(lucky);
        }
        foreach (var brick in BrickBlocks)
        {
            entities.Add(brick);
        }

        entities.Add(flag);
        entities.Add(mario);
        entities.Add(luigi);
    }

    private void LoadLevelLayers()
    {
        lvl1backdrop = new Layer(32, 16, 16, Content.RootDirectory + "/level1_Backdrop.csv");
        lvl1greenery = new Layer(32, 16, 16, Content.RootDirectory + "/level1_Greenery.csv");
        lvl1foreground = new Layer(32, 16, 16, Content.RootDirectory + "/level1_Foreground.csv");

        lvl1backdrop.LoadLayer();
        lvl1greenery.LoadLayer();
        lvl1foreground.LoadLayer();
    }

    public void UpdateLevel(GameTime gameTime)
    {
        mario.SetSwimmingLevel(false);
        luigi.SetSwimmingLevel(false);

        if (gameStateMachine.isMultiplayer())
        {
            mario.SetIsOnGround(false);
            mario.Update(gameTime);
            luigi.SetIsOnGround(false);
            luigi.Update(gameTime);
        }
        else if (gameStateMachine.isSingleplayer())
        {
            mario.SetIsOnGround(false);
            mario.Update(gameTime);
        }

        for (int i = 0; i < Enemy.Count; i++)
        {
            Enemy[i].Updates();
        }

        for (int i = 0; i < LuckyBlocks.Count; i++)
        {
            LuckyBlocks[i].Update(gameTime);
        }

        for (int i = 0; i < BrickBlocks.Count; i++)
        {
            BrickBlocks[i].Update(gameTime);
        }

        flag.Update();
        if (mario.LevelOneDone())
        {
            gameStateMachine.setLevelTwo();
            mario.Reset();
            luigi.Reset();
            
        }

    }

    public void DrawLevel(SpriteBatch spriteBatch)
    {
        lvl1backdrop.Draw(spriteBatch, overworldTiles, Vector2.Zero);
        lvl1greenery.Draw(spriteBatch, overworldTiles, Vector2.Zero);
        lvl1foreground.Draw(spriteBatch, overworldTiles, Vector2.Zero);

        flag.Draw();

        if (gameStateMachine.isMultiplayer())
        {
            luigi.Draw(spriteBatch);
            mario.Draw(spriteBatch);
        }
        else if (gameStateMachine.isSingleplayer())
        {
            mario.Draw(spriteBatch);
        }

        for (int i = 0; i < Enemy.Count; i++)
        {
            Enemy[i].Draw(spriteBatch, EnemyTexture);
        }

        for (int i = 0; i < LuckyBlocks.Count; i++)
        {
            LuckyBlocks[i].Draw(spriteBatch, new Vector2(69, 69));
        }

        DrawBrickBlocks(spriteBatch);
    }

    private void DrawBrickBlocks(SpriteBatch spriteBatch)
    {
        Vector2[] brickBlockPositions = {
            new Vector2(768, 288), new Vector2(704, 288), new Vector2(640, 288),
            new Vector2(2436, 288), new Vector2(2500, 288), new Vector2(2532, 160),
            new Vector2(2564, 160), new Vector2(2596, 160), new Vector2(2628, 160),
            new Vector2(2660, 160), new Vector2(2692, 160), new Vector2(2724, 160),
            new Vector2(2756, 160), new Vector2(2788, 160), new Vector2(2916, 160),
            new Vector2(2948, 160), new Vector2(2980, 160), new Vector2(3012, 288),
            new Vector2(3204, 288), new Vector2(3236, 288), new Vector2(3780, 288),
            new Vector2(3876, 160), new Vector2(3908, 160), new Vector2(3940, 160),
            new Vector2(4100, 160), new Vector2(4192, 160), new Vector2(4132, 288),
            new Vector2(4164, 288), new Vector2(3876, 160), new Vector2(3908, 160),
            new Vector2(5380, 288), new Vector2(5412, 288), new Vector2(5476, 288)
        };

        for (int i = 0; i < BrickBlocks.Count; i++)
        {
            BrickBlocks[i].Draw(spriteBatch, brickBlockPositions[i]);
        }
    }

    public List<IEntity> GetAllEntities()
    {
        return new List<IEntity>(entities);
    }

    public List<Rectangle> GetLevelFloorRectangles()
    {
        return lvl1foreground.GetRedRectangles();
    }
}