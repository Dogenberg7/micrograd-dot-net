using System.Security.AccessControl;

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
    
    public static Value operator +(Value left, Value right)
    {
        var o = new Value(left.Data + right.Data);
        return o;
    }

    public override string ToString()
        => $"Value(data={Data})";
}
