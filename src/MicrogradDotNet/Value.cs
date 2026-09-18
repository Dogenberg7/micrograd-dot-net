namespace MicrogradDotNet;

public class Value
{
    public double Data { get; set; }
    public double Grad { get; set; }

    public Value(double data)
    {
        Data = data;
        Grad = 0.0;
    }

    public override string ToString()
        => $"Value(data={Data})";
}
