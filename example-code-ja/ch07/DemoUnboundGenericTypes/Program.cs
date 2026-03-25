// デモ: typeof と未バインドのジェネリック型

Console.WriteLine("=== typeof と未バインドのジェネリック型 ===\n");

Console.WriteLine("1. 未バインドのジェネリック型");
Console.WriteLine(new string('-', 40));

Type a1 = typeof(MyGenericList<>);
Type a2 = typeof(MyPair<,>);

Console.WriteLine($"MyGenericList<> 名称: {a1.Name}");
Console.WriteLine($"MyPair<,> 名称: {a2.Name}");

Type a3 = typeof(MyGenericList<int>);
Type a4 = typeof(MyPair<string, int>);

Console.WriteLine($"\nMyGenericList<int> 名称: {a3.Name}");
Console.WriteLine($"MyPair<string, int> 完全名: {a4.FullName}");

Console.WriteLine("\n2. ジェネリック クラス内で typeof(T) を取得");
Console.WriteLine(new string('-', 40));

var holder = new TypeReporter<DateTime>();
holder.PrintType();

Console.WriteLine("\n3. リフレクションとジェネリック");
Console.WriteLine(new string('-', 40));

object customerRepo = RepositoryFactory.CreateRepository(typeof(Customer));
Console.WriteLine($"生成されたオブジェクト型: {customerRepo.GetType().Name}");

Console.WriteLine("\n=== 例の終了 ===");

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
        Console.WriteLine($"T の型: {typeof(T).FullName}");
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
