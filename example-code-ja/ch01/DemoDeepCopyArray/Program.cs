using System.Text;

// デモ: 配列のシャローコピーとディープコピー

Console.WriteLine("=== 配列のシャローコピー（値型要素） ===");

int[] original1 = { 1, 2, 3 };
int[] copy1 = (int[])original1.Clone();
copy1[0] = 999;
Console.WriteLine($"original1[0] = {original1[0]}");  // 1（影響なし）
Console.WriteLine($"copy1[0] = {copy1[0]}");          // 999
Console.WriteLine("結論：int のように参照フィールドを含まない値型要素なら、シャローコピーで十分なことが多い");

Console.WriteLine();
Console.WriteLine("=== 配列のシャローコピー（参照型要素） ===");

var original2 = new StringBuilder[]
{
    new StringBuilder("A"),
    new StringBuilder("B")
};
var copy2 = (StringBuilder[])original2.Clone();
copy2[0].Append("!");  // copy2 の変更が original2 に影響
Console.WriteLine($"original2[0] = {original2[0]}");  // A!（影響あり）
Console.WriteLine($"copy2[0] = {copy2[0]}");          // A!
Console.WriteLine("結論: 参照型要素では、シャローコピーだと同じオブジェクトを共有する");

Console.WriteLine();
Console.WriteLine("=== 配列のディープコピー（参照型要素） ===");

var original3 = new StringBuilder[]
{
    new StringBuilder("X"),
    new StringBuilder("Y")
};
// 各要素を新規生成してコピー
var deepCopy = new StringBuilder[original3.Length];
for (int i = 0; i < original3.Length; i++)
{
    deepCopy[i] = new StringBuilder(original3[i].ToString());
}
deepCopy[0].Append("!");
Console.WriteLine($"original3[0] = {original3[0]}");  // X（影響なし）
Console.WriteLine($"deepCopy[0] = {deepCopy[0]}");    // X!
Console.WriteLine("結論: 配列のディープコピーでは、要素ごとに新しいインスタンスを作る必要がある");
