using System.Numerics;

namespace ColourLib
{
	public struct Hsv32Color : IColorB<Hsv32Color>
	{
		private short h;
		private byte s;
		private byte v;
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
		public byte V
		{
			readonly get => v;
			set => v = (byte)Math.Clamp((int)value, 0, 100);
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
		public int V32
		{
			readonly get => v;
			set => v = (byte)Math.Clamp(value, 0, 100);
		}
		public int this[int i]
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
						H32 = value; break;
					case 1:
						S32 = value; break;
					case 2:
						V32 = value; break;
					default:
						throw new IndexOutOfRangeException();
				}
			}
		}
		public Hsv32Color(short h, byte s, byte v)
		{
			H = h;
			S = s;
			V = v;
		}
		public Hsv32Color(int h, int s, int v)
		{
			H32 = h;
			S32 = s;
			V32 = v;
		}
		public Hsv32Color(float h, float s, float v)
		{
			H32 = (int)Math.Round(h * 360f, MidpointRounding.AwayFromZero);
			S32 = (int)Math.Round(s * 100f, MidpointRounding.AwayFromZero);
			V32 = (int)Math.Round(v * 100f, MidpointRounding.AwayFromZero);
		}
		public readonly bool Equals(Hsv32Color color) => h == color.h && s == color.s && v == color.v;
		public override readonly bool Equals(object? color) => color is Hsv32Color c && color is not null && Equals(c);
		public static Hsv32Color Difference(Hsv32Color left, Hsv32Color right)
		{
			left.H = (byte)Math.Abs(left.h - right.h);
			left.S = (byte)Math.Abs(left.s - right.s);
			left.V = (byte)Math.Abs(left.v - right.v);
			return left;
		}
		public readonly Hsv32Color Difference(Hsv32Color color)
		{
			color.H = (byte)Math.Abs(h - color.h);
			color.S = (byte)Math.Abs(s - color.s);
			color.V = (byte)Math.Abs(v - color.v);
			return color;
		}
		public readonly Hsv32Color Lerp(Hsv32Color to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
		public static Hsv32Color Lerp(Hsv32Color from, Hsv32Color to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
		public readonly Hsv32Color LerpUnclamped(Hsv32Color to, float val) => LerpUnclamped(this, to, val);
		public static Hsv32Color LerpUnclamped(Hsv32Color from, Hsv32Color to, float val)
		{
			from.H = (byte)Math.Round(from.h * (1.0f - val) + (to.h * val), MidpointRounding.AwayFromZero);
			from.S = (byte)Math.Round(from.s * (1.0f - val) + (to.s * val), MidpointRounding.AwayFromZero);
			from.V = (byte)Math.Round(from.v * (1.0f - val) + (to.v * val), MidpointRounding.AwayFromZero);
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
		public readonly int Max() => Math.Max(h, Math.Max(s, v));
		public readonly int Min() => Math.Min(h, Math.Min(s, v));
		public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"<{h},{s},{v}>";
		public static Hsv32Color operator +(Hsv32Color left, Hsv32Color right)
		{
			left.H += right.h;
			left.S += right.s;
			left.V += right.v;
			return left;
		}
		public static Hsv32Color operator +(Hsv32Color left, int right)
		{
			left.H32 += right;
			left.S32 += right;
			left.V32 += right;
			return left;
		}
		public static Hsv32Color operator -(Hsv32Color left, Hsv32Color right)
		{
			left.H -= right.h;
			left.S -= right.s;
			left.V -= right.v;
			return left;
		}
		public static Hsv32Color operator -(Hsv32Color left, int right)
		{
			left.H32 -= right;
			left.S32 -= right;
			left.V32 -= right;
			return left;
		}
		public static Hsv32Color operator *(Hsv32Color left, Hsv32Color right)
		{
			left.H *= right.h;
			left.S *= right.s;
			left.V *= right.v;
			return left;
		}
		public static Hsv32Color operator *(Hsv32Color left, int right)
		{
			left.H32 *= right;
			left.S32 *= right;
			left.V32 *= right;
			return left;
		}
		public static Hsv32Color operator /(Hsv32Color left, Hsv32Color right)
		{
			left.H /= right.h;
			left.S /= right.s;
			left.V /= right.v;
			return left;
		}
		public static Hsv32Color operator /(Hsv32Color left, int right)
		{
			left.H32 /= right;
			left.S32 /= right;
			left.V32 /= right;
			return left;
		}
		public static Hsv32Color operator -(Hsv32Color color)
		{
			color.h = (short)(360 - color.h);
			color.s = (byte)(100 - color.s);
			color.v = (byte)(100 - color.v);
			return color;
		}
		public static bool operator ==(Hsv32Color left, Hsv32Color right) => left.Equals(right);
		public static bool operator !=(Hsv32Color left, Hsv32Color right) => !left.Equals(right);
		public static implicit operator Vector4(Hsv32Color color) => new(color.h, color.s, color.v, float.NaN);
		public static implicit operator Hsv32Color(Vector4 color)
		{
			return new(
				(short)Math.Round(color.X, MidpointRounding.AwayFromZero),
				(byte)Math.Round(color.Y, MidpointRounding.AwayFromZero),
				(byte)Math.Round(color.Z, MidpointRounding.AwayFromZero));
		}
		public static explicit operator HsvColor(Hsv32Color color) => new(color.h / 360f, color.s / 100f, color.v / 100f);
		public static explicit operator Color32(Hsv32Color color) => (Color32)(Color)(HsvColor)color;
		public static explicit operator Hsl32Color(Hsv32Color color) => (Hsl32Color)(HslColor)(HsvColor)color;
		public static explicit operator Color(Hsv32Color color) => (Color)(HsvColor)color;
		public override readonly int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), v.GetHashCode());
	}
}
