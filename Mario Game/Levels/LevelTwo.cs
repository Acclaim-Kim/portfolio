using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixel_Plumbers_Fall_2024;


public class LevelTwo : ILevel


{
    private Game1 game;
    private Mario mario;
    private Luigi luigi;
    private TextureManager textureManager;
    private GameStateMachine gameStateMachine;
    private ContentManager Content;

    private List<IEntity> entities = new List<IEntity>();
    private List<ISpriteEnemy> Enemy = new List<ISpriteEnemy>();
    private List<IEntity> entitiesRemoved;
    private List<IEntity> floorRectangles;

    private SpriteBatch spriteBatch;
    private Texture2D EnemyTexture;
    private Texture2D blockTexture;
    private Texture2D obstacleTexture;
    private Texture2D ItemsTexture;
    private Texture2D overworldTiles;
    private Texture2D underwaterTiles;
    private double startAnimationElapsedTime = 0;

    public Point mapSize = new(192 * 32, 30 * 32);
    private Layer lvl2backdrop1;
    private Layer lvl2backdrop2;
    private Layer lvl2greenery;
    private Layer lvl2foreground1;
    private Layer lvl2foreground2;
    private FlagSprite flagSprite;
    private FollowCamera followCamera;

    Boolean startAnimation = false;
    Boolean upPipeAnimation = false;
    bool swimming = false;

    public LevelTwo(
        Game1 game,
        Mario mario,
        Luigi luigi,
        List<IEntity> entities,
        List<IEntity> entitiesRemoved,
        SpriteBatch spriteBatch,
        GameTime gameTime,
        ContentManager Content,
        TextureManager textureManager,
        GameStateMachine gameStateMachine,
        FollowCamera followCamera
    )

    {
        this.textureManager = textureManager;
        this.EnemyTexture = textureManager.GetTexture("Enemy");
        this.blockTexture = textureManager.GetTexture("Block");
        this.ItemsTexture = textureManager.GetTexture("Items");
        this.overworldTiles = textureManager.GetTexture("OverworldTiles");
        this.underwaterTiles = textureManager.GetTexture("UnderwaterTiles");
        this.followCamera = followCamera;
        this.gameStateMachine = gameStateMachine;
        this.entities = entities;
        this.mario = mario;
        this.luigi = luigi;
        this.spriteBatch = spriteBatch;
        this.game = game;
        this.entitiesRemoved = entitiesRemoved;
        this.Content = Content;
    }

    public void InitializeLevel()
    {
        entities.Add(mario);
        entities.Add(luigi);

        lvl2backdrop1 = new Layer(32, 16, 16, Content.RootDirectory + "/level2_OWBackdrop.csv");
        lvl2backdrop2 = new Layer(32, 16, 16, Content.RootDirectory + "/level2_UWBackdrop.csv");
        lvl2greenery = new Layer(32, 16, 16, Content.RootDirectory + "/level2_OWGreenery.csv");
        lvl2foreground1 = new Layer(32, 16, 16, Content.RootDirectory + "/level2_OWForeground.csv");
        lvl2foreground2 = new Layer(32, 16, 16, Content.RootDirectory + "//level2_UWForeground.csv");

        lvl2backdrop1.LoadLayer();
        lvl2backdrop2.LoadLayer();
        lvl2greenery.LoadLayer();
        lvl2foreground1.LoadLayer();
        lvl2foreground2.LoadLayer();
        startAnimation = false;
        upPipeAnimation = false;
        bool swimming = false;

        Enemy = new List<ISpriteEnemy>();
        InitializeEnemies();
        AddEntitiesToGame();
    }

    private void InitializeEnemies()
    {

        int[] BenemyXPositions = {390, 1500, 1700, 2650, 2900, 3250};
        int[] BenemyYPositions = {700, 720, 720, 710, 720, 720};

        for (int i = 0; i < BenemyXPositions.Length; i++)
        {
            Enemy.Add(new Blooper(BenemyXPositions[i], BenemyYPositions[i], (IPlayer)mario, (IPlayer)luigi));
        }

        int[] CenemyXPositions2 = { 2400, 2500, 2550, 2800, 3150, 3500, 3800, 4300, 4700, 5200, 5500};
        int[] CenemyYPositions2 = { 832, 780, 832, 770, 790, 680, 832, 790, 790, 610, 700};
        for (int i = 0; i < CenemyXPositions2.Length; i++)
        {
            Enemy.Add(new Cheeps(2, CenemyXPositions2[i], CenemyYPositions2[i], (IPlayer)mario, (IPlayer)luigi));
        }

        int[] CenemyXPositions0 = {3050, 3700, 4800, 5300, 5800, 6000};
        int[] CenemyYPositions0 = {680, 832, 780, 864, 620, 700};
        for (int i = 0; i < CenemyXPositions0.Length; i++)
        {
            Enemy.Add(new Cheeps(0, CenemyXPositions0[i], CenemyYPositions0[i], (IPlayer)mario, (IPlayer)luigi));
        }
    }

    private void AddEntitiesToGame()
    {
        foreach (var e in Enemy)
        {
            entities.Add(e);
        }
    }



    public void UpdateLevel(GameTime gameTime)
    {
        if (swimming)
        {
            if (mario.marioPosition.Y < 538)
            {
                mario.marioPosition.Y = 538;
            }
            if (luigi.luigiPosition.Y < 538)
            {
                luigi.luigiPosition.Y = 538;
            }
        }

        if (!startAnimation)
        {
            {
                mario.Stop();
                mario.Fall();
                mario.Fall();
                mario.Fall();
                mario.Fall();
                mario.Fall();
                mario.Fall();
                mario.Fall();
                luigi.Stop();

                luigi.Fall();
                luigi.Fall();
                luigi.Fall();
                luigi.Fall();
                luigi.Fall();
                luigi.Fall();
                luigi.Fall();

                mario.Update(gameTime);
                luigi.Update(gameTime);

                mario.marioPosition = new Vector2(70, 800);
                luigi.luigiPosition = new Vector2(40, 800);
                followCamera.SetYPosition();
                mario.SetSwimmingLevel(true);
                luigi.SetSwimmingLevel(true);
                swimming = true;
                startAnimation = true;
            }
        }


        if ((mario.marioPosition.X > 6000 && mario.marioPosition.Y > 672 && mario.marioPosition.Y < 736) || (luigi.luigiPosition.X > 6000 && luigi.luigiPosition.Y > 672 && luigi.luigiPosition.Y < 736))
        {
            upPipeAnimation = true;
        }

        if (upPipeAnimation == true)
        {
            PlayerStateMachine marioStateMachine = mario.getStateMachine();
            PlayerStateMachine luigiStaetMachine = luigi.getStateMachine();


            if (!marioStateMachine.IsDead())
            {
                mario.SetSwimmingLevel(false);
                mario.marioPosition = new Vector2(5132, 270);
                mario.Update(gameTime);
            }
            if (!luigiStaetMachine.IsDead())
            {
                luigi.SetSwimmingLevel(false);
                luigi.luigiPosition = new Vector2(5132, 270);
                luigi.Update(gameTime);
            }
            swimming = false;
            upPipeAnimation = false;
            followCamera.setHigherCamera();
        }

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
            mario.Update(gameTime); ;
        }

        for (int i = 0; i < Enemy.Count; i++)
        {
            Enemy[i].Updates();
        }
    }

    public void DrawLevel(SpriteBatch sB)
    {
        lvl2backdrop1.Draw(spriteBatch, overworldTiles, Vector2.Zero);
        lvl2backdrop2.Draw(spriteBatch, underwaterTiles, Vector2.Zero);
        lvl2greenery.Draw(spriteBatch, overworldTiles, Vector2.Zero);
        lvl2foreground1.Draw(spriteBatch, overworldTiles, Vector2.Zero);
        lvl2foreground2.Draw(spriteBatch, underwaterTiles, Vector2.Zero);

        if (gameStateMachine.isMultiplayer())
        {
            mario.Draw(spriteBatch);
            luigi.Draw(spriteBatch);
        }
        else if (gameStateMachine.isSingleplayer())
        {
            mario.Draw(spriteBatch);
        }

        for (int i = 0; i < Enemy.Count; i++)
        {
            Enemy[i].Draw(spriteBatch, EnemyTexture);
        }
    }

    public List<Rectangle> GetLevelFloorRectangles()
    {
        List<Rectangle> combinedFloorRectangles = new List<Rectangle>();
        combinedFloorRectangles.AddRange(lvl2foreground1.GetRedRectangles());
        combinedFloorRectangles.AddRange(lvl2foreground2.GetRedRectangles());
        return combinedFloorRectangles;
    }
}
