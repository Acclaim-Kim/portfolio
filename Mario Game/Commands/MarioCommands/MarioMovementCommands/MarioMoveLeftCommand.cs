using System.Diagnostics;

public class MarioMoveLeftCommand : IPlayerCommand
{
    private Mario mario;

    public MarioMoveLeftCommand(Mario mario)
    {
        this.mario = mario;
    }

    public void Execute()
    {
        mario.MoveLeft();
    }

    public void Unexecute()
    {
        mario.Stop();
    }
}
