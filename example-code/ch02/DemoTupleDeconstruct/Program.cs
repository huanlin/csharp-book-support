// 示範 Tuple 與解構

// Tuple 語法基礎
(string Name, int Age) person = ("Bob", 23);
Console.WriteLine($"person.Name: {person.Name}");
Console.WriteLine($"person.Age: {person.Age}");

// 方法回傳多值
var coords = GetCoordinates("Taipei 101");
Console.WriteLine($"\n緯度: {coords.Lat}, 經度: {coords.Lon}");

// 解構 Tuple
var personTuple = ("Alice", 30);
(string name, int age) = personTuple;
Console.WriteLine($"\n解構結果: name={name}, age={age}");

// 使用 var 解構
var (lat, lon) = GetCoordinates("Taipei 101");
Console.WriteLine($"var 解構: lat={lat}, lon={lon}");

// 變數捨棄 (Discard)
var (latitude, _) = GetCoordinates("Taipei 101");  // 只要緯度
Console.WriteLine($"\n只取緯度: {latitude}");

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

// 輔助方法
static (double Lat, double Lon) GetCoordinates(string address)
{
    // 假裝查詢了地圖 API
    return (25.0330, 121.5654);
}

// 支援解構的自訂型別
public class Person
{
    public string Name { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public string Email { get; set; } = "";

    // 2 參數的解構方法
    public void Deconstruct(out string name, out DateTime birthDate)
    {
        name = Name;
        birthDate = BirthDate;
    }

    // 3 參數的解構方法
    public void Deconstruct(out string name, out DateTime birthDate, out string email)
    {
        name = Name;
        birthDate = BirthDate;
        email = Email;
    }
}
