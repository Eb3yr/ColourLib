using ColourLib;
using System.Numerics;

HSL24Color one = new(0, 0, 0);
HSL24Color two = new(10, 10, 10);
HSL24Color three = one.Lerp(two, 0.5f);
Console.WriteLine(three);
Console.ReadLine();

// TODO: Reach a consensus on how to handle alpha channels with +-*/. Should it act as every other channel, or take the left argument's alpha channel?
// TODO: More conversions for the byte-based structs. Having to cast to float and then back would be rubbish.