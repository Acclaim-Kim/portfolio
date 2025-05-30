public class MarioMoveRightCommand : IPlayerCommand
{
    private Mario mario;

    public MarioMoveRightCommand(Mario mario)
    {
        this.mario = mario;
    }
    public void Execute()
    {
        mario.MoveRight();
    }

    public void Unexecute()
    {
        mario.Stop();
    }
}
