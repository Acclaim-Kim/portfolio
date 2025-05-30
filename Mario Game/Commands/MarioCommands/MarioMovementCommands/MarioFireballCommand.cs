public class MarioFireballCommand : IPlayerCommand
{
    private Mario mario;

    public MarioFireballCommand(Mario mario)
    {
        this.mario = mario;
    }

    public void Execute()
    {
        mario.ShootFireball();
    }

    public void Unexecute()
    {
        mario.StopRunning();
    }
}
