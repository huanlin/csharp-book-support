// Demo: defensive copies with readonly fields and in parameters

var holder = new Holder(new Measurement(10));
holder.Exec();
holder.VerifyDefensiveCopy();

Console.WriteLine();

var measurement = new Measurement(20);
MeasurementDemo.Exec(in measurement);
MeasurementDemo.VerifyDefensiveCopy(in measurement);
Console.WriteLine($"Original measurement.Value: {measurement.Value}");

public struct Measurement
{
    private int _value;

    // Getter not marked readonly
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
        Console.WriteLine($"First Data.IncrementAndGet(): {Data.IncrementAndGet()}");
        Console.WriteLine($"Second Data.IncrementAndGet(): {Data.IncrementAndGet()}");
        Console.WriteLine($"Final Data.Value: {Data.Value}");
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
        Console.WriteLine($"First data.IncrementAndGet(): {data.IncrementAndGet()}");
        Console.WriteLine($"Second data.IncrementAndGet(): {data.IncrementAndGet()}");
        Console.WriteLine($"data.Value before returning: {data.Value}");
    }
}
