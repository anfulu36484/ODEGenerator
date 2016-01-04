# ODEGenerator

## What is it?

Library for easy generation and calculation a system of ordinary differential equations describing the kinetics of simple reactions and polymerization reactions.

### Example of a simple reaction

ODEs odEs = new ODEs(); //system of differential equations

Substance A = new Substance("A", 0.1); //starting (initial) concentration of A - 0.1
Substance B = new Substance("B", 0.3); //starting (initial) concentration of A - 0.3
Substance C = new Substance("C", 0); //starting (initial) concentration of A - 0

Constant k = new Constant("k", 1.2); //reaction rate constant

odEs.Add(A, B, k, C); //adding to the reaction system (A+B->C)
odEs.PrintResult(new MathVisitor());


###  Example of a polymerization reaction

[Reversible living polymerization]
(https://en.wikipedia.org/wiki/Living_polymerization#Living_ring-opening_metathesis_polymerization)
R0+M -> R1
Ri + M -> Ri+1   i ∈ [1;L-1]
Ri+1 ->Ri+M

M -  monomer concentration
R - concentration of the polymer chain, i - number of monomeric units in the chain


ODEs odEs = new ODEs();

Substance M = new Substance("M", 10);

GroupOfSubstances R = new GroupOfSubstances("R");

int L = 2000;

for (int i = 1; i <= L; i++)
{
    R.CreateSubstance(0.0, i);
}
R[1].Value = 0.01;


Constant kp = new Constant("kp", 2);
Constant kd = new Constant("kd", 1.3);


odEs.Add(R[1], M, kp, R[2]);

for (int i = 2; i < L - 1; i++)
{
    odEs.Add(R[i], M, kp, R[i + 1]);
    odEs.Add(R[i + 1], kd, R[i], M);
}

odEs.Add(R[L - 1], M, kp, R[L]);

odEs.PrintResult(new MathVisitor());

MatlabCodeGenerator matlabCodeGenerator = new MatlabCodeGenerator(odEs,
Enumerable.Range(0, 100).Select(n => (double)n / 10f).ToArray(), R);
matlabCodeGenerator.Generate(Environment.CurrentDirectory + @"\test\test3");