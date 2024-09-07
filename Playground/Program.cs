using ColourLib;
using System.Numerics;

static void OpenHexInBrowser(string hex)
{
	string link = "https://www.atatus.com/tools/color-code-viewer#" + hex;
	System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(link) { UseShellExecute = true });
}

//HSLColor col = new(0.65137756f, 0.9970388f, 0.64967453f);
//Console.WriteLine(((Color)col).ToHex());

//Console.WriteLine((HSLColor)Color.FromHex("B7BEBF"));
Console.WriteLine("Body: " + ((HslColor)Color.FromHex("BCE2FD")).ToString());
Console.WriteLine("Eyes: " + ((HslColor)Color.FromHex("B104AA")).ToString());
Console.ReadLine();
// TODO: Reach a consensus on how to handle alpha channels with +-*/. Should it act as every other channel, or take the left argument's alpha channel?
// TODO: More conversions for the byte-based structs. Having to cast to float and then back is kind of rubbish.
// TODO: Fully implement Color32. I don't know how I missed this.
// TODO: Gamma and sRGB conversions for Color and Color32, as well as some static colours, to achieve proper feature parity with Unity
// Should the Grayscale property be added for HSV and HSL?
// Should an additional "Monochrome" method be added with several different options for determining how the colours are converted to grey?

// TODO: Full testing on arithmetic of all structs
// TODO: ToHex and FromHex for non-RGB structs


// BUG: See line 216 of monogame's HslColor: https://github.com/craftworkgames/MonoGame.Extended/blob/develop/source/MonoGame.Extended/HslColor.cs
// My implementation for HSL and HSV don't wrap around the hue in certain circumstances where it should be done. If the left HsvColor is greater than right, then a wrap around should occur. (Verify this, it is Very Late At Night).
// I have yet to fix this for 32 bit versions