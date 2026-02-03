// 示範航空公司票價計算系統（實戰案例）

Console.WriteLine("=== 航空公司票價計算系統 ===\n");

// 建立乘客資料
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
// 1. C# 8 風格：使用 when 子句
// --------------------------------------------------------------
Console.WriteLine("1. C# 8 風格：使用 when 子句");
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

    Console.WriteLine($"票價 {flightCost,10:C}：{passenger}");
}

// --------------------------------------------------------------
// 2. C# 9+ 風格：使用屬性模式結合關係模式
// --------------------------------------------------------------
Console.WriteLine("\n2. C# 9+ 風格：使用屬性模式結合關係模式");
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

    Console.WriteLine($"票價 {flightCost,10:C}：{passenger}");
}

// --------------------------------------------------------------
// 3. 巢狀 Switch 表達式
// --------------------------------------------------------------
Console.WriteLine("\n3. 巢狀 Switch 表達式");
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

    Console.WriteLine($"票價 {flightCost,10:C}：{passenger}");
}

// --------------------------------------------------------------
// 4. 加入更多票價規則
// --------------------------------------------------------------
Console.WriteLine("\n4. 加入更多票價規則（含折扣和附加費）");
Console.WriteLine(new string('-', 50));

foreach (Passenger passenger in passengers)
{
    (decimal baseCost, string note) = CalculateDetailedPrice(passenger);
    Console.WriteLine($"票價 {baseCost,10:C}：{passenger} {note}");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 計算詳細票價
// ============================================================

static (decimal Price, string Note) CalculateDetailedPrice(Passenger passenger) =>
    passenger switch
    {
        // 頭等艙：根據累積哩程給予折扣
        FirstClassPassenger { AirMiles: > 50_000, Name: var name } =>
            (1_200M, $"[{name} 白金會員折扣]"),
        FirstClassPassenger { AirMiles: > 35_000 } =>
            (1_500M, "[金卡會員折扣]"),
        FirstClassPassenger { AirMiles: > 15_000 } =>
            (1_750M, "[銀卡會員折扣]"),
        FirstClassPassenger =>
            (2_000M, "[標準票價]"),

        // 商務艙：固定票價
        BusinessClassPassenger =>
            (1_000M, "[標準票價]"),

        // 經濟艙：根據行李重量調整
        CoachClassPassenger { CarryOnKG: 0 } =>
            (450M, "[無行李優惠]"),
        CoachClassPassenger { CarryOnKG: < 10.0 } =>
            (500M, "[輕便行李]"),
        CoachClassPassenger { CarryOnKG: < 20.0 } =>
            (600M, "[標準行李]"),
        CoachClassPassenger { CarryOnKG: >= 20.0, CarryOnKG: var kg } =>
            (650M + (decimal)(kg - 20) * 10M, $"[超重行李附加費]"),

        // 預設
        _ =>
            (800M, "[未知艙等]")
    };

// ============================================================
// 乘客類別
// ============================================================

public class Passenger
{
    public string? Name { get; set; }
}

public class BusinessClassPassenger : Passenger
{
    public override string ToString() => $"商務艙：{Name}";
}

public class FirstClassPassenger : Passenger
{
    public int AirMiles { get; set; }

    public override string ToString() =>
        $"頭等艙（{AirMiles:N0} 哩）：{Name}";
}

public class CoachClassPassenger : Passenger
{
    public double CarryOnKG { get; set; }

    public override string ToString() =>
        $"經濟艙（{CarryOnKG:N2} KG 行李）：{Name}";
}
