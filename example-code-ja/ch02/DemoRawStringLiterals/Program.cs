// デモ: Raw String Literals（C# 11+）

var json = """
    {
        "name": "Michael",
        "age": 50,
        "path": "C:\Windows\System32"
    }
    """;
Console.WriteLine("Raw String Literal（JSON）:");
Console.WriteLine(json);

// Raw String + 補間
var userName = "Michael";
var jsonWithInterpolation = $$"""
    {
        "name": "{{userName}}",
        "timestamp": "{{DateTime.Now:yyyy-MM-dd HH:mm:ss}}"
    }
    """;
Console.WriteLine("\nRaw String + 補間:");
Console.WriteLine(jsonWithInterpolation);

// CSS 例
var color = "#3498db";
var css = $$"""
    .button {
        background-color: {{color}};
        border-radius: 4px;
    }
    """;
Console.WriteLine("\nCSS 例:");
Console.WriteLine(css);
