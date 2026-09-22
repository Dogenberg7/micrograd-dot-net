namespace MicrogradDotNet;

public class Layer
{
    public IReadOnlyList<Neuron> Neurons { get; }
    public IEnumerable<Value> Parameters { get; }

    public Layer(int nin, int nout, bool nonLinear = true, Random? rng = null)
    {
        var neurons = new Neuron[nout];
        var parameters = Array.Empty<Value>();
        for (int i = 0; i < nout; i++)
        {
            neurons[i] = new Neuron(nin, nonLinear, rng);
            parameters = parameters.Concat(neurons[i].Parameters).ToArray();
        }
        Neurons = neurons;
        Parameters = parameters;
    }

    public IReadOnlyList<Value> Forward(IReadOnlyList<Value> inputs)
    {
        var outputs = new Value[Neurons.Count];
        for (int i = 0; i < Neurons.Count; i++)
        {
            outputs[i] = Neurons[i].Forward(inputs);
        }
        return outputs;
    }

    public override string ToString()
    {
        return $"Layer({string.Join(",", Neurons.Select(n => n.ToString()))})";
    }
}