﻿using System.Numerics;
using System.Text.Json.Serialization;

namespace ColourLib
{
    
    public struct HslColor : IColorF<HslColor>
    {
        [JsonInclude]
        private float h;
		[JsonInclude]
		private float s;
		[JsonInclude]
		private float l;
        [JsonIgnore]
        public float H
        {
			readonly get => h;
            set { h = float.IsPositive(value) ? value % 1f : -value % 1f ; }
        }
		[JsonIgnore]
        public float S
        {
			readonly get => s;
            set { s = Math.Clamp(value, 0f, 1f); }
        }
		[JsonIgnore]
		public float L
        {
			readonly get => l;
            set { l = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
        {
			readonly get => i switch
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
        public HslColor(float H, float S, float L)
        {
            this.H = H;
            this.S = S;
            this.L = L;
        }
        public HslColor(HslColor color) : this(color.h, color.s, color.l) { }
        public readonly bool Equals(HslColor color) => H == color.H && S == color.S && L == color.L;
        public readonly override bool Equals(object? color) => color is HslColor c && color is not null && Equals(c);
		public readonly float Max() => Math.Max(h, Math.Max(s, l)); 
        public readonly float Min() => Math.Min(h, Math.Min(s, l));
        public readonly HslColor Difference(HslColor color) => Difference(this, color);
        public static HslColor Difference(HslColor left, HslColor right)
        {
            right.H = Math.Abs(right.H - left.H);
            right.S = Math.Abs(right.S - left.S);
            right.L = Math.Abs(right.L - left.L);
            return right;
        }
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{h},{s},{l}>";
        public readonly HslColor Lerp(HslColor to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
        public static HslColor Lerp(HslColor from, HslColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
        public readonly HslColor LerpUnclamped(HslColor to, float val) => LerpUnclamped(this, to, val);
		public static HslColor LerpUnclamped(HslColor from, HslColor to, float val)
		{
            if (to.H < from.H)
				from.H = ((to.h + 1f - from.h) * val) - 1f + from.h;
            else
                from.H = (from.h * (1.0f - val)) + (to.h * val);

            from.S = (from.s * (1.0f - val)) + (to.s * val);
            from.L = (from.l * (1.0f - val)) + (to.l * val);
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
		public static HslColor operator +(HslColor left, HslColor right)
        {
            left.H += right.H;
            left.S += right.S;
            left.L += right.L;
            return left;
        }
        public static HslColor operator -(HslColor left, HslColor right)
        {
            left.H -= right.H;
            left.S -= right.S;
            left.L -= right.L;
            return left;
        }
        public static HslColor operator *(HslColor left, HslColor right)
        {
            left.H *= right.H;
            left.S *= right.S;
            left.L *= right.L;
            return left;
        }
        public static HslColor operator /(HslColor left, HslColor right)
        {
            left.H /= right.H;
            left.S /= right.S;
            left.L /= right.L;
            return left;
        }
        public static HslColor operator +(HslColor left, float right)
        { 
            left.H += right;
            left.S += right;
            left.L += right;
            return left;
        }
		public static HslColor operator -(HslColor left, float right)
        {
            left.H -= right;
            left.S -= right;
            left.L -= right;
            return left;
        }
		public static HslColor operator *(HslColor left, float right)
        {
            left.H *= right;
            left.S *= right;
            left.L *= right;
            return left;
        }
		public static HslColor operator /(HslColor left, float right)
        { 
            left.H /= right;
            left.S /= right;
            left.L /= right;
            return left;
        }
		public static HslColor operator -(HslColor color)
        {
            color.H = 1f - color.H;
            color.S = 1f - color.S;
            color.L = 1f - color.L;
            return color;
        }
        public static bool operator ==(HslColor left, HslColor right) => left.Equals(right);
        public static bool operator !=(HslColor left, HslColor right) => !left.Equals(right);
		public static implicit operator Vector4(HslColor color) => new(color.H, color.S, color.L, float.NaN);
		public static implicit operator HslColor(Vector4 color) => new(color.X, color.Y, color.Z);
		public static explicit operator Color(HslColor color)
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
        public static explicit operator HsvColor(HslColor color)
        {
            float V = color.l + color.S * Math.Min(color.l, 1f - color.l);
            float S = V == 0f ? 0f : 2f * (1f - color.l / V);
            return new(color.h, S, V);
        }
        public static explicit operator Hsl32Color(HslColor color) => new(color.h, color.s, color.l);
        public override readonly int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), l.GetHashCode());
	}
}