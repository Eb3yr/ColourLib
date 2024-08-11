using ColourLib;
using System.Numerics;

HSLColor hsl = new(0.6f, 0.8f, 0.5f);
Console.WriteLine($"hsv24 = {hsl.GetHSL24()}");
Color rgb = (Color)hsl;
Console.WriteLine((Color32)rgb);
HSLColor hslNew = (HSLColor)rgb;
Console.WriteLine($"new hsv24 = {hslNew.GetHSL24()}");
Console.ReadLine();

// TODO: Reach a consensus on how to handle alpha channels with +-*/. Should it act as every other channel, or take the left argument's alpha channel? 