// Demo: Tuple Deconstruction and Custom Type Deconstruction

// Deconstructing a Tuple
var personTuple = ("Alice", 30);
(string name, int age) = personTuple;
Console.WriteLine($"Deconstruction result: name={name}, age={age}");

// Deconstructing using var
var (lat, lon) = GetCoordinates("Taipei 101");
Console.WriteLine($"var deconstruction: lat={lat}, lon={lon}");

// Discards
var (latitude, _) = GetCoordinates("Taipei 101");
Console.WriteLine($"Latitude only: {latitude}");

// Deconstructing a custom type
var personObj = new Person
{
    Name = "Bob",
    Birthday = new DateTime(1980, 5, 15),
    Email = "bob@example.com"
};

// Use Deconstruct with 2 parameters
var (pName, birthday) = personObj;
Console.WriteLine($"\nDeconstructed Person (2 parameters): {pName}, {birthday:yyyy-MM-dd}");

// Use Deconstruct with 3 parameters
var (fullName, dob, email) = personObj;
Console.WriteLine($"Deconstructed Person (3 parameters): {fullName}, {dob:yyyy-MM-dd}, {email}");

static (double Lat, double Lon) GetCoordinates(string address)
{
    return (25.0330, 121.5654);
}

public class Person
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }
    public string Email { get; set; } = "";

    public void Deconstruct(out string name, out DateTime birthday)
    {
        name = Name;
        birthday = Birthday;
    }

    public void Deconstruct(out string name, out DateTime birthday, out string email)
    {
        name = Name;
        birthday = Birthday;
        email = Email;
    }
}
