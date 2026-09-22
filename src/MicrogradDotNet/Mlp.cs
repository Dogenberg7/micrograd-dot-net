namespace MicrogradDotNet;

public class Mlp
{
    public IReadOnlyList<Layer> Layers { get; }
    public IReadOnlyList<Value> Parameters { get; }

    public Mlp(int nin, int[] nouts, Random? rng = null)
    {
        int[] sz = nouts.Prepend(nin).ToArray();
        var layers = new Layer[nouts.Length];
        var parameters = Array.Empty<Value>();
        for (int i = 0; i < nouts.Length; i++)
        {
            layers[i] = new Layer(sz[i], sz[i+1], nonLinear: i!=nouts.Length-1, rng);
            parameters = parameters.Concat(layers[i].Parameters).ToArray();
        }
        Layers = layers;
        Parameters = parameters;
    }

    public IReadOnlyList<Value> Forward(IReadOnlyList<Value> inputs)
    {
        var x = inputs;
        foreach (Layer l in Layers)
        {
            x = l.Forward(x);
        }

        return x;
    }

    public override string ToString()
    {
        return $"MLP[{string.Join(",", Layers)}])";
    }
}