namespace MicrogradDotNet.Tests;

public class LayerTest
{
    [Fact]
    public void Constructor_InitializesNeuronsAndParameters()
    {
        const int numberOfInputs = 3;
        const int numberOfOutputs = 3;
        var layer = new Layer(numberOfInputs, numberOfOutputs);
        var parameters = layer.Parameters.ToList();
        
        Assert.Equal(numberOfOutputs, layer.Neurons.Count);
        Assert.Equal((numberOfInputs + 1) * numberOfOutputs, parameters.Count);
        foreach (var n in layer.Neurons)
        {
            foreach (var p in n.Parameters)
            {
                Assert.Contains(p, parameters);
            }
        }
    }
}