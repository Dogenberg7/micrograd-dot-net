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
}