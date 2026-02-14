// Demo: Tuple Basics

// Tuple syntax definition
(string Name, int Age) person = ("Bob", 23);
Console.WriteLine($"person.Name: {person.Name}");
Console.WriteLine($"person.Age: {person.Age}");

// Returning multiple values from a method
var coords = GetCoordinates("Taipei 101");
Console.WriteLine($"\nLatitude: {coords.Lat}, Longitude: {coords.Lon}");

static (double Lat, double Lon) GetCoordinates(string address)
{
    // Simulate Map API query
    return (25.0330, 121.5654);
}
