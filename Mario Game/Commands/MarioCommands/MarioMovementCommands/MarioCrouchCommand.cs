public class MarioCrouchCommand : IPlayerCommand
{
    private Mario mario;

    public MarioCrouchCommand(Mario mario)
    {
        this.mario = mario;
    }

    public void Execute()
    {
        mario.Crouch();
    }

    public void Unexecute()
    {
        mario.Stop();
    }
}
