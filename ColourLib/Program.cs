using ColourLib;
using System.Numerics;
Color c1 = new(1f, 0f, 0f);
Color c2 = new(0f, 1f, 0f);
Color c3 = Color.Lerp(c1, c2, 0.5f);
Console.WriteLine(c3.ToString());
Console.ReadLine();