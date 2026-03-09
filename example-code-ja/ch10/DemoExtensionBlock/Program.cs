// デモ: C# 14 extension ブロック構文
// 拡張プロパティと拡張メソッドを定義

// C# 14 では extension キーワードで拡張メンバーブロックを定義する
public static class StringExtensions
{
    // 拡張メンバーブロック（インスタンスメンバー）
    extension(string s)
    {
        // 拡張プロパティ（string.IsNullOrEmpty メソッド名との衝突を避ける）
        public bool IsEmpty => string.IsNullOrEmpty(s);
        
        // 拡張メソッド
        public string Reverse()
        {
            if (string.IsNullOrEmpty(s)) return s;
            var chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}

// デモ用メインプログラム
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== C# 14 extension ブロック構文デモ ===\n");

        // シナリオ 1: 拡張プロパティの利用
        Console.WriteLine("シナリオ 1: 拡張プロパティ（括弧不要）");
        string text = "hello";
        bool isEmpty = text.IsEmpty;
        Console.WriteLine($"  \"hello\".IsEmpty = {isEmpty}");
        
        string emptyText = "";
        Console.WriteLine($"  \"\".IsEmpty = {emptyText.IsEmpty}\n");

        // シナリオ 2: 拡張メソッドの利用
        Console.WriteLine("シナリオ 2: 拡張メソッド（括弧が必要）");
        string reversed = text.Reverse();
        Console.WriteLine($"  \"hello\".Reverse() = \"{reversed}\"\n");

        // シナリオ 3: メソッドチェーン
        Console.WriteLine("シナリオ 3: メソッドチェーン");
        string result = "world".Reverse();
        Console.WriteLine($"  \"world\".Reverse() = \"{result}\"");

        // 解説
        Console.WriteLine("\n=== 解説 ===");
        Console.WriteLine("- IsEmpty は拡張「プロパティ」なので呼び出し時に括弧は不要");
        Console.WriteLine("- Reverse() は拡張「メソッド」なので括弧が必要");
        Console.WriteLine("- これが C# 14 の利点: 以前は拡張プロパティを定義できなかった");
    }
}
