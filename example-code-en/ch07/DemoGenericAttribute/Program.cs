Console.WriteLine("=== C# 11 Generic Attribute Example ===\n");

// Using Generic Attribute
var method = typeof(MyClass).GetMethod(nameof(MyClass.Method));
var attr = method?.GetCustomAttributes(typeof(GenericAttribute<string>), false).FirstOrDefault() as GenericAttribute<string>;

if (attr != null)
{
    Console.WriteLine($"Found GenericAttribute<string>!");
    Console.WriteLine($"Type parameter T is: {typeof(string).Name}");
}
else
{
    Console.WriteLine("Attribute not found");
}

Console.WriteLine("\n=== Example End ===");


// Defining Generic Attribute (C# 11)
public class GenericAttribute<T> : Attribute 
{ 
    public GenericAttribute()
    {
        Console.WriteLine($"GenericAttribute constructor called, T = {typeof(T).Name}");
    }
}

public class MyClass
{
    // Using Generic Attribute
    [GenericAttribute<string>()]
    public void Method() { }
}
