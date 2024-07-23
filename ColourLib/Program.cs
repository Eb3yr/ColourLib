using ColourLib;
using System.Numerics;
Color c1 = new(0.9f, 0.9f, 0.9f);
Color c2 = new(1f, 1f, 1f);
Color c3 = Color.LerpUnclamped(c1, c2, -0.5f);
Console.WriteLine(c3.ToString());
Console.ReadLine();

