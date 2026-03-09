// デモ: Property Pattern

Console.WriteLine("=== Property Pattern の例 ===\n");

// --------------------------------------------------------------
// 1. 基本的な Property Pattern
// --------------------------------------------------------------
Console.WriteLine("1. 基本的な Property Pattern");
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
        Console.WriteLine("Alice（30歳）を発見");
    }
    else if (person is { Address.City: "Taipei" })
    {
        Console.WriteLine($"{person.Name} は Taipei 在住");
    }
    else
    {
        Console.WriteLine($"{person.Name} はその他地域在住");
    }
}

// --------------------------------------------------------------
// 2. switch 式で利用
// --------------------------------------------------------------
Console.WriteLine("\n2. switch 式で利用");
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
// 3. ネストした Property Pattern
// --------------------------------------------------------------
Console.WriteLine("\n3. ネストした Property Pattern");
Console.WriteLine(new string('-', 40));

foreach (Person person in people)
{
    // C# 10+ 簡略構文
    if (person is { Address.City: "Taipei", Address.District: "Xinyi" or "Daan" })
    {
        Console.WriteLine($"{person.Name} は Taipei の Xinyi または Daan 区に在住");
    }
}

// --------------------------------------------------------------
// 4. type pattern と組み合わせ
// --------------------------------------------------------------
Console.WriteLine("\n4. type pattern と組み合わせ");
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
// 5. プロパティ値のキャプチャ
// --------------------------------------------------------------
Console.WriteLine("\n5. プロパティ値のキャプチャ");
Console.WriteLine(new string('-', 40));

foreach (Person person in people)
{
    string description = DescribePerson(person);
    Console.WriteLine(description);
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// パターンマッチメソッド
// ============================================================

static string DescribeUri(Uri uri) => uri switch
{
    { Scheme: "http", Port: 80 } => "標準 HTTP",
    { Scheme: "https", Port: 443 } => "標準 HTTPS",
    { Scheme: "ftp", Port: 21 } => "標準 FTP",
    { IsLoopback: true } => "ループバックアドレス",
    _ => "その他"
};

static string DescribeItem(object obj) => obj switch
{
    string { Length: 0 } => "空文字列",
    string { Length: > 100 } => "長い文字列（100文字超）",
    string s => $"文字列（長さ {s.Length}）",
    int[] { Length: 0 } => "空配列",
    int[] arr => $"int 配列（{arr.Length} 要素）",
    _ => "その他"
};

static string DescribePerson(Person person) => person switch
{
    { Name: var name, Age: >= 18 } => $"{name} は成人（{person.Age}歳）",
    { Name: var name } => $"{name} は未成年（{person.Age}歳）"
};

// ============================================================
// データクラス
// ============================================================

public record Address(string City, string District);

public record Person(string Name, int Age, Address Address);
