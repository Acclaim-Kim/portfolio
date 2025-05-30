public class MarioJumpCommand : IPlayerCommand
{
    private Mario mario;

    public MarioJumpCommand(Mario mario)
    {
        this.mario = mario;
    }

    public void Execute()
    {
        mario.Jump();
    }

    public void Unexecute()
    {
    }
}