# MicrogradDotNet

A lightweight, strongly typed scalar autograd engine and neural network library implemented in C# (.NET 8), inspired by Andrej Karpathy's [micrograd](https://github.com/karpathy/micrograd).

`MicrogradDotNet` implements reverse-mode automatic differentiation (backpropagation) over a dynamically constructed Directed Acyclic Graph (DAG) of scalar values, alongside a foundational Multi-Layer Perceptron (MLP) architecture built from scratch without external ML frameworks.

Developed with **Test-Driven Development (TDD)** using **xUnit**.

---

## Features

- **Scalar Autograd Engine (`Value`)**:
  - Dynamically builds computation graphs via operator overloading (`+`, `-`, `*`, `/`, unary `-`).
  - Automatic gradient computation via reverse topological sorting (`Backward()`).
  - Supports non-linear activation functions (`Tanh`, `ReLU`).
  - Native scalar interop (supports both `Value + double` and `double + Value`).
- **Neural Network Primitives**:
  - `Neuron`: Linear combination ($\sum w_i x_i + b$) with optional non-linear activation.
  - `Layer`: Collection of parallel neurons mapping an input space to an output vector.
  - `MLP`: Modular Multi-Layer Perceptron orchestrating sequential layers.
- **Pure .NET 10**: Zero external machine learning dependencies.

---

## Quickstart

### Scalar Autograd Example

```csharp
using MicrogradNet;

var a = new Value(-4.0);
var b = new Value(2.0);

var c = a + b;
var d = a * b + (b * b * b);
c = c + c + 1.0;
c = c + 1.0 + c + (-a);
d = d + d * 2.0 + (b + a).Tanh();
d = d + 3.0 * d + (b - a).Tanh();
var e = c - d;
var f = e * e;

f.Backward();

Console.WriteLine($"Output: {f.Data:F4}"); // Loss value
Console.WriteLine($"df/da:  {a.Grad:F4}"); // Gradient of f with respect to a
Console.WriteLine($"df/db:  {b.Grad:F4}"); // Gradient of f with respect to b
```
### Running tests
```sh
dotnet test
```
### Running the training demo
```sh
dotnet run --project samples/MicrogradDotNet.Sample
```
## License
This project is licensed under the MIT license.