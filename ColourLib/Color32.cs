using System.Numerics;

namespace ColourLib
{
    public struct Color32 : IColorB<Color32>
    {
        private byte r;
        private byte g;
        private byte b;
        private byte a;
        public byte R
        {
            get => r;
            set { r = value; }
        }
        public byte G
        {
            get => g;
            set { g = value; }
        }
        public byte B
        {
            get => b;
            set { b = value; }
        }
        public byte A
        {
            get => a;
            set { a = value; }
        }
		public int R32
		{
			get => r;
			set => r = (byte)Math.Clamp(value, 0, 255);
		}
		public int G32
		{
			get => g;
			set => g = (byte)Math.Clamp(value, 0, 255);
		}
		public int B32
		{
			get => b;
			set => b = (byte)Math.Clamp(value, 0, 255);
		}
		public int A32
		{
			get => a;
			set => a = (byte)Math.Clamp(value, 0, 255);
		}
        public byte this[int i]
        {
            get => i switch
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
                        R = value; break;
                    case 1:
                        G = value; break;
                    case 2:
                        B = value; break;
                    case 3:
                        A = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
		public Color32 Grayscale
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
		public Color32(string hex)
		{
			uint hexInt = uint.Parse(hex, System.Globalization.NumberStyles.HexNumber);
			// Bit shifts != hex shifts. A hex shift of 1 is a bit shift of 4.
			switch (hex.Length)
			{
				case 3:
					R = (byte)((hexInt & 0x00000F00) >> 8);
					G = (byte)((hexInt & 0x000000F0) >> 4);
					B = (byte)(hexInt & 0x0000000F);
					A = 255;
					break;

				case 4:
					R = (byte)((hexInt & 0x0000F000) >> 12);
					G = (byte)((hexInt & 0x00000F00) >> 8);
					B = (byte)((hexInt & 0x000000F0) >> 4);
					A = (byte)(hexInt & 0x0000000F);
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
		public byte Max() => Max(false);
		public byte Min() => Min(false);
		public byte Max(bool compareAlpha) => compareAlpha ? Math.Max(r, Math.Max(g, Math.Max(b, a))) : Math.Max(r, Math.Max(g, b));
		public byte Min(bool compareAlpha) => compareAlpha ? Math.Min(r, Math.Min(g, Math.Min(b, a))) : Math.Min(r, Math.Min(g, b));
		public string ToHex()
		{
			return r.ToString("X") + g.ToString("X") + b.ToString("X") + (a == 255 ? "" : a.ToString("X"));
		}
		public static string ToHex(Color32 color)
		{
			return color.R.ToString("X") + color.G.ToString("X") + color.B.ToString("X") + (color.A == 255 ? "" : color.A.ToString("X"));
		}
		public static Color32 FromHex(string hex) => new(hex);
		public bool Equals(Color32 color) => color.R == r && color.G == g && color.B == b && color.A == a;
		public override bool Equals(object? color) => color is Color32 c && color is not null && Equals(c);
		public Color32 Difference(Color32 color)
		{
			throw new NotImplementedException();
		}

		public static Color32 Difference(Color32 from, Color32 to)
		{
			throw new NotImplementedException();
		}
		public string ToString(string? format, IFormatProvider? formatProvider) => $"<{r},{g},{b},{a}>";
		public Color32 Lerp(Color32 to, float val)
		{
			throw new NotImplementedException();
		}

		public static Color32 Lerp(Color32 from, Color32 to, float val)
		{
			throw new NotImplementedException();
		}

		public Color32 LerpUnclamped(Color32 to, float val)
		{
			throw new NotImplementedException();
		}

		public static Color32 LerpUnclamped(Color32 from, Color32 to, float val)
		{
			throw new NotImplementedException();
		}

		
		public static Color32 operator +(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator -(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator *(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator /(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator +(Color32 left, byte right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator -(Color32 left, byte right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator *(Color32 left, byte right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator /(Color32 left, byte right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator -(Color32 color)
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static implicit operator Vector4(Color32 color)
		{
			throw new NotImplementedException();
		}

		public static implicit operator Color32(Vector4 color)
		{
			throw new NotImplementedException();
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
		public static explicit operator HSL24Color(Color32 color) => (HSL24Color)(HSLColor)(Color)color;	// Yuck
		public static explicit operator HSV24Color(Color32 color) => (HSV24Color)(HSVColor)(Color)color;
		public override readonly int GetHashCode() => HashCode.Combine(r.GetHashCode(), g.GetHashCode(), b.GetHashCode(), a.GetHashCode());
	}
}
