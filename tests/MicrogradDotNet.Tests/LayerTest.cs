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

    [Fact]
    public void Forward_LinearActivation()
    {
        const int numberOfInputs = 3;
        const int numberOfOutputs = 3;
        var layer = new Layer(numberOfInputs, numberOfOutputs, nonLinear: false);

        int i = 1;
        foreach (var n in layer.Neurons)
        {
            n.Bias.Data = i * 10;
            int j = 1;
            foreach (var w in n.Weights)
            {
                w.Data = j * 0.1;
                j++;
            }
            i++;
        }

        Value[] inputs = [new Value(1.0), new Value(2.0), new Value(3.0)];
        var outputs = layer.Forward(inputs);
        
        Assert.Equal(numberOfOutputs, outputs.Count);
        Assert.Equal(11.4, outputs[0].Data);
        Assert.Equal(21.4, outputs[1].Data);
        Assert.Equal(31.4, outputs[2].Data);
    }
}