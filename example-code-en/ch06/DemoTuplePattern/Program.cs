// Demo: Positional Pattern and Tuple Pattern

Console.WriteLine("=== Tuple Pattern and Positional Pattern Example ===\n");

// --------------------------------------------------------------
// 1. Basic Tuple Pattern Usage
// --------------------------------------------------------------
Console.WriteLine("1. Basic Tuple Pattern Usage");
Console.WriteLine(new string('-', 40));

(int, int)[] points = [(0, 0), (0, 5), (5, 0), (3, 3), (2, 7)];

foreach (var (x, y) in points)
{
    string description = DescribePoint(x, y);
    Console.WriteLine($"({x}, {y}): {description}");
}

// --------------------------------------------------------------
// 2. Positional Pattern with record
// --------------------------------------------------------------
Console.WriteLine("\n2. Positional Pattern with record");
Console.WriteLine(new string('-', 40));

Point[] recordPoints =
[
    new Point(0, 0),
    new Point(0, 5),
    new Point(5, 0),
    new Point(4, 4),
    new Point(3, -3)
];

foreach (Point p in recordPoints)
{
    string description = DescribeRecordPoint(p);
    Console.WriteLine($"{p}: {description}");
}

// --------------------------------------------------------------
// 3. Capturing Values in Positional Pattern
// --------------------------------------------------------------
Console.WriteLine("\n3. Capturing Values in Positional Pattern");
Console.WriteLine(new string('-', 40));

foreach (Point p in recordPoints)
{
    string description = DescribeWithCapture(p);
    Console.WriteLine(description);
}

// --------------------------------------------------------------
// 4. Practical Example: State Machine
// --------------------------------------------------------------
Console.WriteLine("\n4. Practical Example: Door State Machine");
Console.WriteLine(new string('-', 40));

// Initial state
DoorState currentState = DoorState.Closed;
Console.WriteLine($"Initial state: {currentState}");

// Execute a series of actions
DoorAction[] actions = [
    DoorAction.Open,
    DoorAction.Close,
    DoorAction.Lock,
    DoorAction.Open,      // Invalid (Locked)
    DoorAction.Unlock,
    DoorAction.Open
];

foreach (DoorAction action in actions)
{
    DoorState previousState = currentState;
    currentState = NextState(currentState, action);
    
    if (previousState == currentState)
    {
        Console.WriteLine($"  Action {action}: Invalid (Remaining {currentState})");
    }
    else
    {
        Console.WriteLine($"  Action {action}: {previousState} -> {currentState}");
    }
}

// --------------------------------------------------------------
// 5. Practical Example: Quadrant Judgment
// --------------------------------------------------------------
Console.WriteLine("\n5. Practical Example: Quadrant Judgment");
Console.WriteLine(new string('-', 40));

(int, int)[] quadrantPoints = [
    (3, 4), (-2, 5), (-3, -4), (5, -2), (0, 0), (0, 5), (5, 0)
];

foreach (var (x, y) in quadrantPoints)
{
    string quadrant = GetQuadrant(x, y);
    Console.WriteLine($"({x,2}, {y,2}): {quadrant}");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Pattern Matching Methods
// ============================================================

static string DescribePoint(int x, int y) => (x, y) switch
{
    (0, 0) => "Origin",
    (0, _) => "On Y-axis",
    (_, 0) => "On X-axis",
    (var a, var b) when a == b => "On diagonal line",
    _ => "Regular point"
};

static string DescribeRecordPoint(Point p) => p switch
{
    (0, 0) => "Origin",
    (0, _) => "On Y-axis",
    (_, 0) => "On X-axis",
    (var x, var y) when x == y => "On main diagonal",
    (var x, var y) when x == -y => "On anti-diagonal",
    _ => "Regular point"
};

static string DescribeWithCapture(Point p) => p switch
{
    (0, 0) => "Origin",
    (var x, var y) when x == y => $"On main diagonal, coordinates ({x}, {y})",
    (var x, var y) when x == -y => $"On anti-diagonal, coordinates ({x}, {y})",
    (var x, var y) => $"Regular point ({x}, {y})"
};

static DoorState NextState(DoorState current, DoorAction action) =>
    (current, action) switch
    {
        (DoorState.Closed, DoorAction.Open) => DoorState.Open,
        (DoorState.Open, DoorAction.Close) => DoorState.Closed,
        (DoorState.Closed, DoorAction.Lock) => DoorState.Locked,
        (DoorState.Locked, DoorAction.Unlock) => DoorState.Closed,
        _ => current  // Invalid operation, remain in current state
    };

static string GetQuadrant(int x, int y) => (x, y) switch
{
    (0, 0) => "Origin",
    (0, _) => "Y-axis",
    (_, 0) => "X-axis",
    (> 0, > 0) => "Quadrant I",
    (< 0, > 0) => "Quadrant II",
    (< 0, < 0) => "Quadrant III",
    (> 0, < 0) => "Quadrant IV"
};

// ============================================================
// Data Classes
// ============================================================

public record Point(int X, int Y);

public enum DoorState { Open, Closed, Locked }
public enum DoorAction { Open, Close, Lock, Unlock }
