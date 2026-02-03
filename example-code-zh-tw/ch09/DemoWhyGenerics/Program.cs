// 示範為什麼需要泛型（Why Generics）
using System.Collections;

Console.WriteLine("=== 為什麼需要泛型 ===\n");

// --------------------------------------------------------------
// 1. ArrayList 的問題：手動轉型
// --------------------------------------------------------------
Console.WriteLine("1. ArrayList 的問題：手動轉型");
Console.WriteLine(new string('-', 40));

ArrayList intList = new ArrayList();
intList.Add(100);
intList.Add(200);
intList.Add(300);

int i1 = (int)intList[0]!;  // 必須手動轉型
int i2 = (int)intList[1]!;

Console.WriteLine($"取出元素：{i1}, {i2}");

// 問題：錯誤的轉型在編譯時不會報錯
// DateTime dt = (DateTime)intList[1];  // 編譯 OK，執行時會 InvalidCastException！

Console.WriteLine("問題：每次存取都要手動轉型，且錯誤轉型編譯器不會警告");

// --------------------------------------------------------------
// 2. 土法煉鋼：自訂專屬集合類別
// --------------------------------------------------------------
Console.WriteLine("\n2. 土法煉鋼：自訂專屬集合類別");
Console.WriteLine(new string('-', 40));

var myIntList = new IntList();
myIntList.Add(100);
myIntList.Add(200);

int num = myIntList[1];  // 不需要手動轉型
Console.WriteLine($"IntList 取出元素：{num}");

var myStrList = new StringList();
myStrList.Add("王曉明");
myStrList.Add("李大同");

string name = myStrList[1];  // 不需要手動轉型
Console.WriteLine($"StringList 取出元素：{name}");

Console.WriteLine("問題：需要為每種型別寫一個類別，維護成本高");

// --------------------------------------------------------------
// 3. 泛型之美：List<T>
// --------------------------------------------------------------
Console.WriteLine("\n3. 泛型之美：List<T>");
Console.WriteLine(new string('-', 40));

List<int> genericIntList = new List<int>();
genericIntList.Add(100);
genericIntList.Add(200);

int value = genericIntList[1];  // 不需要轉型，型別安全
Console.WriteLine($"List<int> 取出元素：{value}");

List<string> genericStrList = new List<string>();
genericStrList.Add("王曉明");
genericStrList.Add("李大同");

string str = genericStrList[1];  // 不需要轉型，型別安全
Console.WriteLine($"List<string> 取出元素：{str}");

// genericIntList.Add("錯誤");  // 編譯錯誤！型別安全

Console.WriteLine("優點：不需要手動轉型、編譯時期型別安全、不用寫重複類別");

// --------------------------------------------------------------
// 4. 效能比較：Boxing/Unboxing
// --------------------------------------------------------------
Console.WriteLine("\n4. 效能比較：Boxing/Unboxing");
Console.WriteLine(new string('-', 40));

// ArrayList 會發生 boxing/unboxing
ArrayList arrayList = new ArrayList();
arrayList.Add(42);        // boxing：int → object
int val1 = (int)arrayList[0]!;  // unboxing：object → int

// List<int> 不會 boxing
List<int> genericList = new List<int>();
genericList.Add(42);        // 沒有 boxing
int val2 = genericList[0];  // 沒有 unboxing

Console.WriteLine("ArrayList：每次加入 int 都會 boxing，取出時 unboxing");
Console.WriteLine("List<int>：沒有 boxing/unboxing，效能更好");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助類別（土法煉鋼的結果）
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
