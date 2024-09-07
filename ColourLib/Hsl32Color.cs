using System.Numerics;

namespace ColourLib
{
	public struct Hsl32Color : IColorB<Hsl32Color>
	{
		private short h;
		private byte s;
		private byte l;
		public short H
		{
			readonly get => h;
			set => h = (short)(value % 360);
		}
		public byte S
		{
			readonly get => s;
			set => s = (byte)Math.Clamp((int)value, 0, 100);
		}
		public byte L
		{
			readonly get => l;
			set => l = (byte)Math.Clamp((int)value, 0, 100);
		}
		public int H32
		{
			readonly get => h;
			set => h = (short)(value % 360);
		}
		public int S32
		{
			readonly get => s;
			set => s = (byte)Math.Clamp(value, 0, 100);
		}
		public int L32
		{
			readonly get => l;
			set => l = (byte)Math.Clamp(value, 0, 100);
		}
		public int this[int i]
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
						H32 = value; break;
					case 1:
						S32 = value; break;
					case 2:
						L32 = value; break;
					default:
						throw new IndexOutOfRangeException();
				}
			}
		}
		public Hsl32Color(byte h, byte s, byte l)
		{
			H = h;
			S = s;
			L = l;
		}
		public Hsl32Color(int h, int s, int l)
		{
			H32 = h;
			S32 = s;
			L32 = l;
		}
		public Hsl32Color(float h, float s, float l)
		{
			H32 = (int)Math.Round(h * 360f, MidpointRounding.AwayFromZero);
			S32 = (int)Math.Round(s * 100f, MidpointRounding.AwayFromZero);
			L32 = (int)Math.Round(l * 100f, MidpointRounding.AwayFromZero);
		}
		public readonly bool Equals(Hsl32Color color) => h == color.h && s == color.s && l == color.l;
		public override readonly bool Equals(object? color) => color is Hsl32Color c && color is not null && Equals(c);
		public static Hsl32Color Difference(Hsl32Color left, Hsl32Color right)
		{
			left.H = (byte)Math.Abs(left.h - right.h);
			left.S = (byte)Math.Abs(left.s - right.s);
			left.L = (byte)Math.Abs(left.l - right.l);
			return left;
		}
		public readonly Hsl32Color Difference(Hsl32Color color)
		{
			color.H = (byte)Math.Abs(h - color.h);
			color.S = (byte)Math.Abs(s - color.s);
			color.L = (byte)Math.Abs(l - color.l);
			return color;
		}
		public readonly Hsl32Color Lerp(Hsl32Color to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
		public static Hsl32Color Lerp(Hsl32Color from, Hsl32Color to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
		public readonly Hsl32Color LerpUnclamped(Hsl32Color to, float val) => LerpUnclamped(this, to, val);
		public static Hsl32Color LerpUnclamped(Hsl32Color from, Hsl32Color to, float val)
		{
			if (to.H < from.H)
				from.H = (byte)Math.Round((((to.h + 255 - from.h) * val) - 255 + from.h), MidpointRounding.AwayFromZero);
			else
				from.H = (byte)Math.Round(from.h * (1.0f - val) + (to.h * val), MidpointRounding.AwayFromZero);

			// float ver
			//if (to.H < from.H)
			//	from.H = ((to.h + 1f - from.h) * val) - 1f + from.h;
			//else
			//	from.H = (from.h * (1.0f - val)) + (to.h * val);

			from.S = (byte)Math.Round(from.s * (1.0f - val) + (to.s * val), MidpointRounding.AwayFromZero);
			from.L = (byte)Math.Round(from.l * (1.0f - val) + (to.l * val), MidpointRounding.AwayFromZero);
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
		public readonly int Max() => Math.Max(h, Math.Max(s, l));
		public readonly int Min() => Math.Min(h, Math.Min(s, l));
		public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"<{h},{s},{l}>";
		public static Hsl32Color operator +(Hsl32Color left, Hsl32Color right)
		{
			left.H += right.h;
			left.S += right.s;
			left.L += right.l;
			return left;
		}
		public static Hsl32Color operator +(Hsl32Color left, int right)
		{
			left.H32 += right;
			left.S32 += right;
			left.L32 += right;
			return left;
		}
		public static Hsl32Color operator -(Hsl32Color left, Hsl32Color right)
		{
			left.H -= right.h;
			left.S -= right.s;
			left.L -= right.l;
			return left;
		}
		public static Hsl32Color operator -(Hsl32Color left, int right)
		{
			left.H32 -= right;
			left.S32 -= right;
			left.L32 -= right;
			return left;
		}
		public static Hsl32Color operator *(Hsl32Color left, Hsl32Color right)
		{
			left.H *= right.h;
			left.S *= right.s;
			left.L *= right.l;
			return left;
		}
		public static Hsl32Color operator *(Hsl32Color left, int right)
		{
			left.H32 *= right;
			left.S32 *= right;
			left.L32 *= right;
			return left;
		}
		public static Hsl32Color operator *(Hsl32Color left, float right)
		{
			left.H32 *= (int)Math.Round(right, MidpointRounding.AwayFromZero);
			left.S32 *= (int)Math.Round(right, MidpointRounding.AwayFromZero);
			left.L32 *= (int)Math.Round(right, MidpointRounding.AwayFromZero);
			return left;
		}
		public static Hsl32Color operator /(Hsl32Color left, Hsl32Color right)
		{
			left.H /= right.h;
			left.S /= right.s;
			left.L /= right.l;
			return left;
		}
		public static Hsl32Color operator /(Hsl32Color left, int right)
		{
			left.H32 /= right;
			left.S32 /= right;
			left.L32 /= right;
			return left;
		}
		public static Hsl32Color operator /(Hsl32Color left, float right)
		{
			left.H32 /= (int)Math.Round(right, MidpointRounding.AwayFromZero);
			left.S32 /= (int)Math.Round(right, MidpointRounding.AwayFromZero);
			left.L32 /= (int)Math.Round(right, MidpointRounding.AwayFromZero);
			return left;
		}
		public static Hsl32Color operator -(Hsl32Color color)
		{
			color.h = (short)(360 - color.h);
			color.s = (byte)(100 - color.s);
			color.l = (byte)(100 - color.l);
			return color;
		}
		public static bool operator ==(Hsl32Color left, Hsl32Color right) => left.Equals(right);
		public static bool operator !=(Hsl32Color left, Hsl32Color right) => !left.Equals(right);
		public static implicit operator Vector4(Hsl32Color color) => new(color.h, color.s, color.l, float.NaN);
		public static implicit operator Hsl32Color(Vector4 color)
		{
			return new(
				(short)Math.Round(color.X, MidpointRounding.AwayFromZero),
				(byte)Math.Round(color.Y, MidpointRounding.AwayFromZero),
				(byte)Math.Round(color.Z, MidpointRounding.AwayFromZero));
		}
		public static explicit operator HslColor(Hsl32Color color) => new(color.h / 360f, color.s / 100f, color.l / 100f);
		public static explicit operator Color32(Hsl32Color color) => (Color32)(Color)(HslColor)color;
		public static explicit operator Hsv32Color(Hsl32Color color) => (Hsv32Color)(HsvColor)(HslColor)color;
		public static explicit operator Color(Hsl32Color color) => (Color)(HslColor)color;
		public override readonly int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), l.GetHashCode());
	}
}
