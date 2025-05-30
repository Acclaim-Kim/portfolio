using Microsoft.Xna.Framework.Media;
using Pixel_Plumbers_Fall_2024;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Threading.Tasks;
using System.Threading;

public class StandCommand : ICommand
{
    private BlackJackStateMachine blackJackStateMachine;

    public StandCommand(BlackJackStateMachine blackJackStateMachine)
    {
        this.blackJackStateMachine = blackJackStateMachine;
    }

    public void Execute()
    {
        blackJackStateMachine.Stand();
    }
}
