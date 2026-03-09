// デモ: なぜジェネリックが必要か

using System.Collections;

Console.WriteLine("=== なぜジェネリックが必要か ===\n");

// --------------------------------------------------------------
// 1. ArrayList の問題: 手動キャスト
// --------------------------------------------------------------
Console.WriteLine("1. ArrayList の問題: 手動キャスト");
Console.WriteLine(new string('-', 40));

ArrayList intList = new ArrayList();
intList.Add(100);
intList.Add(200);
intList.Add(300);

int i1 = (int)intList[0]!;
int i2 = (int)intList[1]!;

Console.WriteLine($"取り出した要素: {i1}, {i2}");

// DateTime dt = (DateTime)intList[1];  // コンパイルは通るが実行時例外

Console.WriteLine("問題: 毎回キャストが必要で、誤キャストをコンパイラが防げない。");

// --------------------------------------------------------------
// 2. 力技: 型ごとの独自コレクションクラス
// --------------------------------------------------------------
Console.WriteLine("\n2. 力技: 型ごとの独自コレクションクラス");
Console.WriteLine(new string('-', 40));

var myIntList = new IntList();
myIntList.Add(100);
myIntList.Add(200);

int num = myIntList[1];
Console.WriteLine($"IntList 取得要素: {num}");

var myStrList = new StringList();
myStrList.Add("John Doe");
myStrList.Add("Jane Smith");

string name = myStrList[1];
Console.WriteLine($"StringList 取得要素: {name}");

Console.WriteLine("問題: 型ごとに新しいクラスが必要になり、保守コストが高い。");

// --------------------------------------------------------------
// 3. ジェネリックの利点: List<T>
// --------------------------------------------------------------
Console.WriteLine("\n3. ジェネリックの利点: List<T>");
Console.WriteLine(new string('-', 40));

List<int> genericIntList = new List<int>();
genericIntList.Add(100);
genericIntList.Add(200);

int value = genericIntList[1];
Console.WriteLine($"List<int> 取得要素: {value}");

List<string> genericStrList = new List<string>();
genericStrList.Add("John Doe");
genericStrList.Add("Jane Smith");

string str = genericStrList[1];
Console.WriteLine($"List<string> 取得要素: {str}");

Console.WriteLine("利点: 手動キャスト不要、コンパイル時型安全、重複クラス不要。");

// --------------------------------------------------------------
// 4. 性能比較: Boxing / Unboxing
// --------------------------------------------------------------
Console.WriteLine("\n4. 性能比較: Boxing / Unboxing");
Console.WriteLine(new string('-', 40));

ArrayList arrayList = new ArrayList();
arrayList.Add(42);
int val1 = (int)arrayList[0]!;

List<int> genericList = new List<int>();
genericList.Add(42);
int val2 = genericList[0];

Console.WriteLine("ArrayList: int 追加時に boxing、取得時に unboxing が発生。");
Console.WriteLine("List<int>: boxing/unboxing がなく、性能面で有利。");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// ヘルパークラス（力技アプローチ）
// ============================================================

public class IntList
{
    private ArrayList _numbers = new ArrayList();

    public void Add(int value) => _numbers.Add(value);

    public int this[int index]
    {
        get => (int)_numbers[index]!;
        set => _numbers[index] = value;
    }
}

public class StringList
{
    private ArrayList _strings = new ArrayList();

    public void Add(string value) => _strings.Add(value);

    public string this[int index]
    {
        get => (string)_strings[index]!;
        set => _strings[index] = value;
    }
}
