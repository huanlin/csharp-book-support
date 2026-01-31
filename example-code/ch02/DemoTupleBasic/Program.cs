// 示範 Tuple 基礎

// Tuple 語法定義
(string Name, int Age) person = ("Bob", 23);
Console.WriteLine($"person.Name: {person.Name}");
Console.WriteLine($"person.Age: {person.Age}");

// 方法回傳多值
var coords = GetCoordinates("Taipei 101");
Console.WriteLine($"\n緯度: {coords.Lat}, 經度: {coords.Lon}");

static (double Lat, double Lon) GetCoordinates(string address)
{
    // 假裝查詢了地圖 API
    return (25.0330, 121.5654);
}
