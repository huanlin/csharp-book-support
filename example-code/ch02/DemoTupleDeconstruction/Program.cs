// 示範 Tuple 解構與自訂型別解構

// 解構 Tuple
var personTuple = ("Alice", 30);
(string name, int age) = personTuple;
Console.WriteLine($"解構結果: name={name}, age={age}");

// 使用 var 解構
var (lat, lon) = GetCoordinates("Taipei 101");
Console.WriteLine($"var 解構: lat={lat}, lon={lon}");

// 變數捨棄 (Discard)
var (latitude, _) = GetCoordinates("Taipei 101");
Console.WriteLine($"只取緯度: {latitude}");

// 自訂型別的解構
var personObj = new Person
{
    Name = "Bob",
    BirthDate = new DateTime(1980, 5, 15),
    Email = "bob@example.com"
};

// 使用 2 參數的 Deconstruct
var (pName, birthDate) = personObj;
Console.WriteLine($"\n解構 Person (2 參數): {pName}, {birthDate:yyyy-MM-dd}");

// 使用 3 參數的 Deconstruct
var (fullName, dob, email) = personObj;
Console.WriteLine($"解構 Person (3 參數): {fullName}, {dob:yyyy-MM-dd}, {email}");

static (double Lat, double Lon) GetCoordinates(string address)
{
    return (25.0330, 121.5654);
}

public class Person
{
    public string Name { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public string Email { get; set; } = "";

    public void Deconstruct(out string name, out DateTime birthDate)
    {
        name = Name;
        birthDate = BirthDate;
    }

    public void Deconstruct(out string name, out DateTime birthDate, out string email)
    {
        name = Name;
        birthDate = BirthDate;
        email = Email;
    }
}
