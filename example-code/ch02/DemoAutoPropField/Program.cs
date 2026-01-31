// 示範 C# 14 field 關鍵字
using System.Runtime.CompilerServices;

var person = new Person();
person.Name = "李四";
person.Age = 30;
person.Email = "test@example.com";
Console.WriteLine($"人員: Name={person.Name}, Age={person.Age}, ID={person.Id}");
Console.WriteLine($"Email: {person.Email}");

// 測試驗證邏輯
try
{
    person.Age = -5;  // 應該拋出例外
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\n驗證失敗: {ex.Message}");
}

// 測試屬性變更通知
Console.WriteLine("\n測試屬性變更通知:");
person.Email = "new@example.com";  // 應該觸發 OnPropertyChanged
person.Email = "new@example.com";  // 相同值，不會觸發

public class Person
{
    // 唯讀屬性：初始化時設定
    public string Id { get; } = Guid.NewGuid().ToString()[..8];

    // 帶驗證的屬性：使用 field 關鍵字
    public string Name
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    } = "";

    // 帶驗證的屬性：setter 中使用 field
    public int Age
    {
        get;
        set
        {
            if (value < 0)
                throw new ArgumentException("年齡不可為負數！");
            field = value;
        }
    }

    // 帶通知的屬性（常見於 MVVM）
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
        Console.WriteLine($"  屬性 {name} 已變更為: {Email}");
    }
}
