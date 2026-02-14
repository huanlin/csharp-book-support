// Demo: Continuous Query using the 'into' keyword

Console.WriteLine("=== 'into' Keyword Example ===\n");

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

// Filter length after removing vowels
var result = from n in names
             select n.Replace("a", "").Replace("e", "")
                     .Replace("i", "").Replace("o", "").Replace("u", "")
             into noVowel
             where noVowel.Length > 2
             orderby noVowel
             select noVowel;

Console.WriteLine($"Source: {string.Join(", ", names)}");
Console.WriteLine($"Length > 2 after removing vowels: {string.Join(", ", result)}");

Console.WriteLine("\n=== Example End ===");
