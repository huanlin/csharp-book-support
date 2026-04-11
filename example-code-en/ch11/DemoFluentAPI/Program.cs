// Demo: Fluent API Design
// Extension methods are a key technology for implementing Fluent APIs

// Define example classes
public record User(string Name, int Age);
public record UserDto(string DisplayName, bool IsAdult);

// Fluent API Extension Methods
public static class FluentExtensions
{
    /// <summary>
    /// Executes a side effect (like logging), then returns the original object to continue chaining.
    /// The name comes from Ruby, meaning to "tap" the object.
    /// </summary>
    public static T Tap<T>(this T obj, Action<T> action)
    {
        action(obj);
        return obj;
    }

    /// <summary>
    /// Converts an object to another type, similar to LINQ's Select, but acts on a single object.
    /// </summary>
    public static TResult Map<TSource, TResult>(this TSource obj, Func<TSource, TResult> selector)
    {
        return selector(obj);
    }

    /// <summary>
    /// Conditional execution: If condition is true, execute the specified action.
    /// </summary>
    public static T When<T>(this T obj, bool condition, Action<T> action)
    {
        if (condition)
            action(obj);
        return obj;
    }
}

// Demo Main Program
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Fluent API Design Demo ===\n");

        // Scenario 1: Basic Tap and Map chaining
        Console.WriteLine("Scenario 1: Creating a user and converting to DTO");
        var userDto = new User("Alice", 30)
            .Tap(u => Console.WriteLine($"  → Creating user: {u.Name}, Age {u.Age}"))
            .Map(u => new UserDto(u.Name, u.Age >= 18))
            .Tap(dto => Console.WriteLine($"  → Converting to DTO: {dto.DisplayName}, Adult: {dto.IsAdult}"));

        Console.WriteLine($"  Result: {userDto}\n");

        // Scenario 2: Conditional Execution
        Console.WriteLine("Scenario 2: Conditional Execution (When)");
        var user2 = new User("Bob", 16)
            .Tap(u => Console.WriteLine($"  → Creating user: {u.Name}"))
            .When(true, u => Console.WriteLine($"  → Condition is true, executing extra logic"));

        // Scenario 3: Comparison with traditional approach
        Console.WriteLine("\nScenario 3: Traditional approach vs. Fluent API");
        Console.WriteLine("Traditional approach requires multiple intermediate variables:");
        Console.WriteLine("  var user = new User(\"Charlie\", 25);");
        Console.WriteLine("  Console.WriteLine($\"Created: {user.Name}\");");
        Console.WriteLine("  var dto = new UserDto(user.Name, user.Age >= 18);");
        Console.WriteLine("  Console.WriteLine($\"Converted: {dto.DisplayName}\");");
        Console.WriteLine("\nFluent API approach is more fluid; the code reads like a \"story\" from top to bottom.");
    }
}
