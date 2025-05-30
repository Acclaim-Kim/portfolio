using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

public class GameStateControlCenter
{
    private GameStateMachine gameStateMachine;
    private KeyboardController gameKeyboardController;
    private MouseController gameMouseController;
    private Game1 game;
    private StartScreenSprite startScreenSprite;
    private LevelScreenSprite levelScreenSprite;
    private GameOverScreen gameOverScreen;
    private MusicMachine MusicMachine;
    private BlackJackStateMachine blackJackStateMachine;
    private Dictionary<Rectangle, ICommand> list;


    private MouseController startMenuController;
    private MouseController levelScreenController;
    private MouseController gameOverScrenController;

    public GameStateControlCenter(GameStateMachine gameStateMachine, KeyboardController gameKeyboardController, MouseController gameMouseController, Game1 game, StartScreenSprite startScreenSprite, LevelScreenSprite levelScreenSprite, GameOverScreen gameOverScreen, SoundManager musics, BlackJackStateMachine blackJackStateMachine, MouseController startMenuController, MouseController levelScreenController, MouseController gameOverScrenController)
    {
        this.gameKeyboardController = gameKeyboardController;
        this.gameMouseController = gameMouseController;
        this.gameStateMachine = gameStateMachine;
        this.game = game;
        this.startScreenSprite = startScreenSprite;
        this.levelScreenSprite = levelScreenSprite;
        this.blackJackStateMachine = blackJackStateMachine;
        this.gameOverScreen = gameOverScreen;
        this.startMenuController = startMenuController;
        this.levelScreenController = levelScreenController;
        this.gameOverScrenController = gameOverScrenController;
        MusicMachine = new MusicMachine(musics);

        InitializeCommands();
    }

    private void InitializeCommands()
    {
        // Keyboard commands
        ICommand startGameCommand = new RunGameCommand(gameStateMachine);
        gameKeyboardController.addCommand(Keys.D9, startGameCommand);
        ICommand musicCommand = new MusicCommand(MusicMachine);
        gameKeyboardController.addCommand(Keys.M, musicCommand);
        ICommand muteCommand = new MuteCommand();
        gameKeyboardController.addCommand(Keys.Z, muteCommand);
        ICommand quitGameCommand = new QuitGameCommand(game);
        gameKeyboardController.addCommand(Keys.Q, quitGameCommand);
        ICommand resetGameCommand = new ResetGameCommand(game);
        gameKeyboardController.addCommand(Keys.R, resetGameCommand);
        ICommand pauseGameCommand = new PauseGameCommand(gameStateMachine, MusicMachine);
        gameKeyboardController.addCommand(Keys.D3, pauseGameCommand);
        ICommand startScreenCommand = new StartScreeGameCommand(gameStateMachine);
        gameKeyboardController.addCommand(Keys.D0, startScreenCommand);

        // Mouse commands
        ICommand helpClickCommand = new PrintMessageCommand("HELP");
        startMenuController.AddCommand(startScreenSprite.GetHelpRectangle(), helpClickCommand);
        ICommand singlePlayerCommand = new SingleplayerCommand(gameStateMachine);
        startMenuController.AddCommand(startScreenSprite.GetOnePlayerRectangle(), singlePlayerCommand); // Single Player
        ICommand multiplayerCommand = new MultiplayerCommand(gameStateMachine);
        startMenuController.AddCommand(startScreenSprite.GetTwoPlayerRectangle(), multiplayerCommand); // Multiplayer

        list = gameMouseController.SendList();
        ICommand levelOneCommand = new LevelOneCommand(gameStateMachine, list, gameMouseController, blackJackStateMachine, game);
        gameMouseController.AddCommand(levelScreenSprite.GetLevelOneRectangle(), levelOneCommand);

        ICommand levelTwoCommand = new LevelTwoCommand(gameStateMachine, list, gameMouseController, blackJackStateMachine, game);
        gameMouseController.AddCommand(levelScreenSprite.GetLevelTwoRectangle(), levelTwoCommand);

        levelScreenController.AddCommand(levelScreenSprite.GetLevelOneRectangle(), levelOneCommand);
        levelScreenController.AddCommand(levelScreenSprite.GetLevelTwoRectangle(), levelTwoCommand);

        ICommand startScreenCommand2 = new StartScreeGameCommand2(gameStateMachine);
        gameOverScrenController.AddCommand(gameOverScreen.GetMainMenuRectangle(), startScreenCommand2);
        ICommand resetGameCommand2 = new ResetGameCommand2(game, gameStateMachine);
        gameOverScrenController.AddCommand(gameOverScreen.GetRestartRectangle(), resetGameCommand2);

        //ICommand BlackJackCommand = new BlackJackCommand(blackJackStateMachine, gameStateMachine);
        //gameMouseController.AddCommand(blackJackStateMachine.DestinationRectangle(), BlackJackCommand);

        //ICommand CardCommand = new CardCommand(blackJackStateMachine);
        //gameMouseController.AddCommand(new Rectangle(660, 130, 75, 110), CardCommand);

        //ICommand StandCommand = new StandCommand(blackJackStateMachine);
        //gameMouseController.AddCommand(new Rectangle(660, 270, 100, 50), StandCommand);
    }
}
