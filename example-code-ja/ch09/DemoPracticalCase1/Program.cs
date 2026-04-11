// ケース 1: Undo 可能システム（Command パターン）

Console.WriteLine("ケース 1: Undo 可能システム");
Console.WriteLine(new string('-', 40));

var manager = new CommandManager();
var counter = 0;

var incrementCommand = new Command(
    execute: () => { counter++; Console.WriteLine($"  実行: counter = {counter}"); },
    undo: () => { counter--; Console.WriteLine($"  Undo: counter = {counter}"); }
);

manager.ExecuteCommand(incrementCommand);
manager.ExecuteCommand(incrementCommand);
manager.ExecuteCommand(incrementCommand);

Console.WriteLine("\n2 回 Undo:");
manager.Undo();
manager.Undo();

Console.ReadKey();

// ============================================================
// ヘルパークラス
// ============================================================

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
