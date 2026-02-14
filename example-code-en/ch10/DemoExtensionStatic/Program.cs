// Demo: C# 14 Static Extension Members
// Define static extension methods, static extension properties, and extension operators

// Extension static members: Called via type name, not object instance
public static class EnumerableExtensions
{
    // Extension Instance Member (with parameter name 'source')
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmpty => !source.Any();
    }
    
    // Extension with reference type constraint
    extension<TSource>(IEnumerable<TSource> source) where TSource : class?
    {
        public IEnumerable<TSource> WhereNotNull()
        {
            return source.Where(x => x is not null);
        }
    }
    
    // Extension Static Member (Note: no parameter name, only type)
    extension<TSource>(IEnumerable<TSource>)
    {
        // Static Extension Method
        public static IEnumerable<TSource> Combine(
            IEnumerable<TSource> first, 
            IEnumerable<TSource> second) 
            => first.Concat(second);
        
        // Static Extension Property
        public static IEnumerable<TSource> Empty 
            => Enumerable.Empty<TSource>();
        
        // Extension Operator
        public static IEnumerable<TSource> operator +(
            IEnumerable<TSource> left, 
            IEnumerable<TSource> right) 
            => left.Concat(right);
    }
}

// Demo Main Program
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== C# 14 Static Extension Member Demo ===\n");

        // Scenario 1: Instance Extension Member (Called via object)
        Console.WriteLine("Scenario 1: Instance Extension Member");
        var numbers = new[] { 1, 2, 3 };
        Console.WriteLine($"  numbers.IsEmpty = {numbers.IsEmpty}");
        
        var emptyList = Array.Empty<int>();
        Console.WriteLine($"  emptyList.IsEmpty = {emptyList.IsEmpty}\n");

        // Scenario 2: Static Extension Member (Called via type name)
        Console.WriteLine("Scenario 2: Static Extension Member");
        var first = new[] { 1, 2 };
        var second = new[] { 3, 4 };
        var combined = IEnumerable<int>.Combine(first, second);
        Console.WriteLine($"  IEnumerable<int>.Combine([1,2], [3,4]) = [{string.Join(", ", combined)}]");
        
        var empty = IEnumerable<string>.Empty;
        Console.WriteLine($"  IEnumerable<string>.Empty.Count() = {empty.Count()}\n");

        // Scenario 3: Extension Operator (Using + syntax)
        Console.WriteLine("Scenario 3: Extension Operator");
        var list1 = new[] { "a", "b" };
        var list2 = new[] { "c", "d" };
        var merged = list1 + list2;
        Console.WriteLine($"  [\"a\",\"b\"] + [\"c\",\"d\"] = [{string.Join(", ", merged)}]\n");

        // Scenario 4: WhereNotNull (Extension with type constraint)
        Console.WriteLine("Scenario 4: Extension method with type constraint");
        var names = new[] { "Alice", null, "Bob", null };
        var validNames = names.WhereNotNull();
        Console.WriteLine($"  After filtering null: [{string.Join(", ", validNames)}]");

        // Explanation
        Console.WriteLine("\n=== Explanation ===");
        Console.WriteLine("- Instance members: extension<T>(IEnumerable<T> source) has parameter name");
        Console.WriteLine("- Static members: extension<T>(IEnumerable<T>) has no parameter name");
        Console.WriteLine("- Extension operators: Can \"add\" operators to existing types");
    }
}
