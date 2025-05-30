public class MarioPowerUpCommand : IPlayerCommand
{
    private Mario mario;

    public MarioPowerUpCommand(Mario mario)
    {
        this.mario = mario;
    }
    public void Execute()
    {
        mario.PowerUp();
    }

    public void Unexecute()
    {
    }
}
