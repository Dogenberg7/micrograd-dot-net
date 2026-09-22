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
}