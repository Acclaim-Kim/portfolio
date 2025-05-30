public class LuigiMoveRightCommand : IPlayerCommand
{
    private Luigi luigi;

    public LuigiMoveRightCommand(Luigi luigi)
    {
        this.luigi = luigi;
    }

    public void Execute()
    {
        luigi.MoveRight();
    }

    public void Unexecute()
    {
        luigi.Stop();
    }
}
