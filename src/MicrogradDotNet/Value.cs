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

    public static Value operator +(Value left, double right)
        => left + new Value(right);
    
    public static Value operator +(double left, Value right)
        => new Value(left) + right;

    public static Value operator *(Value left, Value right)
    {
        var o = new Value(left.Data * right.Data, [left, right], "*");
        return o;
    }
    
    public static Value operator *(Value left, double right)
        => left * new Value(right);
    
    public static Value operator *(double left, Value right)
        => new Value(left) * right;

    public Value Pow(double exponent)
    {
        var o = new Value(Math.Pow(Data, exponent), [this], $"^{exponent}");
        return o;
    }

    public static Value operator -(Value operand)
        => operand * -1.0;

    public static Value operator -(Value left, Value right)
        => left + (-right);
    
    public static Value operator -(Value left, double right)
        => left - new Value(right);
    
    public static Value operator -(double left, Value right)
        => new Value(left) - right;

    public static Value operator /(Value left, Value right)
        => left * right.Pow(-1.0);
    
    public static Value operator /(Value left, double right)
        => left / new Value(right);
    
    public static Value operator /(double left, Value right)
        => new Value(left) / right;

    public Value Exp()
    {
        var o = new Value(Math.Exp(Data), [this], "exp");
        return o;
    }

    public Value Tanh()
    {
        var t = Math.Tanh(Data);
        var o = new Value(t, [this], "tanh");
        return o;
    }

    public override string ToString()
        => $"Value(data={Data})";
}
