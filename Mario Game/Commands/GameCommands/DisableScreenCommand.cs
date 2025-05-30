using Pixel_Plumbers_Fall_2024;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class DisableScreenCommand : ICommand
{
    Dictionary<Rectangle, ICommand> list;
    MouseController gameMouseController;
    public DisableScreenCommand(Dictionary<Rectangle, ICommand> list, MouseController gameMouseController)
    {
        this.list = list;
        this.gameMouseController = gameMouseController;
    }
    public void Execute()
    {
        foreach (var item in list)
        {
            gameMouseController.RemoveCommand(item.Key);
        }
    }

    public void Set(BlackJackStateMachine blackJackStateMachine, GameStateMachine gameStateMachine)
    {
        Dictionary<Rectangle, ICommand> BlackJackList = new Dictionary<Rectangle, ICommand>();
        ICommand BlackJackCommand = new BlackJackCommand(blackJackStateMachine, gameStateMachine);
        ICommand CardCommand = new CardCommand(blackJackStateMachine);
        ICommand StandCommand = new StandCommand(blackJackStateMachine);

        BlackJackList.Add(blackJackStateMachine.DestinationRectangle(), BlackJackCommand);
        BlackJackList.Add(new Rectangle(660, 130, 75, 110), CardCommand);
        BlackJackList.Add(new Rectangle(660, 270, 100, 50), StandCommand);
        gameMouseController.SetList(BlackJackList);
    }
}