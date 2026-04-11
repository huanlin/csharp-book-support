// Case 1: Undoable System (Command Pattern)

Console.WriteLine("Case 1: Undoable System");
Console.WriteLine(new string('-', 40));

var manager = new CommandManager();
var counter = 0;

// Add command
var incrementCommand = new Command(
    execute: () => { counter++; Console.WriteLine($"  Execute: counter = {counter}"); },
    undo: () => { counter--; Console.WriteLine($"  Undo: counter = {counter}"); }
);

manager.ExecuteCommand(incrementCommand);
manager.ExecuteCommand(incrementCommand);
manager.ExecuteCommand(incrementCommand);

Console.WriteLine("\nUndo twice:");
manager.Undo();
manager.Undo();

Console.ReadKey();

// ============================================================
// Helper Classes
// ============================================================

// Command Pattern
public class Command
{
    private readonly Action _execute;
    private readonly Action _undo;

    public Command(Action execute, Action undo)
    {
        _execute = execute;
        _undo = undo;
    }

    public void Execute() => _execute();
    public void Undo() => _undo();
}

public class CommandManager
{
    private readonly Stack<Command> _history = new();

    public void ExecuteCommand(Command command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void Undo()
    {
        if (_history.Count > 0)
        {
            var command = _history.Pop();
            command.Undo();
        }
    }
}
