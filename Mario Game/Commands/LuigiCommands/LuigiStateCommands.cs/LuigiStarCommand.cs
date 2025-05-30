public class LuigiStarCommand : IPlayerCommand
{
    private Luigi luigi;

    public LuigiStarCommand(Luigi luigi)
    {
        this.luigi = luigi;
    }

    public void Execute()
    {
        luigi.CollectStar();
    }

    public void Unexecute()
    {
    }
}
