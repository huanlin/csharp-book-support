// 示範屬性模式（Property Pattern）

Console.WriteLine("=== 屬性模式範例 ===\n");

// --------------------------------------------------------------
// 1. 基本屬性模式
// --------------------------------------------------------------
Console.WriteLine("1. 基本屬性模式");
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
        Console.WriteLine($"找到 Alice，30 歲！");
    }
    else if (person is { Address.City: "Taipei" })
    {
        Console.WriteLine($"{person.Name} 住在台北");
    }
    else
    {
        Console.WriteLine($"{person.Name} 住在其他地區");
    }
}

// --------------------------------------------------------------
// 2. 在 Switch 表達式中使用
// --------------------------------------------------------------
Console.WriteLine("\n2. 在 Switch 表達式中使用");
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
// 3. 巢狀屬性模式
// --------------------------------------------------------------
Console.WriteLine("\n3. 巢狀屬性模式");
Console.WriteLine(new string('-', 40));

foreach (Person person in people)
{
    // C# 10+ 簡化語法
    if (person is { Address.City: "Taipei", Address.District: "Xinyi" or "Daan" })
    {
        Console.WriteLine($"{person.Name} 住在台北信義或大安區");
    }
}

// --------------------------------------------------------------
// 4. 結合型別模式
// --------------------------------------------------------------
Console.WriteLine("\n4. 結合型別模式");
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
// 5. 擷取屬性值
// --------------------------------------------------------------
Console.WriteLine("\n5. 擷取屬性值");
Console.WriteLine(new string('-', 40));

foreach (Person person in people)
{
    string description = DescribePerson(person);
    Console.WriteLine(description);
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模式比對方法
// ============================================================

static string DescribeUri(Uri uri) => uri switch
{
    { Scheme: "http", Port: 80 } => "標準 HTTP",
    { Scheme: "https", Port: 443 } => "標準 HTTPS",
    { Scheme: "ftp", Port: 21 } => "標準 FTP",
    { IsLoopback: true } => "本機位址",
    _ => "其他"
};

static string DescribeItem(object obj) => obj switch
{
    string { Length: 0 } => "空字串",
    string { Length: > 100 } => "長字串（超過 100 字）",
    string s => $"字串，長度 {s.Length}",
    int[] { Length: 0 } => "空陣列",
    int[] arr => $"整數陣列，{arr.Length} 個元素",
    _ => "其他"
};

static string DescribePerson(Person person) => person switch
{
    { Name: var name, Age: >= 18 } => $"{name} 是成年人（{person.Age} 歲）",
    { Name: var name } => $"{name} 是未成年人（{person.Age} 歲）"
};

// ============================================================
// 資料類別
// ============================================================

public record Address(string City, string District);

public record Person(string Name, int Age, Address Address);
