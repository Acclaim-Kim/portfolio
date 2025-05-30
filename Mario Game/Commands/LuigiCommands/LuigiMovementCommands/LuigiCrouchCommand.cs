public class LuigiCrouchCommand : IPlayerCommand
{
    private Luigi luigi;

    public LuigiCrouchCommand(Luigi luigi)
    {
        this.luigi = luigi;
    }

    public void Execute()
    {
        luigi.Crouch();
    }

    public void Unexecute()
    {
        luigi.Stop();
    }
}
