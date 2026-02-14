// Demo: Airline Ticket Pricing System (Practical Case Study)

Console.WriteLine("=== Airline Ticket Pricing System ===\n");

// Create passenger data
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
// 1. C# 8 style: Using when clause
// --------------------------------------------------------------
Console.WriteLine("1. C# 8 style: Using when clause");
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

    Console.WriteLine($"Price {flightCost,10:C}: {passenger}");
}

// --------------------------------------------------------------
// 2. C# 9+ style: Property pattern with relational pattern
// --------------------------------------------------------------
Console.WriteLine("\n2. C# 9+ style: Property pattern with relational pattern");
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

    Console.WriteLine($"Price {flightCost,10:C}: {passenger}");
}

// --------------------------------------------------------------
// 3. Nested Switch Expression
// --------------------------------------------------------------
Console.WriteLine("\n3. Nested Switch Expression");
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

    Console.WriteLine($"Price {flightCost,10:C}: {passenger}");
}

// --------------------------------------------------------------
// 4. Adding More Price Rules
// --------------------------------------------------------------
Console.WriteLine("\n4. Adding More Price Rules (including discounts and surcharges)");
Console.WriteLine(new string('-', 50));

foreach (Passenger passenger in passengers)
{
    (decimal baseCost, string note) = CalculateDetailedPrice(passenger);
    Console.WriteLine($"Price {baseCost,10:C}: {passenger} {note}");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Detailed Price Calculation
// ============================================================

static (decimal Price, string Note) CalculateDetailedPrice(Passenger passenger) =>
    passenger switch
    {
        // First Class: Discount based on accumulated miles
        FirstClassPassenger { AirMiles: > 50_000, Name: var name } =>
            (1_200M, $"[{name} Platinum Member Discount]"),
        FirstClassPassenger { AirMiles: > 35_000 } =>
            (1_500M, "[Gold Card Member Discount]"),
        FirstClassPassenger { AirMiles: > 15_000 } =>
            (1_750M, "[Silver Card Member Discount]"),
        FirstClassPassenger =>
            (2_000M, "[Standard Fare]"),

        // Business Class: Fixed price
        BusinessClassPassenger =>
            (1_000M, "[Standard Fare]"),

        // Coach Class: Adjusted based on luggage weight
        CoachClassPassenger { CarryOnKG: 0 } =>
            (450M, "[No Luggage Offer]"),
        CoachClassPassenger { CarryOnKG: < 10.0 } =>
            (500M, "[Light Luggage]"),
        CoachClassPassenger { CarryOnKG: < 20.0 } =>
            (600M, "[Standard Luggage]"),
        CoachClassPassenger { CarryOnKG: >= 20.0, CarryOnKG: var kg } =>
            (650M + (decimal)(kg - 20) * 10M, $"[Overweight Luggage Surcharge]"),

        // Default
        _ =>
            (800M, "[Unknown Class]")
    };

// ============================================================
// Passenger Classes
// ============================================================

public class Passenger
{
    public string? Name { get; set; }
}

public class BusinessClassPassenger : Passenger
{
    public override string ToString() => $"Business Class: {Name}";
}

public class FirstClassPassenger : Passenger
{
    public int AirMiles { get; set; }

    public override string ToString() =>
        $"First Class ({AirMiles:N0} Miles): {Name}";
}

public class CoachClassPassenger : Passenger
{
    public double CarryOnKG { get; set; }

    public override string ToString() =>
        $"Coach Class ({CarryOnKG:N2} KG Luggage): {Name}";
}
