// デモ: 拡張メソッドの競合解決
// 複数の拡張メソッドが適合する場合、C# はより具体的な引数型を選ぶ。

// string 型向けの拡張メソッド
public static class StringHelper
{
    public static bool IsCapitalized(this string s)
    {
        Console.WriteLine("→ StringHelper.IsCapitalized (string) を呼び出し");
        return !string.IsNullOrEmpty(s) && char.IsUpper(s[0]);
    }
}

// object 型向けの拡張メソッド（より汎用的）
public static class ObjectHelper
{
    public static bool IsCapitalized(this object obj)
    {
        Console.WriteLine("→ ObjectHelper.IsCapitalized (object) を呼び出し");
        return obj?.ToString() is string str && str.Length > 0 && char.IsUpper(str[0]);
    }
}

// デモ用メインプログラム
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== 拡張メソッド競合解決デモ ===\n");

        // シナリオ 1: string 変数で IsCapitalized を呼ぶ
        Console.WriteLine("シナリオ 1: string 変数で IsCapitalized を呼ぶ");
        string text = "Perth";
        bool result1 = text.IsCapitalized();
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine("解説: string は object より具体的なので StringHelper 側が選ばれる\n");

        // シナリオ 2: object 変数で IsCapitalized を呼ぶ
        Console.WriteLine("シナリオ 2: object 変数で IsCapitalized を呼ぶ");
        object obj = "Sydney";
        bool result2 = obj.IsCapitalized();
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine("解説: 変数型が object なので ObjectHelper 側が選ばれる\n");

        // シナリオ 3: static 構文で呼び先を明示
        Console.WriteLine("シナリオ 3: static 構文で明示的に呼び出す");
        bool result3 = ObjectHelper.IsCapitalized("Melbourne");
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine("解説: 引数が string でも static 構文なら ObjectHelper を強制できる\n");

        // 優先順位ルールのまとめ
        Console.WriteLine("=== 優先順位ルール ===");
        Console.WriteLine("1. インスタンスメソッド > 拡張メソッド");
        Console.WriteLine("2. 具体型（class/struct）> インターフェイス");
        Console.WriteLine("3. より具体的な型 > より汎用的な型");
    }
}
