// 示範：C# 14 extension 區塊語法
// 定義擴充屬性和擴充方法

// C# 14 使用 extension 關鍵字來定義擴充成員區塊
public static class StringExtensions
{
    // 擴充成員區塊（實例成員）
    extension(string s)
    {
        // 擴充屬性（避免與 string.IsNullOrEmpty 方法名稱衝突）
        public bool IsEmpty => string.IsNullOrEmpty(s);
        
        // 擴充方法
        public string Reverse()
        {
            if (string.IsNullOrEmpty(s)) return s;
            var chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}

// 示範主程式
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== C# 14 extension 區塊語法示範 ===\n");

        // 情境 1：使用擴充屬性
        Console.WriteLine("情境 1：擴充屬性 (不需括號)");
        string text = "hello";
        bool isEmpty = text.IsEmpty;
        Console.WriteLine($"  \"hello\".IsEmpty = {isEmpty}");
        
        string emptyText = "";
        Console.WriteLine($"  \"\".IsEmpty = {emptyText.IsEmpty}\n");

        // 情境 2：使用擴充方法
        Console.WriteLine("情境 2：擴充方法 (需要括號)");
        string reversed = text.Reverse();
        Console.WriteLine($"  \"hello\".Reverse() = \"{reversed}\"\n");

        // 情境 3：鏈式呼叫
        Console.WriteLine("情境 3：鏈式呼叫");
        string result = "world".Reverse();
        Console.WriteLine($"  \"world\".Reverse() = \"{result}\"");

        // 說明
        Console.WriteLine("\n=== 說明 ===");
        Console.WriteLine("- IsEmpty 是擴充「屬性」，呼叫時不需要括號");
        Console.WriteLine("- Reverse() 是擴充「方法」，需要括號");
        Console.WriteLine("- 這是 C# 14 的優勢：以前無法定義擴充屬性");
    }
}
