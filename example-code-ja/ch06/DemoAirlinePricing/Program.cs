// デモ: 航空券料金システム（実践ケーススタディ）

Console.WriteLine("=== 航空券料金システム ===\n");

// 乗客データ作成
Passenger[] passengers =
[
    new FirstClassPassenger { AirMiles = 1_419, Name = "Suman" },
    new FirstClassPassenger { AirMiles = 16_562, Name = "Lucy" },
    new FirstClassPassenger { AirMiles = 38_000, Name = "Chen" },
    new BusinessClassPassenger { Name = "Janice" },
    new CoachClassPassenger { CarryOnKG = 25.7, Name = "Dave" },
    new CoachClassPassenger { CarryOnKG = 0, Name = "Amit" },
];

// --------------------------------------------------------------
// 1. C# 8 スタイル: when 句を使用
// --------------------------------------------------------------
Console.WriteLine("1. C# 8 スタイル: when 句を使用");
Console.WriteLine(new string('-', 50));

foreach (Passenger passenger in passengers)
{
    decimal flightCost = passenger switch
    {
        FirstClassPassenger p when p.AirMiles > 35_000 => 1_500M,
        FirstClassPassenger p when p.AirMiles > 15_000 => 1_750M,
        FirstClassPassenger => 2_000M,
        BusinessClassPassenger => 1_000M,
        CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
        CoachClassPassenger => 650M,
        _ => 800M
    };

    Console.WriteLine($"料金 {flightCost,10:C}: {passenger}");
}

// --------------------------------------------------------------
// 2. C# 9+ スタイル: プロパティ + 関係パターン
// --------------------------------------------------------------
Console.WriteLine("\n2. C# 9+ スタイル: プロパティ + 関係パターン");
Console.WriteLine(new string('-', 50));

foreach (Passenger passenger in passengers)
{
    decimal flightCost = passenger switch
    {
        FirstClassPassenger { AirMiles: > 35_000 } => 1_500M,
        FirstClassPassenger { AirMiles: > 15_000 } => 1_750M,
        FirstClassPassenger => 2_000M,
        BusinessClassPassenger => 1_000M,
        CoachClassPassenger { CarryOnKG: < 10.0 } => 500M,
        CoachClassPassenger => 650M,
        _ => 800M
    };

    Console.WriteLine($"料金 {flightCost,10:C}: {passenger}");
}

// --------------------------------------------------------------
// 3. 入れ子の switch 式
// --------------------------------------------------------------
Console.WriteLine("\n3. 入れ子の switch 式");
Console.WriteLine(new string('-', 50));

foreach (Passenger passenger in passengers)
{
    decimal flightCost = passenger switch
    {
        FirstClassPassenger p => p.AirMiles switch
        {
            > 35_000 => 1_500M,
            > 15_000 => 1_750M,
            _ => 2_000M
        },
        BusinessClassPassenger => 1_000M,
        CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
        CoachClassPassenger => 650M,
        _ => 800M
    };

    Console.WriteLine($"料金 {flightCost,10:C}: {passenger}");
}

// --------------------------------------------------------------
// 4. 料金ルール拡張
// --------------------------------------------------------------
Console.WriteLine("\n4. 料金ルール拡張（割引・加算込み）");
Console.WriteLine(new string('-', 50));

foreach (Passenger passenger in passengers)
{
    (decimal baseCost, string note) = CalculateDetailedPrice(passenger);
    Console.WriteLine($"料金 {baseCost,10:C}: {passenger} {note}");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// 詳細料金計算
// ============================================================

static (decimal Price, string Note) CalculateDetailedPrice(Passenger passenger) =>
    passenger switch
    {
        // ファーストクラス: マイルに応じて割引
        FirstClassPassenger { AirMiles: > 50_000, Name: var name } =>
            (1_200M, $"[{name} プラチナ会員割引]"),
        FirstClassPassenger { AirMiles: > 35_000 } =>
            (1_500M, "[ゴールド会員割引]"),
        FirstClassPassenger { AirMiles: > 15_000 } =>
            (1_750M, "[シルバー会員割引]"),
        FirstClassPassenger =>
            (2_000M, "[通常運賃]"),

        // ビジネスクラス: 固定料金
        BusinessClassPassenger =>
            (1_000M, "[通常運賃]"),

        // エコノミー: 手荷物重量で調整
        CoachClassPassenger { CarryOnKG: 0 } =>
            (450M, "[手荷物なし割引]"),
        CoachClassPassenger { CarryOnKG: < 10.0 } =>
            (500M, "[軽量手荷物]"),
        CoachClassPassenger { CarryOnKG: < 20.0 } =>
            (600M, "[標準手荷物]"),
        CoachClassPassenger { CarryOnKG: >= 20.0 and var kg } =>
            (650M + (decimal)(kg - 20) * 10M, "[超過手荷物加算]"),

        // デフォルト
        _ =>
            (800M, "[不明クラス]")
    };

// ============================================================
// 乗客クラス
// ============================================================

public class Passenger
{
    public string? Name { get; set; }
}

public class BusinessClassPassenger : Passenger
{
    public override string ToString() => $"ビジネスクラス: {Name}";
}

public class FirstClassPassenger : Passenger
{
    public int AirMiles { get; set; }

    public override string ToString() =>
        $"ファーストクラス（{AirMiles:N0} マイル）: {Name}";
}

public class CoachClassPassenger : Passenger
{
    public double CarryOnKG { get; set; }

    public override string ToString() =>
        $"エコノミークラス（手荷物 {CarryOnKG:N2} KG）: {Name}";
}
