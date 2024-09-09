using System.Drawing;
using System.Numerics;

namespace ColourLib
{
    public struct Color32 : IColorB<Color32>, IRgb<Color32>
    {
        private byte r;
        private byte g;
        private byte b;
        private byte a;
        public byte R
        {
            readonly get => r;
            set { r = value; }
        }
        public byte G
        {
            readonly get => g;
            set { g = value; }
        }
        public byte B
        {
            readonly get => b;
            set { b = value; }
        }
        public byte A
        {
            readonly get => a;
            set { a = value; }
        }
		public int R32
		{
			readonly get => r;
			set => r = (byte)Math.Clamp(value, 0, 255);
		}
		public int G32
		{
			readonly get => g;
			set => g = (byte)Math.Clamp(value, 0, 255);
		}
		public int B32
		{
			readonly get => b;
			set => b = (byte)Math.Clamp(value, 0, 255);
		}
		public int A32
		{
			readonly get => a;
			set => a = (byte)Math.Clamp(value, 0, 255);
		}
        public int this[int i]
        {
            readonly get => i switch
            {
                0 => r,
                1 => g,
                2 => b,
                3 => a,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch (i)
                {
                    case 0:
                        R32 = value; break;
                    case 1:
                        G32 = value; break;
                    case 2:
                        B32 = value; break;
                    case 3:
                        A32 = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
		public readonly Color32 Grayscale
		{
			get => new(
				(byte)Math.Round(0.299f * r, MidpointRounding.AwayFromZero),
				(byte)Math.Round(0.587f * g, MidpointRounding.AwayFromZero),
				(byte)Math.Round(0.114f * b, a, MidpointRounding.AwayFromZero));
		}
		public Color32(byte r, byte g, byte b, byte a = 255)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}
		public Color32(int r, int g, int b, int a = 255)
		{
			R32 = r;
			G32 = g;
			B32 = b;
			A32 = a;
		}
		public Color32(float r, float g, float b, float a = 1f)
		{
			R32 = (int)Math.Round(r * 255f, MidpointRounding.AwayFromZero);
			G32 = (int)Math.Round(g * 255f, MidpointRounding.AwayFromZero);
			B32 = (int)Math.Round(b * 255f, MidpointRounding.AwayFromZero);
			A32 = (int)Math.Round(a * 255f, MidpointRounding.AwayFromZero);
		}
		public Color32(Color32 color) : this(color.r, color.g, color.b, color.a) { }
		public Color32(Color32 color, byte a) : this(color.r, color.g, color.b, a) { }
		public Color32(Color32 color, int a) : this(color.r, color.g, color.b)
		{
			A32 = a;
		}
		public Color32(string hex)
		{
			if (hex[0] == '#')
				hex = hex.Substring(1);

			uint hexInt = uint.Parse(hex, System.Globalization.NumberStyles.HexNumber);
			switch (hex.Length)
			{
				case 3:
					R = (byte)(((hexInt & 0x00000F00) | (hexInt & 0x00000F00) << 4) >> 8);
					G = (byte)(((hexInt & 0x000000F0) | (hexInt & 0x000000F0) << 4) >> 4);
					B = (byte)((hexInt & 0x0000000F) | (hexInt & 0x0000000F) << 4);
					A = 255;
					break;

				case 4:
					R = (byte)(((hexInt & 0x0000F000) | (hexInt & 0x0000F000) << 4) >> 12);
					G = (byte)(((hexInt & 0x00000F00) | (hexInt & 0x00000F00) << 4) >> 8);
					B = (byte)(((hexInt & 0x000000F0) | (hexInt & 0x000000F0) << 4) >> 4);
					A = (byte)((hexInt & 0x0000000F) | (hexInt & 0x0000000F) << 4);
					break;

				case 6:
					R = (byte)((hexInt & 0x00FF0000) >> 16);
					G = (byte)((hexInt & 0x0000FF00) >> 8);
					B = (byte)(hexInt & 0x000000FF);
					A = 255;
					break;

				case 8:
					R = (byte)((hexInt & 0xFF000000) >> 24);
					G = (byte)((hexInt & 0x00FF0000) >> 16);
					B = (byte)((hexInt & 0x0000FF00) >> 8);
					A = (byte)(hexInt & 0x000000FF);
					break;

				default:
					throw new ArgumentException("Color constructor only accepts hexadecimal strings of length 3, 4, 6 or 8.");
			}
		}
		public Color32(string hex, byte a) : this(hex)
		{
			this.a = a;
		}
		public Color32(string hex, int a) : this(hex)
		{
			A32 = a;
		}
		public readonly int Max() => Max(false);
		public readonly int Min() => Min(false);
		public readonly int Max(bool compareAlpha) => compareAlpha ? Math.Max(r, Math.Max(g, Math.Max(b, a))) : Math.Max(r, Math.Max(g, b));
		public readonly int Min(bool compareAlpha) => compareAlpha ? Math.Min(r, Math.Min(g, Math.Min(b, a))) : Math.Min(r, Math.Min(g, b));
		public readonly string ToHex()
		{
			string[] strs = [r.ToString("X"), g.ToString("X"), b.ToString("X"), a.ToString("X")];
			for (int i = 0; i < 4; i++)
				if (strs[i].Length == 1)
					strs[i] = "0" + strs[i];
			if (a == 255)
				strs[3] = "";

			return strs[0] + strs[1] + strs[2] + strs[3];
		}
		public static string ToHex(Color32 color) => color.ToHex();
		public static Color32 FromHex(string hex) => new(hex);
		public readonly int ToArgb()
		{
			int argb = 0;
			argb += a << 24;
			argb += r << 16;
			argb += g << 8;
			argb += b;
			return argb;
		}
		public static int ToArgb(Color32 color) => color.ToArgb();
		public static Color32 FromArgb(int argb)
		{
			return new(
				argb & 0x000000FF,
				argb & 0x0000FF00,
				argb & 0x00FF0000,
				argb & 0xFF000000
			);
		}
		public readonly int ToAbgr()
		{
			int argb = 0;
			argb += a << 24;
			argb += b << 16;
			argb += g << 8;
			argb += r;
			return argb;
		}
		public static int ToAbgr(Color32 color) => color.ToAbgr();
		public static Color32 FromAbgr(int abgr)
		{
			return new(
				abgr & 0x00FF0000,
				abgr & 0x0000FF00,
				abgr & 0x000000FF,
				abgr & 0xFF000000
			);
		}
		public readonly bool Equals(Color32 color) => color.R == r && color.G == g && color.B == b && color.A == a;
		public override readonly bool Equals(object? color) => color is Color32 c && color is not null && Equals(c);
		public readonly Color32 Difference(Color32 color) => Difference(this, color);
		public static Color32 Difference(Color32 left, Color32 right)
		{
			right.R = (byte)Math.Abs(right.R - left.R);
			right.G = (byte)Math.Abs(right.G - left.G);
			right.B = (byte)Math.Abs(right.B - left.B);
			right.A = (byte)Math.Abs(right.A - left.A);
			return right;
		}
		public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"<{r},{g},{b},{a}>";
		public readonly Color32 Lerp(Color32 to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
		public static Color32 Lerp(Color32 from, Color32 to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
		public readonly Color32 LerpUnclamped(Color32 to, float val) => LerpUnclamped(this, to, val);
		public static Color32 LerpUnclamped(Color32 from, Color32 to, float val)
		{
			from.R32 = (int)Math.Round((from.r * (1.0f - val)) + (to.r * val), MidpointRounding.AwayFromZero);
			from.G32 = (int)Math.Round((from.g * (1.0f - val)) + (to.g * val), MidpointRounding.AwayFromZero);
			from.B32 = (int)Math.Round((from.b * (1.0f - val)) + (to.b * val), MidpointRounding.AwayFromZero);
			from.A32 = (int)Math.Round((from.a * (1.0f - val)) + (to.a * val), MidpointRounding.AwayFromZero);
			return from;
		}
		public static bool InverseLerp(Vector4 left, Vector4 right, Vector4 val, out float lerpVal)
		{
			lerpVal = 0f;
			Vector4 ratioVec = (val - right) / (left - val);
			float? previous = null;
			for (int i = 0; i < 4; i++)
			{
				if (float.IsNaN(ratioVec[i]))
					continue;

				if (previous is null)
				{
					previous = ratioVec[i];
					lerpVal = (val[i] - left[i]) / (right[i] - left[i]);
				}
				else if (ratioVec[i] != previous)
				{
					lerpVal = 0f;
					return false;
				}
			}
			if (float.IsInfinity(lerpVal))
			{
				lerpVal = 0f;
				return false;
			}
			if (lerpVal > 1f || lerpVal < 0f)
			{
				lerpVal = 0f;
				return false;
			}
			return true;
		}
		public static Color32 operator +(Color32 left, Color32 right)
		{
			left.R32 += right.r;
			left.G32 += right.g;
			left.B32 += right.b;
			left.A32 += right.a;
			return left;
		}
		public static Color32 operator -(Color32 left, Color32 right)
		{
			left.R32 -= right.r;
			left.G32 -= right.g;
			left.B32 -= right.b;
			left.A32 -= right.a;
			return left;
		}
		public static Color32 operator *(Color32 left, Color32 right)
		{
			left.R32 *= right.r;
			left.G32 *= right.g;
			left.B32 *= right.b;
			left.A32 *= right.a;
			return left;
		}
		public static Color32 operator /(Color32 left, Color32 right)
		{
			left.R32 = (int)Math.Round((float)left.r / right.r, MidpointRounding.AwayFromZero);
			left.G32 = (int)Math.Round((float)left.g / right.g, MidpointRounding.AwayFromZero);
			left.B32 = (int)Math.Round((float)left.b / right.b, MidpointRounding.AwayFromZero);
			left.A32 = (int)Math.Round((float)left.a / right.a, MidpointRounding.AwayFromZero);
			return left;
		}
		public static Color32 operator +(Color32 left, int right)
		{
			left.R32 += right;
			left.R32 += right;
			left.R32 += right;
			left.R32 += right;
			return left;
		}
		public static Color32 operator -(Color32 left, int right)
		{
			left.R32 -= right;
			left.R32 -= right;
			left.R32 -= right;
			left.R32 -= right;
			return left;
		}
		public static Color32 operator *(Color32 left, int right)
		{
			left.R32 *= right;
			left.R32 *= right;
			left.R32 *= right;
			left.R32 *= right;
			return left;
		}
		public static Color32 operator /(Color32 left, int right)
		{
			left.R32 /= right;
			left.G32 /= right;
			left.B32 /= right;
			left.A32 /= right;
			
			return left;
		}
		public static Color32 operator *(Color32 left, float right)
		{
			left.R32 = left.r * (int)Math.Round(right, MidpointRounding.AwayFromZero);
			left.R32 = left.g * (int)Math.Round(right, MidpointRounding.AwayFromZero);
			left.R32 = left.b * (int)Math.Round(right, MidpointRounding.AwayFromZero);
			left.R32 = left.a * (int)Math.Round(right, MidpointRounding.AwayFromZero);
			return left;
		}
		public static Color32 operator /(Color32 left, float right)
		{
			left.R32 = (int)Math.Round(left.r / right, MidpointRounding.AwayFromZero);
			left.G32 = (int)Math.Round(left.g / right, MidpointRounding.AwayFromZero);
			left.B32 = (int)Math.Round(left.b / right, MidpointRounding.AwayFromZero);
			left.A32 = (int)Math.Round(left.a / right, MidpointRounding.AwayFromZero);
			return left;
		}
		public static Color32 operator -(Color32 color)
		{
			color.r = (byte)(255 - color.r);
			color.g = (byte)(255 - color.g);
			color.b = (byte)(255 - color.b);
			color.a = (byte)(255 - color.a);
			return color;
		}
		public static bool operator ==(Color32 left, Color32 right) => left.Equals(right);
		public static bool operator !=(Color32 left, Color32 right) => !left.Equals(right);
		public static implicit operator Vector4(Color32 color) => new(color.r, color.g, color.b, color.a);
		public static implicit operator Color32(Vector4 color)
		{
			return new(
			(byte)Math.Round(color.X, MidpointRounding.AwayFromZero),
			(byte)Math.Round(color.Y, MidpointRounding.AwayFromZero),
			(byte)Math.Round(color.Z, MidpointRounding.AwayFromZero),
			(byte)Math.Round(color.W, MidpointRounding.AwayFromZero)
				);
		}
		public static explicit operator Color(Color32 color)
		{
			return new(
				color.r / 255f,
				color.g / 255f,
				color.b / 255f,
				color.a / 255f
				);
		}
		public static explicit operator Hsl32Color(Color32 color) => (Hsl32Color)(HslColor)(Color)color;
		public static explicit operator Hsv32Color(Color32 color) => (Hsv32Color)(HsvColor)(Color)color;
		public override readonly int GetHashCode() => HashCode.Combine(r.GetHashCode(), g.GetHashCode(), b.GetHashCode(), a.GetHashCode());
	}
}
