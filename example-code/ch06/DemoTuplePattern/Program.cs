// 示範位置模式與 Tuple 模式

Console.WriteLine("=== Tuple 模式與位置模式範例 ===\n");

// --------------------------------------------------------------
// 1. Tuple 模式基本用法
// --------------------------------------------------------------
Console.WriteLine("1. Tuple 模式基本用法");
Console.WriteLine(new string('-', 40));

(int, int)[] points = [(0, 0), (0, 5), (5, 0), (3, 3), (2, 7)];

foreach (var (x, y) in points)
{
    string description = DescribePoint(x, y);
    Console.WriteLine($"({x}, {y}): {description}");
}

// --------------------------------------------------------------
// 2. 位置模式與 record
// --------------------------------------------------------------
Console.WriteLine("\n2. 位置模式與 record");
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
// 3. 在位置模式中擷取值
// --------------------------------------------------------------
Console.WriteLine("\n3. 在位置模式中擷取值");
Console.WriteLine(new string('-', 40));

foreach (Point p in recordPoints)
{
    string description = DescribeWithCapture(p);
    Console.WriteLine(description);
}

// --------------------------------------------------------------
// 4. 實戰範例：狀態機
// --------------------------------------------------------------
Console.WriteLine("\n4. 實戰範例：門的狀態機");
Console.WriteLine(new string('-', 40));

// 初始狀態
DoorState currentState = DoorState.Closed;
Console.WriteLine($"初始狀態：{currentState}");

// 執行一系列動作
DoorAction[] actions = [
    DoorAction.Open,
    DoorAction.Close,
    DoorAction.Lock,
    DoorAction.Open,      // 無效（已鎖定）
    DoorAction.Unlock,
    DoorAction.Open
];

foreach (DoorAction action in actions)
{
    DoorState previousState = currentState;
    currentState = NextState(currentState, action);
    
    if (previousState == currentState)
    {
        Console.WriteLine($"  動作 {action}: 無效（維持 {currentState}）");
    }
    else
    {
        Console.WriteLine($"  動作 {action}: {previousState} -> {currentState}");
    }
}

// --------------------------------------------------------------
// 5. 實戰範例：象限判斷
// --------------------------------------------------------------
Console.WriteLine("\n5. 實戰範例：象限判斷");
Console.WriteLine(new string('-', 40));

(int, int)[] quadrantPoints = [
    (3, 4), (-2, 5), (-3, -4), (5, -2), (0, 0), (0, 5), (5, 0)
];

foreach (var (x, y) in quadrantPoints)
{
    string quadrant = GetQuadrant(x, y);
    Console.WriteLine($"({x,2}, {y,2}): {quadrant}");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模式比對方法
// ============================================================

static string DescribePoint(int x, int y) => (x, y) switch
{
    (0, 0) => "原點",
    (0, _) => "Y 軸上",
    (_, 0) => "X 軸上",
    (var a, var b) when a == b => "在對角線上",
    _ => "一般點"
};

static string DescribeRecordPoint(Point p) => p switch
{
    (0, 0) => "原點",
    (0, _) => "Y 軸上",
    (_, 0) => "X 軸上",
    (var x, var y) when x == y => "在主對角線上",
    (var x, var y) when x == -y => "在反對角線上",
    _ => "一般點"
};

static string DescribeWithCapture(Point p) => p switch
{
    (0, 0) => "原點",
    (var x, var y) when x == y => $"在主對角線上，座標 ({x}, {y})",
    (var x, var y) when x == -y => $"在反對角線上，座標 ({x}, {y})",
    (var x, var y) => $"一般點 ({x}, {y})"
};

static DoorState NextState(DoorState current, DoorAction action) =>
    (current, action) switch
    {
        (DoorState.Closed, DoorAction.Open) => DoorState.Open,
        (DoorState.Open, DoorAction.Close) => DoorState.Closed,
        (DoorState.Closed, DoorAction.Lock) => DoorState.Locked,
        (DoorState.Locked, DoorAction.Unlock) => DoorState.Closed,
        _ => current  // 無效操作，保持原狀態
    };

static string GetQuadrant(int x, int y) => (x, y) switch
{
    (0, 0) => "原點",
    (0, _) => "Y 軸",
    (_, 0) => "X 軸",
    (> 0, > 0) => "第一象限",
    (< 0, > 0) => "第二象限",
    (< 0, < 0) => "第三象限",
    (> 0, < 0) => "第四象限"
};

// ============================================================
// 資料類別
// ============================================================

public record Point(int X, int Y);

public enum DoorState { Open, Closed, Locked }
public enum DoorAction { Open, Close, Lock, Unlock }
