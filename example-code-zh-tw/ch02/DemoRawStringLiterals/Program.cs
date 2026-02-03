// 示範原始字串常值 (C# 11+)

var json = """
    {
        "name": "Michael",
        "age": 50,
        "path": "C:\\Windows\\System32"
    }
    """;
Console.WriteLine("原始字串常值 (JSON):");
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
