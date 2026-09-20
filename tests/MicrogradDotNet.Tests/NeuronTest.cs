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
}