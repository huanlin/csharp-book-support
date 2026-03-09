// デモ: 従来の文字列解析 vs Span を使ったゼロアロケーション解析

Console.WriteLine("=== 文字列解析比較（割り当てあり vs なし） ===\n");

string input = "123,456,789";
Console.WriteLine($"入力文字列: \"{input}\"");

// 従来方式: Substring が新しい文字列を生成（割り当てあり）
int val1 = LegacyParser.Parse(input);
Console.WriteLine($"ParseLegacy 結果: {val1}");

// モダン方式: Span を使い割り当てゼロ
int val2 = ModernParser.Parse(input.AsSpan());
Console.WriteLine($"ParseModern 結果: {val2}");

// ヘルパークラス

public static class LegacyParser
{
    public static int Parse(string input)
    {
        int commaPos = input.IndexOf(',');
        // Substring は新しい string を生成（割り当てあり）
        string firstStr = input.Substring(0, commaPos);
        return int.Parse(firstStr);
    }
}

public static class ModernParser
{
    public static int Parse(ReadOnlySpan<char> input)
    {
        int commaPos = input.IndexOf(',');
        // Slice は参照範囲を切るだけ（割り当てなし）
        ReadOnlySpan<char> firstSpan = input.Slice(0, commaPos);
        // int.Parse は ReadOnlySpan<char> をサポート
        return int.Parse(firstSpan);
    }
}
