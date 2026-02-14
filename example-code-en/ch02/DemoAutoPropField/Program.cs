// Demo: C# 14 field keyword
using System.Runtime.CompilerServices;

var person = new Person();
person.Name = "Li Si";
person.Age = 30;
person.Email = "test@example.com";
Console.WriteLine($"Person: Name={person.Name}, Age={person.Age}, ID={person.Id}");
Console.WriteLine($"Email: {person.Email}");

// Testing validation logic
try
{
    person.Age = -5;  // Should throw an exception
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\nValidation failed: {ex.Message}");
}

// Testing property change notification
Console.WriteLine("\nTesting property change notification:");
person.Email = "new@example.com";  // Should trigger OnPropertyChanged
person.Email = "new@example.com";  // Same value; should not trigger

public class Person
{
    // Read-only property: set during initialization
    public string Id { get; } = Guid.NewGuid().ToString()[..8];

    // Property with validation: using field keyword
    public string Name
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    } = "";

    // Property with validation: setter using field
    public int Age
    {
        get;
        set
        {
            if (value < 0)
                throw new ArgumentException("Age cannot be negative!");
            field = value;
        }
    }

    // Property with notification (common in MVVM)
    public string Email
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();
            }
        }
    } = "";

    private void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        Console.WriteLine($"  Property {name} changed to: {Email}");
    }
}
