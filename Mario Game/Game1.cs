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
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace Pixel_Plumbers_Fall_2024
{
    public class Game1 : Game
    {
        // core game components
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private FollowCamera camera;

        // managers
        private TextureManager textureManager;
        private SoundManager soundManager;
        public HudManager hudManager;

        // game state management
        private GameStateMachine gameStateMachine;
        private GameStateControlCenter gameStateControlCenter;
        private KeyboardController gameStateKeyboardController;
        private MouseController gameStateMouseController;
        private DisablePlayerCommand disablePlayerCommand;

        // players and controllers
        private Mario mario;
        private Luigi luigi;
        private MarioControlCenter marioControlCenter;
        private LuigiControlCenter luigiControlCenter;
        private PlayerMovementController marioMovementController;
        private PlayerMovementController luigiMovementController;
        private KeyboardControllerMovement keyboardControllerMovement;
        private KeyboardController keyboardController;

        private MouseController startMenuController;
        private MouseController levelScreenController;
        private MouseController gameOverScrenController;

        // levels
        private LevelOne levelOne;
        private Point lvl1mapSize;
        private LevelTwo levelTwo;
        private Point lvl2mapSize;

        // game entities and collections
        public List<Fireball> fireballs = new List<Fireball>();
        public List<ScorePopup> scorePopups = new List<ScorePopup>();
        public List<ScorePopup> removedSP = new List<ScorePopup>();
        public List<IEntity> entities = new List<IEntity>();
        private List<IEntity> entitiesRemoved = new List<IEntity>();
        private List<Rectangle> lvl1CollidableRectangles;
        private List<Rectangle> lvl2CollidableRectangles;

        // additional game components
        private BlackJackStateMachine blackJackStateMachine;
        private StartScreenSprite startScreenSprite;
        private LevelScreenSprite levelScreenSprite;
        private GameOverScreen gameOverScreenSprite;
        private SpriteFont startScreenFonts;
        private SpriteFont levelScreenFonts;

        // game mechanics
        private Sort sort = new Sort();
        private Sweep sweep;
        private Ground ground;
        private ToggleFalling toggleFalling;

        // timing
        private float gameOverTickTimer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            camera = new(Vector2.Zero);
        }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeGameComponents();
            InitializeLevels();
        }

        private void InitializeGameComponents()
        {
            sweep = new Sweep(new GameTime(), disablePlayerCommand);
            keyboardController = new KeyboardController();
            keyboardControllerMovement = new KeyboardControllerMovement();

            lvl1CollidableRectangles = new List<Rectangle>();
            lvl2CollidableRectangles = new List<Rectangle>();
        }

        private void InitializeLevels()
        {
            levelOne = new LevelOne(this, mario, luigi, entities, entitiesRemoved, spriteBatch, new GameTime(), Content, textureManager, gameStateMachine);
            levelOne.InitializeLevel();
            lvl1CollidableRectangles = levelOne.GetLevelFloorRectangles();

            levelTwo = new LevelTwo(this, mario, luigi, entities, entitiesRemoved, spriteBatch, new GameTime(), Content, textureManager, gameStateMachine, camera);
            levelTwo.InitializeLevel();
            lvl2CollidableRectangles = levelTwo.GetLevelFloorRectangles();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadManagers();
            LoadPlayers();
            LoadUserInterface();
            LoadControllers();
        }

        private void LoadManagers()
        {
            var squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            squareTexture.SetData(new Color[] { Color.Red });

            textureManager = new TextureManager(Content, squareTexture);
            soundManager = new SoundManager(Content);
        }

        private void LoadPlayers()
        {
            var marioSounds = new List<SoundEffect> {
                soundManager.GetSound("powerup"),
                soundManager.GetSound("pipe"),
                soundManager.GetSound("fireball"),
                soundManager.GetSound("jump"),
                soundManager.GetSound("death"),
                soundManager.GetSound("flagpole"),
                soundManager.GetSound("coin"),
                soundManager.GetSound("powerup"),
                soundManager.GetSound("1-up")
            };

            gameStateMachine = new GameStateMachine();

            mario = new Mario(this, entities, marioSounds, textureManager, new GameTime(), ref this.gameStateMachine, ref levelScreenFonts);
            luigi = new Luigi(this, entities, marioSounds, textureManager, new GameTime(), ref this.gameStateMachine, ref levelScreenFonts);
            mario.SetLuigiRef(luigi);
            luigi.SetMarioRef(mario);

        }

        private void LoadUserInterface()
        {
            startScreenFonts = Content.Load<SpriteFont>("StartScreenFonts");
            levelScreenFonts = Content.Load<SpriteFont>("LevelScreenFonts");
            startScreenSprite = new StartScreenSprite(textureManager, startScreenFonts);
            levelScreenSprite = new LevelScreenSprite(textureManager, levelScreenFonts);
            gameOverScreenSprite = new GameOverScreen(textureManager, levelScreenFonts);

            blackJackStateMachine = new BlackJackStateMachine(textureManager, soundManager.GetSound("fwip"), startScreenFonts);
            hudManager = new HudManager(startScreenFonts, this, mario);
        }

        private void LoadControllers()
        {
            marioMovementController = new PlayerMovementController();
            luigiMovementController = new PlayerMovementController();
            disablePlayerCommand = new DisablePlayerCommand(marioMovementController, luigiMovementController);

            marioControlCenter = new MarioControlCenter(mario, marioMovementController);
            luigiControlCenter = new LuigiControlCenter(luigi, luigiMovementController);

            gameStateKeyboardController = new KeyboardController();
            gameStateMouseController = new MouseController();
            startMenuController = new MouseController();
            levelScreenController = new MouseController();
            gameOverScrenController = new MouseController();

            gameStateControlCenter = new GameStateControlCenter(
                gameStateMachine,
                gameStateKeyboardController,
                gameStateMouseController,
                this,
                startScreenSprite,
                levelScreenSprite,
                gameOverScreenSprite,
                soundManager,
                blackJackStateMachine,
                startMenuController,
                levelScreenController,
                gameOverScrenController
            );
        }

        protected override void Update(GameTime gameTime)
        {

            if (gameStateMachine.isStartScreen())
            {
                startMenuController.Update();
            }
            else if (gameStateMachine.isLevelScreen())
            {
                levelScreenController.Update();
            }
            else if (gameStateMachine.isCurrentStateOver())
            {
                gameOverScrenController.Update();

            }

            UpdateGameState(gameTime);
            UpdateGameplay(gameTime);
            UpdateGameOver(gameTime);
            base.Update(gameTime);
        }

        private void UpdateGameState(GameTime gameTime)
        {
            gameStateKeyboardController.Update();
            gameStateMouseController.Update();

            entities = sort.SortList(entities, entities.Count, entities);
            sweep.Compare(entities, entitiesRemoved, camera.position);
            blackJackStateMachine.Update();
        }

        private void UpdateGameplay(GameTime gameTime)
        {
            if (!gameStateMachine.isCurrentStateRunning()) return;

            UpdateInputControllers();
            UpdateCurrentLevel(gameTime);
            UpdateCamera();
            UpdateFireballs(gameTime);
            UpdateScorePopups(gameTime);
            UpdateRemovedEntities();
            hudManager.Update(gameTime, camera);
        }

        private void UpdateInputControllers()
        {
            startMenuController.Update();
            levelScreenController.Update();
            gameOverScrenController.Update();
            keyboardController.Update();
            keyboardControllerMovement.Update();
            marioMovementController.Update();
            luigiMovementController.Update();
        }

        private void UpdateCurrentLevel(GameTime gameTime)
        {
            ground = DetermineCurrentGround();
            toggleFalling = new ToggleFalling(ground, entities, mario, luigi);

            if (gameStateMachine.isLevelOne())
                levelOne.UpdateLevel(gameTime);
            else if (gameStateMachine.isLevelTwo())
                levelTwo.UpdateLevel(gameTime);

            toggleFalling.updates();
        }

        private Ground DetermineCurrentGround()
        {
            return gameStateMachine.isLevelOne()
                ? new Ground(lvl1CollidableRectangles)
                : new Ground(lvl2CollidableRectangles);
        }

        private void UpdateCamera()
        {
            if (gameStateMachine.isLevelOne())
            {
                camera.Follow(mario, luigi, new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), levelOne.mapSize.X);
            }
            else if (gameStateMachine.isLevelTwo())
            {
                camera.Follow(mario, luigi, new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), levelTwo.mapSize.X);
            }

        }

        private void UpdateFireballs(GameTime gameTime)
        {
            foreach (var fireball in fireballs)
            {
                fireball.Update(gameTime);
            }
        }

        private void UpdateScorePopups(GameTime gameTime)
        {
            foreach (var sp in scorePopups)
            {
                sp.Update(gameTime);
            }
            foreach (var sp in removedSP)
            {
                scorePopups.Remove(sp);
            }
        }
        private void UpdateRemovedEntities()
        {
            foreach (var consumedEntity in entitiesRemoved)
            {
                if (entities.Contains(consumedEntity))
                    entities.Remove(consumedEntity);
            }
        }

        private void UpdateGameOver(GameTime gameTime)
        {
            if (!gameStateMachine.isCurrentStateOver()) return;

            gameOverTickTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (gameOverTickTimer > 5000)
            {
                gameStateMachine.setGameStateOver();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointWrap, null, null, null, transformMatrix: camera.GetViewMatrix());
            DrawGameScreen();

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawGameScreen()
        {
            if (gameStateMachine.isCurrentStateOver())
            {
                gameOverScreenSprite.Draw(spriteBatch, new Vector2(200, 200));
                camera.Reset();
            }

            if (gameStateMachine.isCurrentStateStart())
            {
                startScreenSprite.Draw(spriteBatch, new Vector2(200, 200));
                camera.Reset();
            }

            if (gameStateMachine.isLevelScreen())
            {
                levelScreenSprite.Draw(spriteBatch, new Vector2(200, 200));
                camera.Reset();
            }

            if (gameStateMachine.isCurrentStateRunning() || gameStateMachine.isCurrentStatePaused())
            {
                DrawCurrentLevel();
                DrawFireballs();
                DrawScorePopups();
                hudManager.Draw(spriteBatch);
                blackJackStateMachine.Draw(spriteBatch);
            }
        }

        private void DrawCurrentLevel()
        {
            if (gameStateMachine.isLevelOne())
                levelOne.DrawLevel(spriteBatch);
            else if (gameStateMachine.isLevelTwo())
                levelTwo.DrawLevel(spriteBatch);
        }

        private void DrawFireballs()
        {
            foreach (var fireball in fireballs)
            {
                fireball.Draw(spriteBatch);
            }
        }
        private void DrawScorePopups()
        {
            foreach (var sp in scorePopups)
            {
                sp.Draw(spriteBatch, startScreenFonts);
            }
        }

        public void ResetGame()
        {
            entities.Clear();
            fireballs.Clear();
            luigi.Reset();
            mario.Reset();
            hudManager.SetTime(400);
            camera.Reset();
            levelOne.InitializeLevel();
            levelTwo.InitializeLevel();
        }

        public void GameOver()
        {
            gameStateMachine.setGameStateOver();
            soundManager.GetSound("over").Play();
        }

        public void SetKey(KeyboardController keys)
        {
            keyboardController = keys;
        }
    }
}