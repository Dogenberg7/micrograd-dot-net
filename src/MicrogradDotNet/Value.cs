using System.Security.AccessControl;

namespace MicrogradDotNet;

public class Value
{
    public double Data { get; set; }
    public double Grad { get; set; }
    public Value[] Children { get; }
    public string Op { get; }

    public Value(double data, Value[]? children = null, string op = "")
    {
        Data = data;
        Grad = 0.0;
        Children = children ?? [];
        Op = op;
    }
    
    public static Value operator +(Value left, Value right)
    {
        var o = new Value(left.Data + right.Data, [left, right], "+");
        return o;
    }

    public override string ToString()
        => $"Value(data={Data})";
}
