public class LuigiFireballCommand : IPlayerCommand
{
    private Luigi luigi;

    public LuigiFireballCommand(Luigi luigi)
    {
        this.luigi = luigi;
    }

    public void Execute()
    {
        luigi.ShootFireball();
    }

    public void Unexecute()
    {
        luigi.StopRunning();
    }
}
