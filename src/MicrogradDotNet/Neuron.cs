namespace MicrogradDotNet;

public class Neuron
{
    public IReadOnlyList<Value> Weights { get; }
    public Value Bias { get; }
    public bool NonLinear { get; }

    public IEnumerable<Value> Parameters => Weights.Append(Bias);

    public Neuron(int nin, bool nonLinear = true, Random? rng = null)
    {
        var random = rng ?? Random.Shared;
        NonLinear = nonLinear;

        var weights = new Value[nin];
        for (int i = 0; i < nin; i++)
        {
            double w = random.NextDouble() * 2.0 - 1.0;
            weights[i] = new Value(w);
        }
        
        Weights = weights;
        Bias = new Value(0.0);
    }
}