using System.Numerics;

namespace ColourLib
{
    public struct Color : IColorF<Color>
    {
        private float r;
        private float g;
        private float b;
        private float a;
        public float R
        {
        get => r;
        set { r = Math.Clamp(value, 0f, 1f); }
        }
        public float G
        {
        get => g;
        set { g = Math.Clamp(value, 0f, 1f); }
        }
        public float B
        {
        get => b;
        set { b = Math.Clamp(value, 0f, 1f); }
        }
        public float A
        {
            get => a;
            set { a = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
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
                switch(i)
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
        public Color Grayscale { get => new(0.299f * r, 0.587f * g, 0.114f * b, a); }
        public Color(float R, float G, float B, float A = 1f)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
        public Color(int R, int G, int B, int A = 255)
        {
            this.R = R / 255f;
            this.G = G / 255f;
            this.B = B / 255f;
            this.A = A / 255f;
        }
        public Color(string hex)
        {
			if (hex[0] == '#')
				hex = hex.Substring(1);

			uint hexInt = uint.Parse(hex, System.Globalization.NumberStyles.HexNumber);
			switch (hex.Length)
			{
				case 3:
					R = (((hexInt & 0x00000F00) | (hexInt & 0x00000F00) << 4) >> 8) / 255f;
					G = (((hexInt & 0x000000F0) | (hexInt & 0x000000F0) << 4) >> 4) / 255f;
					B = ((hexInt & 0x0000000F) | (hexInt & 0x0000000F) << 4) / 255f;
					A = 1f;
					break;

				case 4:
					R = (((hexInt & 0x0000F000) | (hexInt & 0x0000F000) << 4) >> 12) / 255f;
					G = (((hexInt & 0x00000F00) | (hexInt & 0x00000F00) << 4) >> 8) / 255f;
					B = (((hexInt & 0x000000F0) | (hexInt & 0x000000F0) << 4) >> 4) / 255f;
					A = ((hexInt & 0x0000000F) | (hexInt & 0x0000000F) << 4) / 255f;
					break;

				case 6:
					R = ((hexInt & 0x00FF0000) >> 16) / 255f;
					G = ((hexInt & 0x0000FF00) >> 8) / 255f;
					B = (hexInt & 0x000000FF) / 255f;
					A = 1f;
					break;

				case 8:
					R = ((hexInt & 0xFF000000) >> 24) / 255f;
					G = ((hexInt & 0x00FF0000) >> 16) / 255f;
					B = ((hexInt & 0x0000FF00) >> 8) / 255f;
					A = (hexInt & 0x000000FF) / 255f;
					break;

				default:
					throw new ArgumentException("Color constructor only accepts hexadecimal strings of length 3, 4, 6 or 8.");
			}
		}
        public bool Equals(Color color) => color.R == R && color.G == G && color.B == B && color.A == A;
        public override bool Equals(object? color) => color is Color c && color is not null && Equals(c);
        public float Max() => Max(false);
        public float Min() => Min(false);
		public float Max(bool compareAlpha) => compareAlpha ? Math.Max(r, Math.Max(g, Math.Max(b, a))) : Math.Max(r, Math.Max(g, b));
		public float Min(bool compareAlpha) => compareAlpha ? Math.Min(r, Math.Min(g, Math.Min(b, a))) : Math.Min(r, Math.Min(g, b));
        public string ToHex() => ((Color32)this).ToHex();
        public static string ToHex(Color color) => color.ToHex();
        public static Color FromHex(string hex) => new(hex);
		public Color Difference(Color color) => Difference(this, color);
        public static Color Difference(Color left, Color right)
        {
            right.R = Math.Abs(right.R - left.R);
            right.G = Math.Abs(right.G - left.G);
            right.B = Math.Abs(right.B - left.B);
            right.A = Math.Abs(right.A - left.A);
            return right;
        }
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{r},{g},{b},{a}>";
        public Color Lerp(Color colorTo, float val) => LerpUnclamped(this, colorTo, Math.Clamp(val, 0f, 1f));
        public static Color Lerp(Color from, Color to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
        public Color LerpUnclamped(Color to, float val) => LerpUnclamped(this, to, val);
        public static Color LerpUnclamped(Color from, Color to, float val)
        {
			from.R = (from.r * (1.0f - val)) + (to.r * val);
            from.G = (from.g * (1.0f - val)) + (to.g * val);
            from.B = (from.b * (1.0f - val)) + (to.b * val);
			from.A = (from.a * (1.0f - val)) + (to.a * val);
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
		public static Color operator +(Color left, Color right)
		{
			left.R += right.R;
			left.G += right.G;
			left.B += right.B;
            left.A += right.A;
			return left;
		}
		public static Color operator -(Color left, Color right)
		{
			left.R -= right.R;
			left.G -= right.G;
			left.B -= right.B;
            left.A -= right.A;
			return left;
		}
		public static Color operator *(Color left, Color right)
		{
			left.R *= right.R;
			left.G *= right.G;
			left.B *= right.B;
            left.A *= right.A;
			return left;
		}
		public static Color operator /(Color left, Color right)
		{
			left.R /= right.R;
			left.G /= right.G;
			left.B /= right.B;
            left.A /= right.A;
			return left;
		}
		public static Color operator +(Color left, float right)
		{
			left.R += right;
			left.G += right;
			left.B += right;
            left.A += right;
			return left;
		}
		public static Color operator -(Color left, float right)
		{
			left.R -= right;
			left.G -= right;
			left.B -= right;
            left.A -= right;
			return left;
		}
		public static Color operator *(Color left, float right)
		{
			left.R *= right;
			left.G *= right;
			left.B *= right;
            left.A *= right;
			return left;
		}
		public static Color operator /(Color left, float right)
		{
			left.R /= right;
			left.G /= right;
			left.B /= right;
            left.A /= right;
			return left;
		}
		public static Color operator -(Color color)
		{
			color.R = 1f - color.R;
			color.G = 1f - color.G;
			color.B = 1f - color.B;
			color.A = 1f - color.A;
			return color;
		}
		public static bool operator ==(Color left, Color right) => left.Equals(right);
        public static bool operator !=(Color left, Color right) => !left.Equals(right);

        public static implicit operator Vector4(Color color) => new(color.R, color.G, color.B, color.A);

        public static implicit operator Color(Vector4 color) => new(color.X, color.Y, color.Z, color.W);
        public static explicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(
                (int)Math.Round(color.A * 255f, MidpointRounding.AwayFromZero),
                (int)Math.Round(color.R * 255f, MidpointRounding.AwayFromZero),
                (int)Math.Round(color.G * 255f, MidpointRounding.AwayFromZero),
                (int)Math.Round(color.B * 255f, MidpointRounding.AwayFromZero)
                );
        }
        public static explicit operator Color(System.Drawing.Color color) => new(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);

		public static explicit operator HSVColor(Color color)
        {
			float Xmax = color.Max();   // = V
			float Xmin = color.Min();   // = V - C
			float C = Xmax - Xmin;  // Chroma = 2(V - L)
			float H = 0f;
			if (Xmax == color.r)
				H = ((color.g - color.b) / C) % 6;
			else if (Xmax == color.g)
				H = (color.b - color.r) / C + 2;
			else if (Xmax == color.b)
				H = (color.r - color.g) / C + 4;

			H /= 6f;
            float S = Xmax == 0 ? 0f : C / Xmax;
            return new(H, S, Xmax);
        }

        public static explicit operator HSLColor(Color color)
        {
            float Xmax = color.Max();   // = V
            float Xmin = color.Min();   // = V - C
            float C = Xmax - Xmin;  // Chroma = 2(V - L)
            float L = (Xmax + Xmin) * 0.5f;
            float H = 0f;
            if (Xmax == color.r)
                H = ((color.g - color.b) / C) % 6;
            else if (Xmax == color.g)
                H = (color.b - color.r) / C + 2;
            else if (Xmax == color.b)
                H = (color.r - color.g) / C + 4;

            H /= 6f;
            float S = L == 0 || L == 1 ? 0f : (Xmax - L) / Math.Min(L, 1f - L);
            return new(H, S, L);
        }
        public static explicit operator Color32(Color color) => new(color.R, color.G, color.B, color.A);
		public override readonly int GetHashCode() => HashCode.Combine(r.GetHashCode(), g.GetHashCode(), b.GetHashCode(), a.GetHashCode());
    }
}
