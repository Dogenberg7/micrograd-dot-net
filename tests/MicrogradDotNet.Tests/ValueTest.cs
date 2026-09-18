namespace MicrogradDotNet.Tests;

public class ValueTest
{
    [Fact]
    public void Constructor_SetsInitialDataGrad()
    {
        const double expectedData = 4.0;
        const double expectedGrad = 0.0;

        var v = new Value(expectedData);

        Assert.Equal(expectedData, v.Data);
        Assert.Equal(expectedGrad, v.Grad);
    }
}
