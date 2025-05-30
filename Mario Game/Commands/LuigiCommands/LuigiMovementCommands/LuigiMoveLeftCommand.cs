public class LuigiMoveLeftCommand : IPlayerCommand
{
    private Luigi luigi;

    public LuigiMoveLeftCommand(Luigi luigi)
    {
        this.luigi = luigi;
    }

    public void Execute()
    {
        luigi.MoveLeft();
    }

    public void Unexecute()
    {
        luigi.Stop();
    }
}
