using System.Numerics;

namespace ColourLib
{
    public struct HSLColor : IColorF<HSLColor>
    {
        private float h;
        private float s;
        private float l;
        public float H
        {
            get => h;
            set { h = value % 1f; }
        }
        public float S
        {
            get => s;
            set { s = Math.Clamp(value, 0f, 1f); }
        }
        public float L
        {
            get => l;
            set { l = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
        {
            get => i switch
            {
                0 => h,
                1 => s,
                2 => l,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch (i)
                {
                    case 0:
                        H = value; break;
                    case 1:
                        S = value; break;
                    case 2:
                        L = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        public HSLColor(float H, float S, float L)
        {
            this.H = H;
            this.S = S;
            this.L = L;
        }
        public Vector3 GetHSL24() => new(h * 360f, s * 100f, l * 100f); // Temp until I implement a HSLColor32 class
        public bool Equals(HSLColor color) => H == color.H && S == color.S && L == color.L;
        public override bool Equals(object? color) => color is HSLColor c && color is not null && Equals(c);
		public float Max() => Math.Max(h, Math.Max(s, l)); 
        public float Min() => Math.Min(h, Math.Min(s, l));
        public HSLColor Difference(HSLColor color) => Difference(this, color);
        public static HSLColor Difference(HSLColor left, HSLColor right)
        {
            right.H = Math.Abs(right.H - left.H);
            right.S = Math.Abs(right.S - left.S);
            right.L = Math.Abs(right.L - left.L);
            return right;
        }
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{h},{s},{l}>";
        public HSLColor Lerp(HSLColor to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
        public static HSLColor Lerp(HSLColor from, HSLColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
        public HSLColor LerpUnclamped(HSLColor to, float val) => LerpUnclamped(this, to, val);
		public static HSLColor LerpUnclamped(HSLColor from, HSLColor to, float val)
		{
            return new(
                (from.h * (1.0f - val)) + (to.h * val),
                (from.s * (1.0f - val)) + (to.s * val),
                (from.l * (1.0f - val)) + (to.l * val)
            );
		}
        public static HSLColor operator +(HSLColor left, HSLColor right)
        {
            left.H += right.H;
            left.S += right.S;
            left.L += right.L;
            return left;
        }
        public static HSLColor operator -(HSLColor left, HSLColor right)
        {
            left.H -= right.H;
            left.S -= right.S;
            left.L -= right.L;
            return left;
        }
        public static HSLColor operator *(HSLColor left, HSLColor right)
        {
            left.H *= right.H;
            left.S *= right.S;
            left.L *= right.L;
            return left;
        }
        public static HSLColor operator /(HSLColor left, HSLColor right)
        {
            left.H /= right.H;
            left.S /= right.S;
            left.L /= right.L;
            return left;
        }
        public static HSLColor operator +(HSLColor left, float right)
        { 
            left.H += right;
            left.S += right;
            left.L += right;
            return left;
        }
		public static HSLColor operator -(HSLColor left, float right)
        {
            left.H -= right;
            left.S -= right;
            left.L -= right;
            return left;
        }
		public static HSLColor operator *(HSLColor left, float right)
        {
            left.H *= right;
            left.S *= right;
            left.L *= right;
            return left;
        }
		public static HSLColor operator /(HSLColor left, float right)
        { 
            left.H /= right;
            left.S /= right;
            left.L /= right;
            return left;
        }
		public static HSLColor operator -(HSLColor color)
        {
            color.H = 1f - color.H;
            color.S = 1f - color.S;
            color.L = 1f - color.L;
            return color;
        }
        public static bool operator ==(HSLColor left, HSLColor right) => left.Equals(right);
        public static bool operator !=(HSLColor left, HSLColor right) => !left.Equals(right);
		public static implicit operator Vector4(HSLColor color) => new(color.H, color.S, color.L, float.NaN);
		public static implicit operator HSLColor(Vector4 color) => new(color.X, color.Y, color.Z);
		public static explicit operator Color(HSLColor color)
        {
            float C = (1 - Math.Abs(2 * color.l - 1)) * color.s;
            float hPrime = color.h * 6;
            float X = C * (1 - Math.Abs(hPrime % 2 - 1));
			Color rgb1 = hPrime switch
			{
				< 1 => new(C, X, 0),
				< 2 => new(X, C, 0),
				< 3 => new(0, C, X),
				< 4 => new(0, X, C),
				< 5 => new(X, 0, C),
				< 6 => new(C, 0, X),
				_ => throw new ArgumentOutOfRangeException($"hPrime = {hPrime} exceeds the range [0, 6] in HSLColor.cs")
			};
			rgb1 += color.l - 0.5f * C;
			rgb1.A = 1f;
			return rgb1;
		}
        public static explicit operator HSVColor(HSLColor color)
        {
            float V = color.l + color.S * Math.Min(color.l, 1f - color.l);
            float S = V == 0f ? 0f : 2f * (1f - color.l / V);
            return new(color.h, S, V);
        }
        public static explicit operator HSL24Color(HSLColor color)
        {
            return new(
                (byte)Math.Round(color.h, MidpointRounding.AwayFromZero),
                (byte)Math.Round(color.s, MidpointRounding.AwayFromZero),
                (byte)Math.Round(color.l, MidpointRounding.AwayFromZero));
        }
        public override readonly int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), l.GetHashCode());
	}
}
