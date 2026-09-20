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

    [Fact]
    public void Add_NewValue()
    {
        const double v1Data = 4.0;
        const double v2Data = 5.0;
        const double expectedData = v1Data + v2Data;
        
        var v1 = new Value(v1Data);
        var v2 = new Value(v2Data);

        var v = v1 + v2;
        
        Assert.NotEqual(v1, v);
        Assert.NotEqual(v2, v);
        Assert.Equal(expectedData, v.Data);
    }

    [Fact]
    public void Constructor_ChildrenTree()
    {
        const double vData = 4.0;
        const string op = "operation";
        
        var v = new Value(vData);

        var v1 = new Value(v.Data, [v], op);
        
        Assert.Equal(v, v1.Children[0]);
        Assert.Equal(op, v1.Op);
    }
}
