namespace MicrogradDotNet.Tests;

public class MlpTest
{
    [Fact]
    public void Constructor_InitializesLayersAndParameters()
    {
        const int numberOfInputs = 3;
        int[] numbersOfOutputs = [3, 3, 1];
        var mlp = new Mlp(numberOfInputs, numbersOfOutputs);
        var parameters = mlp.Parameters.ToList();
        
        Assert.Equal(numbersOfOutputs.Length, mlp.Layers.Count);
        for (int i = 0; i < numbersOfOutputs.Length; i++)
        {
            Assert.Equal(numbersOfOutputs[i], mlp.Layers[i].Neurons.Count);
            for (int j = 0; j < mlp.Layers[i].Neurons.Count; j++)
            {
                Assert.Equal((j - 1 < 0 ? numberOfInputs : numbersOfOutputs[j-1]), mlp.Layers[i].Neurons[j].Weights.Count);
            }

            foreach (var p in mlp.Layers[i].Parameters)
            {
                Assert.Contains(p, parameters);
            }
        }
    }

    [Fact]
    public void Forward_PropagationWorks()
    {
        const int numberOfInputs = 2;
        int[] numbersOfOutputs = [2, 1];
        Value[] inputs = [new Value(-120.0), new Value(2.0)];
        
        var mlp = new Mlp(numberOfInputs, numbersOfOutputs);

        foreach (var l in mlp.Layers)
        {
            foreach (var n in l.Neurons)
            {
                n.Bias.Data = 10.0;
                n.Weights[0].Data = 0.1;
                n.Weights[1].Data = 0.2;
            }
        }

        const double expected = 10.0;
        var res = mlp.Forward(inputs);
        
        Assert.Equal(expected, res[0].Data);

    }
}