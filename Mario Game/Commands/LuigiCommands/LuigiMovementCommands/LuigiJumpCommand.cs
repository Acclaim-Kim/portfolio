public class LuigiJumpCommand : IPlayerCommand
{
    private Luigi luigi;

    public LuigiJumpCommand(Luigi luigi)
    {
        this.luigi = luigi;
    }

    public void Execute()
    {
        luigi.Jump();
    }

    public void Unexecute()
    {
    }
}
