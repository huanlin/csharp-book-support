// デモ: Position パターンと Tuple パターン

Console.WriteLine("=== Tuple パターンと Position パターンの例 ===\n");

// --------------------------------------------------------------
// 1. 基本的な Tuple パターン
// --------------------------------------------------------------
Console.WriteLine("1. 基本的な Tuple パターン");
Console.WriteLine(new string('-', 40));

(int, int)[] points = [(0, 0), (0, 5), (5, 0), (3, 3), (2, 7)];

foreach (var (x, y) in points)
{
    string description = DescribePoint(x, y);
    Console.WriteLine($"({x}, {y}): {description}");
}

// --------------------------------------------------------------
// 2. record で Position パターン
// --------------------------------------------------------------
Console.WriteLine("\n2. record で Position パターン");
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
// 3. Position パターンで値をキャプチャ
// --------------------------------------------------------------
Console.WriteLine("\n3. Position パターンで値をキャプチャ");
Console.WriteLine(new string('-', 40));

foreach (Point p in recordPoints)
{
    string description = DescribeWithCapture(p);
    Console.WriteLine(description);
}

// --------------------------------------------------------------
// 4. 実践例: ドア状態遷移
// --------------------------------------------------------------
Console.WriteLine("\n4. 実践例: ドア状態遷移");
Console.WriteLine(new string('-', 40));

DoorState currentState = DoorState.Closed;
Console.WriteLine($"初期状態: {currentState}");

DoorAction[] actions = [
    DoorAction.Open,
    DoorAction.Close,
    DoorAction.Lock,
    DoorAction.Open,
    DoorAction.Unlock,
    DoorAction.Open
];

foreach (DoorAction action in actions)
{
    DoorState previousState = currentState;
    currentState = NextState(currentState, action);

    if (previousState == currentState)
    {
        Console.WriteLine($"  操作 {action}: 無効（{currentState} のまま）");
    }
    else
    {
        Console.WriteLine($"  操作 {action}: {previousState} -> {currentState}");
    }
}

// --------------------------------------------------------------
// 5. 実践例: 象限判定
// --------------------------------------------------------------
Console.WriteLine("\n5. 実践例: 象限判定");
Console.WriteLine(new string('-', 40));

(int, int)[] quadrantPoints = [
    (3, 4), (-2, 5), (-3, -4), (5, -2), (0, 0), (0, 5), (5, 0)
];

foreach (var (x, y) in quadrantPoints)
{
    string quadrant = GetQuadrant(x, y);
    Console.WriteLine($"({x,2}, {y,2}): {quadrant}");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// パターンマッチメソッド
// ============================================================

static string DescribePoint(int x, int y) => (x, y) switch
{
    (0, 0) => "原点",
    (0, _) => "Y 軸上",
    (_, 0) => "X 軸上",
    (var a, var b) when a == b => "対角線上",
    _ => "通常の点"
};

static string DescribeRecordPoint(Point p) => p switch
{
    (0, 0) => "原点",
    (0, _) => "Y 軸上",
    (_, 0) => "X 軸上",
    (var x, var y) when x == y => "主対角線上",
    (var x, var y) when x == -y => "反対角線上",
    _ => "通常の点"
};

static string DescribeWithCapture(Point p) => p switch
{
    (0, 0) => "原点",
    (var x, var y) when x == y => $"主対角線上, 座標 ({x}, {y})",
    (var x, var y) when x == -y => $"反対角線上, 座標 ({x}, {y})",
    (var x, var y) => $"通常の点 ({x}, {y})"
};

static DoorState NextState(DoorState current, DoorAction action) =>
    (current, action) switch
    {
        (DoorState.Closed, DoorAction.Open) => DoorState.Open,
        (DoorState.Open, DoorAction.Close) => DoorState.Closed,
        (DoorState.Closed, DoorAction.Lock) => DoorState.Locked,
        (DoorState.Locked, DoorAction.Unlock) => DoorState.Closed,
        _ => current
    };

static string GetQuadrant(int x, int y) => (x, y) switch
{
    (0, 0) => "原点",
    (0, _) => "Y 軸",
    (_, 0) => "X 軸",
    (> 0, > 0) => "第1象限",
    (< 0, > 0) => "第2象限",
    (< 0, < 0) => "第3象限",
    (> 0, < 0) => "第4象限"
};

// ============================================================
// データクラス
// ============================================================

public record Point(int X, int Y);

public enum DoorState { Open, Closed, Locked }
public enum DoorAction { Open, Close, Lock, Unlock }
