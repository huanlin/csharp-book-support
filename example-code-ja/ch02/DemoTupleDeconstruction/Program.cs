// デモ: Tuple 分解とカスタム型分解

// Tuple の分解
var personTuple = ("Alice", 30);
(string name, int age) = personTuple;
Console.WriteLine($"分解結果: name={name}, age={age}");

// var を使った分解
var (lat, lon) = GetCoordinates("Taipei 101");
Console.WriteLine($"var 分解: lat={lat}, lon={lon}");

// 破棄変数
var (latitude, _) = GetCoordinates("Taipei 101");
Console.WriteLine($"緯度のみ: {latitude}");

// カスタム型の分解
var personObj = new Person
{
    Name = "Bob",
    Birthday = new DateTime(1980, 5, 15),
    Email = "bob@example.com"
};

// 引数2個版 Deconstruct
var (pName, birthday) = personObj;
Console.WriteLine($"\nPerson 分解（2引数）: {pName}, {birthday:yyyy-MM-dd}");

// 引数3個版 Deconstruct
var (fullName, dob, email) = personObj;
Console.WriteLine($"Person 分解（3引数）: {fullName}, {dob:yyyy-MM-dd}, {email}");

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
