// デモ: Tuple の基本

// Tuple 構文定義
(string Name, int Age) person = ("Bob", 23);
Console.WriteLine($"person.Name: {person.Name}");
Console.WriteLine($"person.Age: {person.Age}");

// メソッドから複数値を返す
var coords = GetCoordinates("Taipei 101");
Console.WriteLine($"\n緯度: {coords.Lat}, 経度: {coords.Lon}");

static (double Lat, double Lon) GetCoordinates(string address)
{
    // 地図 API 呼び出しを想定
    return (25.0330, 121.5654);
}
