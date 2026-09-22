namespace MicrogradDotNet.Tests;

public class NeuronTest
{
    [Fact]
    public void Constructor_InitializesWeightsAndParameters()
    {
        const int numberOfInputs = 3;
        var n = new Neuron(numberOfInputs);

        var parameters = n.Parameters.ToList();
        
        Assert.Equal(numberOfInputs, n.Weights.Count);
        Assert.NotNull(n.Bias);
        Assert.Equal(numberOfInputs + 1, parameters.Count);
        Assert.Contains(n.Bias, parameters);
        foreach (var w in n.Weights)
        {
            Assert.Contains(w, parameters);
        }
    }

    [Fact]
    public void Forward_LinearActivation()
    {
        var neuron = new Neuron(2, nonLinear: false);
        neuron.Weights[0].Data = 2.0;
        neuron.Weights[1].Data = -3.0;
        neuron.Bias.Data = 1.0;

        Value[] inputs = [new Value(1.5), new Value(2.0)];
        const double expected = -2.0;
        
        var output = neuron.Forward(inputs).Data;
        
        Assert.Equal(expected, output, precision: 5);
    }
}