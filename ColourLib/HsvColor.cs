using System.Numerics;

namespace ColourLib
{
    public struct HsvColor : IColorF<HsvColor>
    {
        private float h;
        private float s;
        private float v;
        public float H
            {
            readonly get => h;
            set { h = value % 1f; }
            }
        public float S
            {
            readonly get => s;
            set { s = Math.Clamp(value, 0f, 1f); }
            }
        public float V
        {
			readonly get => v;
            set { v = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
        {
			readonly get => i switch
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
        public HsvColor(float H, float S, float V)
        {
            this.H = H;
            this.S = S;
            this.V = V;
        }
		public readonly bool Equals(HsvColor color) => H == color.H && S == color.S && V == color.V;
        public override bool Equals(object? color) => color is HsvColor c && color is not null && Equals(c);
		public readonly float Max() => Math.Max(h, Math.Max(s, v)); 
        public readonly float Min() => Math.Min(h, Math.Min(s, v));
		public readonly HsvColor Difference(HsvColor color) => Difference(this, color);
        public static HsvColor Difference(HsvColor left, HsvColor right)
        {
            right.H = Math.Abs(right.H - left.H);
            right.S = Math.Abs(right.S - left.S);
            right.V = Math.Abs(right.V - left.V);
            return right;
        }
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{h},{s},{v}>";
        public readonly HsvColor Lerp(HsvColor to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
        public static HsvColor Lerp(HsvColor from, HsvColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
        public readonly HsvColor LerpUnclamped(HsvColor to, float val) => LerpUnclamped(this, to, val);
        public static HsvColor LerpUnclamped(HsvColor from, HsvColor to, float val)
        {
			if (to.H < from.H)
				from.H = ((to.h + 1f - from.h) * val) - 1f + from.h;
			else
				from.H = (from.h * (1.0f - val)) + (to.h * val);

			from.S = (from.s * (1.0f - val)) + (to.s * val);
            from.V = (from.v * (1.0f - val)) + (to.v * val);
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
		public static HsvColor operator +(HsvColor left, HsvColor right)
		{
			left.H += right.H;
			left.S += right.S;
			left.V += right.V;
			return left;
		}
		public static HsvColor operator -(HsvColor left, HsvColor right)
		{
			left.H -= right.H;
			left.S -= right.S;
			left.V -= right.V;
			return left;
		}
		public static HsvColor operator *(HsvColor left, HsvColor right)
		{
			left.H *= right.H;
			left.S *= right.S;
			left.V *= right.V;
			return left;
		}
		public static HsvColor operator /(HsvColor left, HsvColor right)
		{
			left.H /= right.H;
			left.S /= right.S;
			left.V /= right.V;
			return left;
		}
		public static HsvColor operator +(HsvColor left, float right)
		{
			left.H += right;
			left.S += right;
			left.V += right;
			return left;
		}
		public static HsvColor operator -(HsvColor left, float right)
		{
			left.H -= right;
			left.S -= right;
			left.V -= right;
			return left;
		}
		public static HsvColor operator *(HsvColor left, float right)
		{
			left.H *= right;
			left.S *= right;
			left.V *= right;
			return left;
		}
		public static HsvColor operator /(HsvColor left, float right)
		{
			left.H /= right;
			left.S /= right;
			left.V /= right;
			return left;
		}
		public static HsvColor operator -(HsvColor color)
		{
			color.H = 1f - color.H;
			color.S = 1f - color.S;
			color.V = 1f - color.V;
			return color;
		}
		public static bool operator ==(HsvColor left, HsvColor right) => left.Equals(right);
        public static bool operator !=(HsvColor left, HsvColor right) => !left.Equals(right);
        public static implicit operator Vector3(HsvColor color) => new(color.H, color.S, color.V);
        public static implicit operator Vector4(HsvColor color) => new(color.H, color.S, color.V, float.NaN);
        public static implicit operator HsvColor(Vector3 color) => new(color.X, color.Y, color.Z);
        public static implicit operator HsvColor(Vector4 color) => new(color.X, color.Y, color.Z);
		public static explicit operator Color(HsvColor color)
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
        public static explicit operator HslColor(HsvColor color)
        {
            float L = color.v * (1f - 0.5f * color.S);
            float S = L == 0f || L == 1f ? 0f : (color.v - L) / Math.Min(L, 1f - L);
            return new(color.h, S, L);
        }
        public static explicit operator Hsv32Color(HsvColor color) => new(color.h, color.s, color.v);
		public override readonly int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), v.GetHashCode());
    }
}
