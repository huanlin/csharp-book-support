// 示範 defensive copy：readonly 欄位與 in 參數

var holder = new Holder(new Measurement(10));
holder.Exec();
holder.VerifyDefensiveCopy();

Console.WriteLine();

var measurement = new Measurement(20);
MeasurementDemo.Exec(in measurement);
MeasurementDemo.VerifyDefensiveCopy(in measurement);
Console.WriteLine($"原始 measurement.Value: {measurement.Value}");

public struct Measurement
{
    private int _value;

    // 未標示 readonly 的 getter
    public int Value
    {
        get { return _value; }
    }

    public Measurement(int value)
    {
        _value = value;
    }

    public int IncrementAndGet()
    {
        _value++;
        return _value;
    }
}

public class Holder
{
    public readonly Measurement Data;

    public Holder(Measurement data)
    {
        Data = data;
    }

    public void Exec()
    {
        var value = Data.Value;
        Console.WriteLine($"Data.Value: {value}");
    }

    public void VerifyDefensiveCopy()
    {
        Console.WriteLine($"第一次 Data.IncrementAndGet(): {Data.IncrementAndGet()}");
        Console.WriteLine($"第二次 Data.IncrementAndGet(): {Data.IncrementAndGet()}");
        Console.WriteLine($"最後 Data.Value: {Data.Value}");
    }
}

static class MeasurementDemo
{
    public static void Exec(in Measurement data)
    {
        Console.WriteLine($"data.Value: {data.Value}");
    }

    public static void VerifyDefensiveCopy(in Measurement data)
    {
        Console.WriteLine($"第一次 data.IncrementAndGet(): {data.IncrementAndGet()}");
        Console.WriteLine($"第二次 data.IncrementAndGet(): {data.IncrementAndGet()}");
        Console.WriteLine($"離開方法前 data.Value: {data.Value}");
    }
}
