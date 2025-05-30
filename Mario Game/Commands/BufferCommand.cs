public class PrintMessageCommand : ICommand
{
    private string message;

    public PrintMessageCommand(string message)
    {
        this.message = message;
    }

    public void Execute()
    {
        System.Console.WriteLine("Clicked on this: " + message);
    }
}
