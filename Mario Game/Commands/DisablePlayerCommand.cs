using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
public class DisablePlayerCommand : ICommand
{
    Dictionary<Keys, IPlayerCommand> commandsMario;
    Dictionary<Keys, IPlayerCommand> commandsLuigi;
    PlayerMovementController marioMovementController;
    PlayerMovementController luigiMovementController;
    int count = 0;
    public DisablePlayerCommand(PlayerMovementController marioMovementController, PlayerMovementController luigiMovementController)
    {
        this.marioMovementController = marioMovementController;
        commandsMario = marioMovementController.GetCommands();
        this.luigiMovementController = luigiMovementController;
        commandsLuigi = luigiMovementController.GetCommands();
    }

    public void Execute()
    {
            marioMovementController.SetCommands(new Dictionary<Keys, IPlayerCommand>());
            luigiMovementController.SetCommands(new Dictionary<Keys, IPlayerCommand>());
    }
    public void ReturnCommand()
    {
        marioMovementController.SetCommands(commandsMario);
        luigiMovementController.SetCommands(commandsLuigi);
    }
}
