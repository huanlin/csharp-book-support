using System.Text;

// 示範陣列的淺層複製與深層複製

Console.WriteLine("=== 陣列的淺層複製 (value type 元素) ===");

int[] original1 = { 1, 2, 3 };
int[] copy1 = (int[])original1.Clone();
copy1[0] = 999;
Console.WriteLine($"original1[0] = {original1[0]}");  // 1 (不受影響)
Console.WriteLine($"copy1[0] = {copy1[0]}");          // 999
Console.WriteLine("結論: value type 元素，淺層複製已經足夠");

Console.WriteLine();
Console.WriteLine("=== 陣列的淺層複製 (reference type 元素) ===");

var original2 = new StringBuilder[]
{
    new StringBuilder("A"),
    new StringBuilder("B")
};
var copy2 = (StringBuilder[])original2.Clone();
copy2[0].Append("!");  // 修改 copy2 會影響 original2！
Console.WriteLine($"original2[0] = {original2[0]}");  // A! (受影響！)
Console.WriteLine($"copy2[0] = {copy2[0]}");          // A!
Console.WriteLine("結論: reference type 元素，淺層複製會共用同一個物件");

Console.WriteLine();
Console.WriteLine("=== 陣列的深層複製 (reference type 元素) ===");

var original3 = new StringBuilder[]
{
    new StringBuilder("X"),
    new StringBuilder("Y")
};
// 使用迴圈逐一複製
var deepCopy = new StringBuilder[original3.Length];
for (int i = 0; i < original3.Length; i++)
{
    deepCopy[i] = new StringBuilder(original3[i].ToString());
}
deepCopy[0].Append("!");
Console.WriteLine($"original3[0] = {original3[0]}");  // X (不受影響)
Console.WriteLine($"deepCopy[0] = {deepCopy[0]}");    // X!
Console.WriteLine("結論: 深層複製陣列需要逐一建立新的物件實例");
