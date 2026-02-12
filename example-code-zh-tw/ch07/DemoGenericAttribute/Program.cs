Console.WriteLine("=== C# 11 泛型 Attribute 範例 ===\n");

// 使用泛型 Attribute
var method = typeof(MyClass).GetMethod(nameof(MyClass.Method));
var attr = method?.GetCustomAttributes(typeof(GenericAttribute<string>), false).FirstOrDefault() as GenericAttribute<string>;

if (attr != null)
{
    Console.WriteLine($"找到 GenericAttribute<string>！");
    Console.WriteLine($"型別參數 T 是：{typeof(string).Name}");
}
else
{
    Console.WriteLine("找不到 Attribute");
}

Console.WriteLine("\n=== 範例結束 ===");


// 定義泛型 Attribute (C# 11)
public class GenericAttribute<T> : Attribute 
{ 
    public GenericAttribute()
    {
        Console.WriteLine($"GenericAttribute 建構子被呼叫，T = {typeof(T).Name}");
    }
}

public class MyClass
{
    // 使用泛型 Attribute
    [GenericAttribute<string>()]
    public void Method() { }
}
