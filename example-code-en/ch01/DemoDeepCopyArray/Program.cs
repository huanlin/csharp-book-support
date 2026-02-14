using System.Text;

// Demo: Shallow Copy vs. Deep Copy of Arrays

Console.WriteLine("=== Shallow Copy of Arrays (value type elements) ===");

int[] original1 = { 1, 2, 3 };
int[] copy1 = (int[])original1.Clone();
copy1[0] = 999;
Console.WriteLine($"original1[0] = {original1[0]}");  // 1 (Not affected)
Console.WriteLine($"copy1[0] = {copy1[0]}");          // 999
Console.WriteLine("Conclusion: Shallow copy is sufficient for value type elements");

Console.WriteLine();
Console.WriteLine("=== Shallow Copy of Arrays (reference type elements) ===");

var original2 = new StringBuilder[]
{
    new StringBuilder("A"),
    new StringBuilder("B")
};
var copy2 = (StringBuilder[])original2.Clone();
copy2[0].Append("!");  // Modifying copy2 affects original2!
Console.WriteLine($"original2[0] = {original2[0]}");  // A! (Affected!)
Console.WriteLine($"copy2[0] = {copy2[0]}");          // A!
Console.WriteLine("Conclusion: Shallow copy shares the same object for reference type elements");

Console.WriteLine();
Console.WriteLine("=== Deep Copy of Arrays (reference type elements) ===");

var original3 = new StringBuilder[]
{
    new StringBuilder("X"),
    new StringBuilder("Y")
};
// Use a loop to copy each element
var deepCopy = new StringBuilder[original3.Length];
for (int i = 0; i < original3.Length; i++)
{
    deepCopy[i] = new StringBuilder(original3[i].ToString());
}
deepCopy[0].Append("!");
Console.WriteLine($"original3[0] = {original3[0]}");  // X (Not affected)
Console.WriteLine($"deepCopy[0] = {deepCopy[0]}");    // X!
Console.WriteLine("Conclusion: Deep copying an array requires manually instantiating new objects for each element");
