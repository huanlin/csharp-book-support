// 示範 typeof 與未綁定泛型型別

Console.WriteLine("=== typeof 與未綁定泛型型別 ===\n");

Console.WriteLine("1. 未綁定泛型型別");
Console.WriteLine(new string('-', 40));

Type a1 = typeof(MyGenericList<>);
Type a2 = typeof(MyPair<,>);

Console.WriteLine($"MyGenericList<> 名稱：{a1.Name}");
Console.WriteLine($"MyPair<,> 名稱：{a2.Name}");

Type a3 = typeof(MyGenericList<int>);
Type a4 = typeof(MyPair<string, int>);

Console.WriteLine($"\nMyGenericList<int> 名稱：{a3.Name}");
Console.WriteLine($"MyPair<string, int> 完整名稱：{a4.FullName}");

Console.WriteLine("\n2. 在泛型類別中取得 typeof(T)");
Console.WriteLine(new string('-', 40));

var holder = new TypeReporter<DateTime>();
holder.PrintType();

Console.WriteLine("\n3. 反射與泛型");
Console.WriteLine(new string('-', 40));

object customerRepo = RepositoryFactory.CreateRepository(typeof(Customer));
Console.WriteLine($"建立的物件型別：{customerRepo.GetType().Name}");

Console.WriteLine("\n=== 範例結束 ===");

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
        Console.WriteLine($"T 的型別是：{typeof(T).FullName}");
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
