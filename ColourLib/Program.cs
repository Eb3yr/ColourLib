using ColourLib;
using System.Numerics;

HSLColor hsl = new(0.6f, 0.8f, 0.5f);
Console.WriteLine($"hsl24 = {hsl.GetHSL24()}");
Color rgb = (Color)hsl;
Console.WriteLine((Color32)rgb);
Console.ReadLine();

