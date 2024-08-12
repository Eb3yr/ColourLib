using ColourLib;
using System.Numerics;

HSL24Color one = new(0, 0, 0);
HSL24Color two = new(10, 10, 10);
HSL24Color three = one.Lerp(two, 0.5f);
Console.WriteLine(three);
Console.ReadLine();

// TODO: Reach a consensus on how to handle alpha channels with +-*/. Should it act as every other channel, or take the left argument's alpha channel?
// TODO: More conversions for the byte-based structs. Having to cast to float and then back is kind of rubbish. 
// TODO: Gamma and sRGB conversions for Color and Color32, as well as some static colours, to achieve proper feature parity with Unity
// Should the Grayscale property be added for HSV and HSL?
// Should an additional "Monochrome" method be added with several different options for determining how the colours are converted to grey?