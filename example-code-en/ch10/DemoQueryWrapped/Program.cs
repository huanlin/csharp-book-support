// Demo: Wrapped Queries

Console.WriteLine("=== Wrapped Queries Example ===\n");

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

// Wrapped form (using internal query result as input for external query)
var wrappedQuery = from n1 in
                   (
                       from n2 in names
                       select n2.Replace("a", "").Replace("e", "")
                                .Replace("i", "").Replace("o", "").Replace("u", "")
                   )
                   where n1.Length > 2
                   orderby n1
                   select n1;

Console.WriteLine($"Wrapped query result: {string.Join(", ", wrappedQuery)}");

// Equivalent fluent syntax
var fluentQuery = names
    .Select(n => n.Replace("a", "").Replace("e", "")
                  .Replace("i", "").Replace("o", "").Replace("u", ""))
    .Where(n => n.Length > 2)
    .OrderBy(n => n);

Console.WriteLine($"Fluent syntax result: {string.Join(", ", fluentQuery)}");

Console.WriteLine("\n=== Example End ===");
