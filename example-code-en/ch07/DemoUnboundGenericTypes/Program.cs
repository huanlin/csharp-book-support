// Demo: typeof and Unbound Generic Types

Console.WriteLine("=== typeof and Unbound Generic Types ===\n");

Console.WriteLine("1. Unbound generic types");
Console.WriteLine(new string('-', 40));

Type a1 = typeof(MyGenericList<>);
Type a2 = typeof(MyPair<,>);

Console.WriteLine($"MyGenericList<> Name: {a1.Name}");
Console.WriteLine($"MyPair<,> Name: {a2.Name}");

Type a3 = typeof(MyGenericList<int>);
Type a4 = typeof(MyPair<string, int>);

Console.WriteLine($"\nMyGenericList<int> Name: {a3.Name}");
Console.WriteLine($"MyPair<string, int> Full Name: {a4.FullName}");

Console.WriteLine("\n2. Getting typeof(T) inside a generic class");
Console.WriteLine(new string('-', 40));

var holder = new TypeReporter<DateTime>();
holder.PrintType();

Console.WriteLine("\n3. Reflection and generics");
Console.WriteLine(new string('-', 40));

object customerRepo = RepositoryFactory.CreateRepository(typeof(Customer));
Console.WriteLine($"Created object type: {customerRepo.GetType().Name}");

Console.WriteLine("\n=== Example End ===");

public class MyGenericList<T>
{
}

public readonly struct MyPair<TKey, TValue>
{
}

public class TypeReporter<T>
{
    public void PrintType()
    {
        Console.WriteLine($"T is: {typeof(T).FullName}");
    }
}

public class Repository<T>
{
}

public sealed class Customer
{
}

public static class RepositoryFactory
{
    public static object CreateRepository(Type entityType)
    {
        Type openType = typeof(Repository<>);
        Type closedType = openType.MakeGenericType(entityType);
        return Activator.CreateInstance(closedType)!;
    }
}
