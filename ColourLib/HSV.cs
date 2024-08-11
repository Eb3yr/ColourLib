using System.Numerics;

namespace ColourLib
{
    public struct HSVColor : IColorF<HSVColor>
    {
        private float h;
        private float s;
        private float v;
        public float H
            {
            get => h;
            set { h = Math.Clamp(value, 0f, 1f); }
            }
        public float S
            {
            get => s;
            set { s = Math.Clamp(value, 0f, 1f); }
            }
        public float V
        {
            get => v;
            set { v = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
        {
            get => i switch
            {
                0 => h,
                1 => s,
                2 => v,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch (i)
                {
                    case 0:
                        h = value; break;
                    case 1:
                        s = value; break;
                    case 2:
                        v = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        public HSVColor(float H, float S, float V)
        {
            this.H = H;
            this.S = S;
            this.V = V;
        }
		public Vector3 GetHSV24() => new(h * 360f, s * 100f, v * 100f); // Temp until I implement a HSVColor32 class
		public bool Equals(HSVColor color) => H == color.H && S == color.S && V == color.V;
        public override bool Equals(object? color) => color is HSVColor c && color is not null && Equals(c);
		public float Max() => Math.Max(h, Math.Max(s, v)); 
        public float Min() => Math.Min(h, Math.Min(s, v));
		public HSVColor Difference(HSVColor color) => Difference(this, color);
        public static HSVColor Difference(HSVColor left, HSVColor right)
        {
            right.H = Math.Abs(right.H - left.H);
            right.S = Math.Abs(right.S - left.S);
            right.V = Math.Abs(right.V - left.V);
            return right;
        }
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{h}, {s}, {v}>";
        public HSVColor Lerp(HSVColor to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
        public static HSVColor Lerp(HSVColor from, HSVColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
        public HSVColor LerpUnclamped(HSVColor to, float val) => LerpUnclamped(this, to, val);
        public static HSVColor LerpUnclamped(HSVColor from, HSVColor to, float val)
        {
            return new()
            {
                H = (from.h * (1.0f - val)) + (to.h * val),
                S = (from.s * (1.0f - val)) + (to.s * val),
                V = (from.v * (1.0f - val)) + (to.v * val)
            };
        }
		public static HSVColor operator +(HSVColor left, HSVColor right)
		{
			left.H += right.H;
			left.S += right.S;
			left.V += right.V;
			return left;
		}
		public static HSVColor operator -(HSVColor left, HSVColor right)
		{
			left.H -= right.H;
			left.S -= right.S;
			left.V -= right.V;
			return left;
		}
		public static HSVColor operator *(HSVColor left, HSVColor right)
		{
			left.H *= right.H;
			left.S *= right.S;
			left.V *= right.V;
			return left;
		}
		public static HSVColor operator /(HSVColor left, HSVColor right)
		{
			left.H /= right.H;
			left.S /= right.S;
			left.V /= right.V;
			return left;
		}
		public static HSVColor operator +(HSVColor left, float right)
		{
			left.H += right;
			left.S += right;
			left.V += right;
			return left;
		}
		public static HSVColor operator -(HSVColor left, float right)
		{
			left.H -= right;
			left.S -= right;
			left.V -= right;
			return left;
		}
		public static HSVColor operator *(HSVColor left, float right)
		{
			left.H *= right;
			left.S *= right;
			left.V *= right;
			return left;
		}
		public static HSVColor operator /(HSVColor left, float right)
		{
			left.H /= right;
			left.S /= right;
			left.V /= right;
			return left;
		}
		public static HSVColor operator -(HSVColor color)
		{
			color.H = 1f - color.H;
			color.S = 1f - color.S;
			color.V = 1f - color.V;
			return color;
		}
		public static bool operator ==(HSVColor left, HSVColor right) => left.Equals(right);
        public static bool operator !=(HSVColor left, HSVColor right) => !left.Equals(right);
        public static implicit operator Vector3(HSVColor color) => new(color.H, color.S, color.V);
        public static implicit operator Vector4(HSVColor color) => new(color.H, color.S, color.V, float.NaN);
        public static implicit operator HSVColor(Vector3 color) => new(color.X, color.Y, color.Z);
        public static implicit operator HSVColor(Vector4 color) => new(color.X, color.Y, color.Z);
		public static explicit operator Color(HSVColor color)
        {
            float C = color.v * color.s;
            float hPrime = color.h * 6f;
            float X = C * (1 - Math.Abs(hPrime % 2 - 1));
            Color rgb = hPrime switch
            {
				< 1 => new(C, X, 0),
				< 2 => new(X, C, 0),
				< 3 => new(0, C, X),
				< 4 => new(0, X, C),
				< 5 => new(X, 0, C),
				< 6 => new(C, 0, X),
				_ => throw new ArgumentOutOfRangeException($"hPrime = {hPrime} exceeds the range [0, 6] in HSVColor.cs")
			};
            rgb += color.v - C;
            rgb.A = 1f;
			return rgb;
        }
        public static explicit operator HSLColor(HSVColor color)
        {
            float L = color.v * (1f - 0.5f * color.S);
            float S = L == 0f || L == 1f ? 0f : (color.v - L) / Math.Min(L, 1f - L);
            return new(color.h, S, L);
        }
		public override readonly int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), v.GetHashCode());
    }
}
