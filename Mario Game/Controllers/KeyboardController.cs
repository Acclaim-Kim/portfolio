using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using Microsoft.VisualBasic;

public class KeyboardController : IController
{
    Dictionary<Keys, ICommand> KeyBinds;
    KeyboardState previousKeyState;
    public KeyboardController()
    {
        KeyBinds = new Dictionary<Keys, ICommand>();
    }

    public void addCommand(Keys key, ICommand command)
    {
        KeyBinds.Add(key, command);
    }

    public void Update()
    {
        var keysPressed = Keyboard.GetState().GetPressedKeys();
        KeyboardState state = Keyboard.GetState();
        foreach (var key in keysPressed)
        {
            if (KeyBinds.ContainsKey(key) & previousKeyState.IsKeyUp(key))
            {
                KeyBinds[key].Execute();
            }
        }

        previousKeyState = state;
    }
}
