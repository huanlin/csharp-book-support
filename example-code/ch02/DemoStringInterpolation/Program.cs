// 示範字串插補與原始字串常值

// 基本字串插補
var name = "Michael";
var age = 50;
var s1 = string.Format("Name: {0}, Age: {1}", name, age);  // 傳統寫法
var s2 = $"Name: {name}, Age: {age}";                       // 字串插補
Console.WriteLine(s1);
Console.WriteLine(s2);

// 格式化與對齊
var price = 123.456m;
var dt = DateTime.Now;

Console.WriteLine($"\n格式化範例:");
Console.WriteLine($"  Price: {price:C2}");           // 貨幣格式
Console.WriteLine($"  Date: {dt:yyyy-MM-dd}");       // 日期格式
Console.WriteLine($"  Hex: {255:X4}");               // 十六進位

// 對齊
var playerName = "Jordan";
var score = 95;
Console.WriteLine($"\n對齊範例:");
Console.WriteLine($"|{playerName,-10}|{score,5}|");  // 左對齊/右對齊

// 常數插補字串 (C# 10+)
const string Greeting = "Hello";
const string Target = "World";
const string Message = $"{Greeting}, {Target}!";
Console.WriteLine($"\n常數插補: {Message}");

// 原始字串常值 (C# 11+)
var json = """
    {
        "name": "Michael",
        "age": 50,
        "path": "C:\\Windows\\System32"
    }
    """;
Console.WriteLine($"\n原始字串常值 (JSON):");
Console.WriteLine(json);

// 原始字串 + 插補
var userName = "Michael";
var jsonWithInterpolation = $$"""
    {
        "name": "{{userName}}",
        "timestamp": "{{DateTime.Now:yyyy-MM-dd HH:mm:ss}}"
    }
    """;
Console.WriteLine($"\n原始字串 + 插補:");
Console.WriteLine(jsonWithInterpolation);

// CSS 範例
var color = "#3498db";
var css = $$"""
    .button {
        background-color: {{color}};
        border-radius: 4px;
    }
    """;
Console.WriteLine($"\nCSS 範例:");
Console.WriteLine(css);
