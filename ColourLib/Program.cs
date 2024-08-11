using ColourLib;
using System.Numerics;

HSLColor hsl = new(0.6f, 0.8f, 0.5f);
Console.WriteLine($"hsv24 = {hsl.GetHSL24()}");
Color rgb = (Color)hsl;
Console.WriteLine((Color32)rgb);
HSLColor hslNew = (HSLColor)rgb;
Console.WriteLine($"new hsv24 = {hslNew.GetHSL24()}");
Console.ReadLine();

// TODO: Decide between returning a new struct for operators or modifying one passed as an argument. I expect it's insignificant, but it's worth benchmarking, since this differs between Color, HSV and HSL and it'd be nice to keep it consistent.