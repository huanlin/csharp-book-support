// デモ: C# 14 field キーワード
using System.Runtime.CompilerServices;

var person = new Person();
person.Name = "Li Si";
person.Age = 30;
person.Email = "test@example.com";
Console.WriteLine($"Person: Name={person.Name}, Age={person.Age}, ID={person.Id}");
Console.WriteLine($"Email: {person.Email}");

// バリデーションの確認
try
{
    person.Age = -5;  // 例外が発生する想定
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\nバリデーション失敗: {ex.Message}");
}

// プロパティ変更通知の確認
Console.WriteLine("\nプロパティ変更通知のテスト:");
person.Email = "new@example.com";  // OnPropertyChanged が呼ばれる
person.Email = "new@example.com";  // 同じ値なので呼ばれない

public class Person
{
    // 読み取り専用: 初期化時に設定
    public string Id { get; } = Guid.NewGuid().ToString()[..8];

    // field を使った検証付きプロパティ
    public string Name
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    } = "";

    // field を使った setter 検証
    public int Age
    {
        get;
        set
        {
            if (value < 0)
                throw new ArgumentException("年齢は負の値にできません");
            field = value;
        }
    }

    // 変更通知付きプロパティ（MVVM でよく使う）
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
        Console.WriteLine($"  プロパティ {name} が変更されました: {Email}");
    }
}
