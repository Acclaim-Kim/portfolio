public class MarioStarCommand : IPlayerCommand
{
    private Mario mario;

    public MarioStarCommand(Mario mario)
    {
        this.mario = mario;
    }
    public void Execute()
    {
        mario.CollectStar();
    }

    public void Unexecute()
    {
    }
}
