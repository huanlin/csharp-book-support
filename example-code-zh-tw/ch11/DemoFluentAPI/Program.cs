// 示範：Fluent API 設計
// 擴充方法是實作 Fluent API 的關鍵技術

// 定義範例類別
public record User(string Name, int Age);
public record UserDto(string DisplayName, bool IsAdult);

// Fluent API 擴充方法
public static class FluentExtensions
{
    /// <summary>
    /// 執行一個副作用（如 log），然後回傳原物件繼續鏈接。
    /// 名稱來自 Ruby，意思是「輕輕點一下」。
    /// </summary>
    public static T Tap<T>(this T obj, Action<T> action)
    {
        action(obj);
        return obj;
    }

    /// <summary>
    /// 將物件轉換為另一個型別，類似 LINQ 的 Select，但作用於單一物件。
    /// </summary>
    public static TResult Map<TSource, TResult>(this TSource obj, Func<TSource, TResult> selector)
    {
        return selector(obj);
    }

    /// <summary>
    /// 條件式執行：如果條件為真，執行指定動作。
    /// </summary>
    public static T When<T>(this T obj, bool condition, Action<T> action)
    {
        if (condition)
            action(obj);
        return obj;
    }
}

// 示範主程式
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Fluent API 設計示範 ===\n");

        // 情境 1：基本的 Tap 和 Map 鏈接
        Console.WriteLine("情境 1：建立使用者並轉換為 DTO");
        var userDto = new User("Alice", 30)
            .Tap(u => Console.WriteLine($"  → 建立使用者：{u.Name}, 年齡 {u.Age}"))
            .Map(u => new UserDto(u.Name, u.Age >= 18))
            .Tap(dto => Console.WriteLine($"  → 轉換為 DTO：{dto.DisplayName}, 成年: {dto.IsAdult}"));

        Console.WriteLine($"  結果：{userDto}\n");

        // 情境 2：條件式執行
        Console.WriteLine("情境 2：條件式執行 (When)");
        var user2 = new User("Bob", 16)
            .Tap(u => Console.WriteLine($"  → 建立使用者：{u.Name}"))
            .When(true, u => Console.WriteLine($"  → 條件為真，執行額外邏輯"));

        // 情境 3：對比傳統寫法
        Console.WriteLine("\n情境 3：傳統寫法 vs Fluent API");
        Console.WriteLine("傳統寫法需要多個中間變數：");
        Console.WriteLine("  var user = new User(\"Charlie\", 25);");
        Console.WriteLine("  Console.WriteLine($\"建立：{user.Name}\");");
        Console.WriteLine("  var dto = new UserDto(user.Name, user.Age >= 18);");
        Console.WriteLine("  Console.WriteLine($\"轉換：{dto.DisplayName}\");");
        Console.WriteLine("\nFluent API 寫法更流暢，程式碼從上到下「說故事」。");
    }
}
