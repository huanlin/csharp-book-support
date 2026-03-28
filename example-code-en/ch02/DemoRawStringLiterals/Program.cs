// Demo: Raw String Literals (C# 11+)

var json = """
    {
        "name": "Michael",
        "age": 50,
        "path": "C:\\Windows\\System32"
    }
    """;
Console.WriteLine("Raw String Literal (JSON):");
Console.WriteLine(json);

// Raw String + Interpolation
var userName = "Michael";
var jsonWithInterpolation = $$"""
    {
        "name": "{{userName}}",
        "timestamp": "{{DateTime.Now:yyyy-MM-dd HH:mm:ss}}"
    }
    """;
Console.WriteLine($"\nRaw String + Interpolation:");
Console.WriteLine(jsonWithInterpolation);

// CSS Example
var color = "#3498db";
var css = $$"""
    .button {
        background-color: {{color}};
        border-radius: 4px;
    }
    """;
Console.WriteLine($"\nCSS Example:");
Console.WriteLine(css);
