// 示範：擴充方法之間的衝突解決
// 當多個擴充方法有相容的簽章時，C# 會選擇參數型別較具體的版本。

// 針對 string 型別的擴充方法
public static class StringHelper
{
    public static bool IsCapitalized(this string s)
    {
        Console.WriteLine("→ 呼叫 StringHelper.IsCapitalized (string)");
        return !string.IsNullOrEmpty(s) && char.IsUpper(s[0]);
    }
}

// 針對 object 型別的擴充方法（較泛化）
public static class ObjectHelper
{
    public static bool IsCapitalized(this object obj)
    {
        Console.WriteLine("→ 呼叫 ObjectHelper.IsCapitalized (object)");
        return obj?.ToString() is string str && str.Length > 0 && char.IsUpper(str[0]);
    }
}

// 示範主程式
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== 擴充方法衝突解決示範 ===\n");

        // 情境 1：string 變數呼叫 IsCapitalized
        Console.WriteLine("情境 1：string 變數呼叫 IsCapitalized");
        string text = "Perth";
        bool result1 = text.IsCapitalized();
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine("說明: 因為 string 比 object 更具體，所以選擇 StringHelper 的版本\n");

        // 情境 2：object 變數呼叫 IsCapitalized
        Console.WriteLine("情境 2：object 變數呼叫 IsCapitalized");
        object obj = "Sydney";
        bool result2 = obj.IsCapitalized();
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine("說明: 變數型別是 object，所以選擇 ObjectHelper 的版本\n");

        // 情境 3：明確使用靜態語法指定要呼叫哪個版本
        Console.WriteLine("情境 3：明確使用靜態語法");
        bool result3 = ObjectHelper.IsCapitalized("Melbourne");
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine("說明: 即使傳入 string，透過靜態語法仍可強制呼叫 ObjectHelper 的版本\n");

        // 優先順序規則總結
        Console.WriteLine("=== 優先順序規則 ===");
        Console.WriteLine("1. 實例方法 > 擴充方法");
        Console.WriteLine("2. 具體型別 (class/struct) > 介面 (interface)");
        Console.WriteLine("3. 較具體的型別 > 較泛化的型別");
    }
}
