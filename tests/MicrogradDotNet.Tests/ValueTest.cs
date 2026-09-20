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
    
    [Fact]
    public void Add_ChildrenTree()
    {
        const double v1Data = 4.0;
        const double v2Data = 5.0;
        
        var v1 = new Value(v1Data);
        var v2 = new Value(v2Data);
        
        const string expectedOp = "+";

        var v = v1 + v2;
        
        Assert.Equal(expectedOp, v.Op);
        Assert.Equal(v1, v.Children[0]);
        Assert.Equal(v2, v.Children[1]);
    }
    
    [Fact]
    public void Add_ValueAndNotValue()
    {
        const double vData = 5.0;
        const double d = 4.0;
        var v = new Value(vData);

        const double expectedData = 9.0;

        var v1 = v + d;
        var v2 = d + v;
        
        Assert.Equal(expectedData, v1.Data);
        Assert.Equal(expectedData, v2.Data);
    }
    
    [Fact]
    public void Multiply_NewValue()
    {
        const double v1Data = 4.0;
        const double v2Data = 5.0;
        
        var v1 = new Value(v1Data);
        var v2 = new Value(v2Data);
        
        const double expectedData = v1Data * v2Data;
        const string expectedOp = "*";

        var v = v1 * v2;
        
        Assert.NotEqual(v1, v);
        Assert.NotEqual(v2, v);
        Assert.Equal(expectedData, v.Data);
        Assert.Equal(expectedOp, v.Op);
        Assert.Equal(v1, v.Children[0]);
        Assert.Equal(v2, v.Children[1]);
    }
    
    [Fact]
    public void Multiply_ValueAndNotValue()
    {
        const double vData = 5.0;
        const double d = 4.0;
        var v = new Value(vData);

        const double expectedData = 20.0;

        var v1 = v * d;
        var v2 = d * v;
        
        Assert.Equal(expectedData, v1.Data);
        Assert.Equal(expectedData, v2.Data);
    }
    
    [Fact]
    public void Pow_Operation()
    {
        const double vData = 5.0;
        const double exp = 2.0;
        var v = new Value(vData);

        var expectedData = Math.Pow(vData, exp);
        var expectedOp = $"^{exp}";
        
        var v1 = v.Pow(exp);
        
        Assert.Equal(expectedData, v1.Data);
        Assert.Equal(expectedOp, v1.Op);
        Assert.Equal(v, v1.Children[0]);
    }

    [Fact]
    public void Negate_Operation()
    {
        const double vData = 5.0;
        var v = new Value(vData);
        
        const double expectedData = -vData;

        var v1 = -v;
        
        Assert.Equal(expectedData, v1.Data);
        Assert.Equal(v, v1.Children[0]);
        Assert.Equal(-1.0, v1.Children[1].Data);
    }
    
    [Fact]
    public void Subtract_Operation()
    {
        const double v1Data = 5.0;
        const double v2Data = 7.0;
        var v1 = new Value(v1Data);
        var v2 = new Value(v2Data);
        
        const double expectedData = -2.0;

        var v = v1 - v2;
        
        Assert.Equal(expectedData, v.Data);
        Assert.Equal(v1, v.Children[0]);
        Assert.Equal(-v2.Data, v.Children[1].Data);
    }
    
    [Fact]
    public void Subtract_ValueAndNotValue()
    {
        const double vData = 5.0;
        const double d = 4.0;
        var v = new Value(vData);

        const double expectedData1 = 1.0;
        const double expectedData2 = -1.0;

        var v1 = v - d;
        var v2 = d - v;
        
        Assert.Equal(expectedData1, v1.Data);
        Assert.Equal(expectedData2, v2.Data);
    }
    
    [Fact]
    public void Divide_Operation()
    {
        const double v1Data = 5.0;
        const double v2Data = 2.0;
        var v1 = new Value(v1Data);
        var v2 = new Value(v2Data);
        
        const double expectedData = v1Data / v2Data;

        var v = v1 / v2;
        
        Assert.Equal(expectedData, v.Data);
        Assert.Equal(v1, v.Children[0]);
        Assert.Equal(v2.Pow(-1.0).Data, v.Children[1].Data);
    }

    [Fact]
    public void Divide_ValueAndNotValue()
    {
        const double vData = 5.0;
        const double d = 2.0;
        var v = new Value(vData);

        const double expectedData1 = vData / d;
        const double expectedData2 = d / vData;

        var v1 = v / d;
        var v2 = d / v;
        
        Assert.Equal(expectedData1, v1.Data);
        Assert.Equal(expectedData2, v2.Data);
    }
    
    [Fact]
    public void Exp_Operation()
    {
        const double vData = 5.0;
        var v = new Value(vData);
        
        var expectedData = Math.Exp(vData);
        const string expectedOp = "exp";

        var v1 = v.Exp();
        
        Assert.Equal(expectedData, v1.Data);
        Assert.Equal(expectedOp, v1.Op);
        Assert.Equal(v, v1.Children[0]);
    }
    
    [Fact]
    public void Tanh_Operation()
    {
        const double vData = 5.0;
        var v = new Value(vData);
        
        var expectedData = Math.Tanh(vData);
        const string expectedOp = "tanh";

        var v1 = v.Tanh();
        
        Assert.Equal(expectedData, v1.Data);
        Assert.Equal(expectedOp, v1.Op);
        Assert.Equal(v, v1.Children[0]);
    }
    
    [Fact]
    public void Relu_Operation()
    {
        const double v1Data = 5.0;
        const double v2Data = -2.0;
        var v1 = new Value(v1Data);
        var v2 = new Value(v2Data);

        var expectedData1 = Math.Max(0.0, v1Data);
        var expectedData2 = Math.Max(0.0, v2Data);
        const string expectedOp = "ReLU";

        var v3 = v1.Relu();
        var v4 = v2.Relu();
        
        Assert.Equal(expectedData1, v3.Data);
        Assert.Equal(expectedData2, v4.Data);
        Assert.Equal(expectedOp, v3.Op);
        Assert.Equal(expectedOp, v4.Op);
        Assert.Equal(v1, v3.Children[0]);
        Assert.Equal(v2, v4.Children[0]);
    }

    [Fact]
    public void Addition_Backward_AccumulatesGradients()
    {
        const double v1Data = 3.0;
        const double v2Data = 2.0;
        const double vGrad = 5.0;
        
        var v1 = new Value(v1Data);
        var v2 = new Value(v2Data);
        var v = v1 + v2;
        
        v.Grad = vGrad;
        v.BackwardAction();
        
        Assert.Equal(vGrad, v1.Grad);
        Assert.Equal(vGrad, v2.Grad);
    }
    
    [Fact]
    public void Addition_Backward_AccumulatesWhenSameNodeIsReused()
    {
        const double v1Data = 3.0;
        const double vGrad = 5.0;
        var v1 = new Value(v1Data);
        var v = v1 + v1;

        v.Grad = vGrad;
        v.BackwardAction();
        
        Assert.Equal(2*vGrad, v1.Grad);
    }
    
    [Fact]
    public void Multiplication_Backward_AccumulatesGradients()
    {
        const double v1Data = 3.0;
        const double v2Data = 2.0;
        const double vGrad = 5.0;
        
        var v1 = new Value(v1Data);
        var v2 = new Value(v2Data);
        var v = v1 * v2;
        
        const double expectedGrad1 = v2Data * vGrad;
        const double expectedGrad2 = v1Data * vGrad;
        
        v.Grad = vGrad;
        v.BackwardAction();
        
        Assert.Equal(expectedGrad1, v1.Grad);
        Assert.Equal(expectedGrad2, v2.Grad);
    }
    
    [Fact]
    public void Multiplication_Backward_AccumulatesWhenSameNodeIsReused()
    {
        const double v1Data = 3.0;
        const double vGrad = 5.0;
        var v1 = new Value(v1Data);
        var v = v1 * v1;
        
        const double expectedGrad1 = 2 * v1Data * vGrad;

        v.Grad = vGrad;
        v.BackwardAction();
        
        Assert.Equal(expectedGrad1, v1.Grad);
    }
    
    [Fact]
    public void Pow_Backward_AccumulatesGradients()
    {
        const double v1Data = 3.0;
        const double exponent = 4.0;
        const double vGrad = 5.0;
        
        var v1 = new Value(v1Data);
        var v = v1.Pow(exponent);
        
        var expectedGrad1 = exponent * Math.Pow(v1Data, exponent - 1) * vGrad;
        
        v.Grad = vGrad;
        v.BackwardAction();
        
        Assert.Equal(expectedGrad1, v1.Grad);
    }
    
    [Fact]
    public void Exp_Backward_AccumulatesGradients()
    {
        const double v1Data = 3.0;
        const double vGrad = 5.0;
        
        var v1 = new Value(v1Data);
        var v = v1.Exp();
        
        var expectedGrad1 = Math.Exp(v1Data) * vGrad;
        
        v.Grad = vGrad;
        v.BackwardAction();
        
        Assert.Equal(expectedGrad1, v1.Grad);
    }
}
