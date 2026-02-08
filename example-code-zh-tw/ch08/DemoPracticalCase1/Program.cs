// 案例 1：可撤銷的操作系統（命令模式）

Console.WriteLine("案例 1：可撤銷的操作系統");
Console.WriteLine(new string('-', 40));

var manager = new CommandManager();
var counter = 0;

// 增加命令
var incrementCommand = new Command(
    execute: () => { counter++; Console.WriteLine($"  執行：counter = {counter}"); },
    undo: () => { counter--; Console.WriteLine($"  撤銷：counter = {counter}"); }
);

manager.ExecuteCommand(incrementCommand);
manager.ExecuteCommand(incrementCommand);
manager.ExecuteCommand(incrementCommand);

Console.WriteLine("\n撤銷兩次：");
manager.Undo();
manager.Undo();

Console.ReadKey();

// ============================================================
// 輔助類別
// ============================================================

// 命令模式
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
