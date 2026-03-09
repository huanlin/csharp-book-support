Console.WriteLine("=== C# 11 Generic Attribute の例 ===\n");

// Generic Attribute 利用
var method = typeof(MyClass).GetMethod(nameof(MyClass.Method));
var attr = method?.GetCustomAttributes(typeof(GenericAttribute<string>), false).FirstOrDefault() as GenericAttribute<string>;

if (attr != null)
{
    Console.WriteLine("GenericAttribute<string> を検出");
    Console.WriteLine($"型引数 T は: {typeof(string).Name}");
}
else
{
    Console.WriteLine("属性が見つかりません");
}

Console.WriteLine("\n=== 例の終了 ===");

// Generic Attribute 定義（C# 11）
public class GenericAttribute<T> : Attribute
{
    public GenericAttribute()
    {
        Console.WriteLine($"GenericAttribute コンストラクター呼び出し, T = {typeof(T).Name}");
    }
}

public class MyClass
{
    [GenericAttribute<string>()]
    public void Method() { }
}
