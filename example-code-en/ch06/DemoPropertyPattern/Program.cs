// Demo: Property Pattern

Console.WriteLine("=== Property Pattern Example ===\n");

// --------------------------------------------------------------
// 1. Basic Property Pattern
// --------------------------------------------------------------
Console.WriteLine("1. Basic Property Pattern");
Console.WriteLine(new string('-', 40));

Person[] people =
[
    new Person("Alice", 30, new Address("Taipei", "Xinyi")),
    new Person("Bob", 25, new Address("Taipei", "Daan")),
    new Person("Charlie", 35, new Address("Kaohsiung", "Zuoying")),
    new Person("Diana", 28, new Address("Taipei", "Zhongshan"))
];

foreach (Person person in people)
{
    if (person is { Name: "Alice", Age: 30 })
    {
        Console.WriteLine($"Found Alice, 30 years old!");
    }
    else if (person is { Address.City: "Taipei" })
    {
        Console.WriteLine($"{person.Name} lives in Taipei");
    }
    else
    {
        Console.WriteLine($"{person.Name} lives in other areas");
    }
}

// --------------------------------------------------------------
// 2. Using in Switch Expression
// --------------------------------------------------------------
Console.WriteLine("\n2. Using in Switch Expression");
Console.WriteLine(new string('-', 40));

Uri[] uris =
[
    new Uri("http://example.com:80"),
    new Uri("https://example.com:443"),
    new Uri("ftp://files.example.com:21"),
    new Uri("http://localhost:8080"),
    new Uri("https://secure.example.com:8443")
];

foreach (Uri uri in uris)
{
    string description = DescribeUri(uri);
    Console.WriteLine($"{uri} -> {description}");
}

// --------------------------------------------------------------
// 3. Nested Property Pattern
// --------------------------------------------------------------
Console.WriteLine("\n3. Nested Property Pattern");
Console.WriteLine(new string('-', 40));

foreach (Person person in people)
{
    // C# 10+ Simplified Syntax
    if (person is { Address.City: "Taipei", Address.District: "Xinyi" or "Daan" })
    {
        Console.WriteLine($"{person.Name} lives in Xinyi or Daan district, Taipei");
    }
}

// --------------------------------------------------------------
// 4. Combined with Type Pattern
// --------------------------------------------------------------
Console.WriteLine("\n4. Combined with Type Pattern");
Console.WriteLine(new string('-', 40));

object[] items =
[
    "",
    "Hello, World!",
    "This is a very long string that exceeds one hundred characters and should be classified as a long string in our pattern matching example.",
    new int[] { },
    new int[] { 1, 2, 3 }
];

foreach (object item in items)
{
    string description = DescribeItem(item);
    Console.WriteLine(description);
}

// --------------------------------------------------------------
// 5. Capturing Property Values
// --------------------------------------------------------------
Console.WriteLine("\n5. Capturing Property Values");
Console.WriteLine(new string('-', 40));

foreach (Person person in people)
{
    string description = DescribePerson(person);
    Console.WriteLine(description);
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Pattern Matching Methods
// ============================================================

static string DescribeUri(Uri uri) => uri switch
{
    { Scheme: "http", Port: 80 } => "Standard HTTP",
    { Scheme: "https", Port: 443 } => "Standard HTTPS",
    { Scheme: "ftp", Port: 21 } => "Standard FTP",
    { IsLoopback: true } => "Loopback address",
    _ => "Other"
};

static string DescribeItem(object obj) => obj switch
{
    string { Length: 0 } => "Empty string",
    string { Length: > 100 } => "Long string (over 100 characters)",
    string s => $"String, length {s.Length}",
    int[] { Length: 0 } => "Empty array",
    int[] arr => $"Integer array, {arr.Length} elements",
    _ => "Other"
};

static string DescribePerson(Person person) => person switch
{
    { Name: var name, Age: >= 18 } => $"{name} is an adult ({person.Age} years old)",
    { Name: var name } => $"{name} is a minor ({person.Age} years old)"
};

// ============================================================
// Data Classes
// ============================================================

public record Address(string City, string District);

public record Person(string Name, int Age, Address Address);
