// デモ: Fluent API 設計
// 拡張メソッドは Fluent API 実装の中核技術

// 例のクラス定義
public record User(string Name, int Age);
public record UserDto(string DisplayName, bool IsAdult);

// Fluent API 用拡張メソッド
public static class FluentExtensions
{
    /// <summary>
    /// 副作用処理（ログなど）を実行して元オブジェクトを返し、チェーンを継続する。
    /// 名前は Ruby の "tap" に由来する。
    /// </summary>
    public static T Tap<T>(this T obj, Action<T> action)
    {
        action(obj);
        return obj;
    }

    /// <summary>
    /// オブジェクトを別型へ変換する。単一オブジェクト版の LINQ Select に近い。
    /// </summary>
    public static TResult Map<TSource, TResult>(this TSource obj, Func<TSource, TResult> selector)
    {
        return selector(obj);
    }

    /// <summary>
    /// 条件実行: condition が true の場合のみ action を実行する。
    /// </summary>
    public static T When<T>(this T obj, bool condition, Action<T> action)
    {
        if (condition)
            action(obj);
        return obj;
    }
}

// デモ用メインプログラム
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Fluent API 設計デモ ===\n");

        // シナリオ 1: Tap と Map の基本チェーン
        Console.WriteLine("シナリオ 1: ユーザー作成と DTO 変換");
        var userDto = new User("Alice", 30)
            .Tap(u => Console.WriteLine($"  → ユーザー作成: {u.Name}, 年齢 {u.Age}"))
            .Map(u => new UserDto(u.Name, u.Age >= 18))
            .Tap(dto => Console.WriteLine($"  → DTO 変換: {dto.DisplayName}, 成人: {dto.IsAdult}"));

        Console.WriteLine($"  結果: {userDto}\n");

        // シナリオ 2: 条件付き実行
        Console.WriteLine("シナリオ 2: 条件付き実行（When）");
        var user2 = new User("Bob", 16)
            .Tap(u => Console.WriteLine($"  → ユーザー作成: {u.Name}"))
            .When(true, u => Console.WriteLine("  → 条件が true のため追加ロジックを実行"));

        // シナリオ 3: 従来方式との比較
        Console.WriteLine("\nシナリオ 3: 従来方式 vs Fluent API");
        Console.WriteLine("従来方式は中間変数が多くなりやすい:");
        Console.WriteLine("  var user = new User(\"Charlie\", 25);");
        Console.WriteLine("  Console.WriteLine($\"Created: {user.Name}\");");
        Console.WriteLine("  var dto = new UserDto(user.Name, user.Age >= 18);");
        Console.WriteLine("  Console.WriteLine($\"Converted: {dto.DisplayName}\");");
        Console.WriteLine("\nFluent API は上から下へ " +
                          "ストーリーのように読めるため可読性が高い。");
    }
}
