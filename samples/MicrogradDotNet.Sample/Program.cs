using MicrogradDotNet;

// Inputs
Value[][] xs = [
    [new Value(2.0), new Value(3.0), new Value(-1.0)],
    [new Value(3.0), new Value(-1.0), new Value(0.5)],
    [new Value(0.5), new Value(1.0), new Value(1.0)],
    [new Value(1.0), new Value(1.0), new Value(-1.0)]
];

// Targets
double[] ys = [1.0, -1.0, -1.0, 1.0];

// 3 inputs, 2 layers with 4 neurons each, 1 output
var model = new Mlp(3, [4, 4, 1]);

Console.WriteLine($"Total parameter count: {model.Parameters.Count()}");

// Training Loop
const int epochs = 20;
const double learningRate = 0.05;

for (int step = 0; step < epochs; step++)
{
    var ypreds = new List<Value>();
    foreach (var x in xs)
    {
        ypreds.Add(model.Forward(x)[0]);
    }
    
    Value loss = new Value(0.0);
    for (int i = 0; i < ys.Length; i++)
    {
        var diff = ypreds[i] - ys[i];
        loss = loss + (diff * diff);
    }
    
    model.ZeroGrad();
    
    loss.Backward();
    
    foreach (var p in model.Parameters)
    {
        p.Data -= learningRate * p.Grad;
    }

    Console.WriteLine($"Step {step + 1,2} | Loss: {loss.Data:F6} | Predictions: {string.Join(",", ypreds.Select(y => y.Data))}");
}