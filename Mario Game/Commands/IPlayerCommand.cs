using System.Runtime.CompilerServices;

public interface IPlayerCommand
{
    public void Execute();
    public void Unexecute();
}