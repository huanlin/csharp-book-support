// デモ: readonly フィールドと in パラメーターで発生する defensive copy

var holder = new Holder(new Measurement(10));
holder.Exec();
holder.VerifyDefensiveCopy();

Console.WriteLine();

var measurement = new Measurement(20);
MeasurementDemo.Exec(in measurement);
MeasurementDemo.VerifyDefensiveCopy(in measurement);
Console.WriteLine($"元の measurement.Value: {measurement.Value}");

public struct Measurement
{
    private int _value;

    // readonly が付いていない getter
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
        Console.WriteLine($"1 回目の Data.IncrementAndGet(): {Data.IncrementAndGet()}");
        Console.WriteLine($"2 回目の Data.IncrementAndGet(): {Data.IncrementAndGet()}");
        Console.WriteLine($"最後の Data.Value: {Data.Value}");
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
        Console.WriteLine($"1 回目の data.IncrementAndGet(): {data.IncrementAndGet()}");
        Console.WriteLine($"2 回目の data.IncrementAndGet(): {data.IncrementAndGet()}");
        Console.WriteLine($"戻る直前の data.Value: {data.Value}");
    }
}
