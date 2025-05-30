using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

public class PlayerMovementController : IController
{
    Dictionary<Keys, IPlayerCommand> KeyBinds;
    Dictionary<Buttons, IPlayerCommand> ControllerBinds;
    KeyboardState previousKeyState;
    GamePadState prevGamePadState;

    List<Buttons> contButtonsUsed;

    public PlayerMovementController()
    {
        KeyBinds = new Dictionary<Keys, IPlayerCommand>();
        ControllerBinds = new Dictionary<Buttons, IPlayerCommand>();
        previousKeyState = Keyboard.GetState();

        contButtonsUsed = new List<Buttons>();
        contButtonsUsed.Add(Buttons.A);
        contButtonsUsed.Add(Buttons.B);
        contButtonsUsed.Add(Buttons.DPadLeft);
        contButtonsUsed.Add(Buttons.DPadRight);
        contButtonsUsed.Add(Buttons.DPadDown);
    }

    public void addCommand(Keys key, IPlayerCommand command)
    {
        KeyBinds.Add(key, command);
    }

    public void addControllerCommand(Buttons button, IPlayerCommand command)
    {
        ControllerBinds.Add(button, command);
    }
    public Dictionary<Keys, IPlayerCommand> GetCommands()
    {
        return KeyBinds;
    }

    public void SetCommands(Dictionary<Keys, IPlayerCommand> commands)
    {
        KeyBinds = commands;
    }

    public void Update()
    {
        KeyboardState currentKeyState = Keyboard.GetState();
        GamePadState currentContState = GamePad.GetState(0);
        Keys[] keysPressed = currentKeyState.GetPressedKeys();
        Keys[] previouslyPressedKeys = previousKeyState.GetPressedKeys();

        foreach (var key in keysPressed)
        {
            if (KeyBinds.ContainsKey(key))
            {
                KeyBinds[key].Execute();
            }
        }

        foreach (var key in previouslyPressedKeys)
        {
            if (!keysPressed.Contains(key) && KeyBinds.ContainsKey(key))
            {
                KeyBinds[key].Unexecute();
            }
        }

        if (ControllerBinds.Count > 0)
        {
            checkControllerInput(currentContState);
        }

        previousKeyState = currentKeyState;
        prevGamePadState = currentContState;
    }

    private void checkControllerInput(GamePadState contState)
    {
        foreach (var b in contButtonsUsed)
        {
            if (contState.IsButtonDown(b)){
                ControllerBinds[b].Execute();
            }
        }
        foreach (var b in contButtonsUsed)
        {
            if (!contState.IsButtonDown(b) && prevGamePadState.IsButtonDown(b))
            {
                ControllerBinds[b].Unexecute();
            }
        }

    }
}
