using System.Numerics;

namespace ColourLib
{
	public struct HSV24Color : IColorB<HSV24Color>
	{
		private byte h;
		private byte s;
		private byte v;
		public byte H
		{
			get => h;
			set { h = value; }
		}
		public byte S
		{
			get => s;
			set { s = value; }
		}
		public byte V
		{
			get => v;
			set { v = value; }
		}
		public int H32
		{
			get => h;
			set => h = (byte)Math.Clamp(value, 0, 255);
		}
		public int S32
		{
			get => s;
			set => s = (byte)Math.Clamp(value, 0, 255);
		}
		public int V32
		{
			get => v;
			set => v = (byte)Math.Clamp(value, 0, 255);
		}
		public byte this[int i]
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
						H = value; break;
					case 1:
						S = value; break;
					case 2:
						V = value; break;
					default:
						throw new IndexOutOfRangeException();
				}
			}
		}
		public HSV24Color(byte h, byte s, byte v)
		{
			this.h = h;
			this.s = s;
			this.v = v;
		}
		public HSV24Color(int h, int s, int v)
		{
			H32 = h;
			S32 = s;
			V32 = v;
		}
		public HSV24Color(float h, float s, float v)
		{
			H32 = (int)Math.Round(h * 255f, MidpointRounding.AwayFromZero);
			S32 = (int)Math.Round(s * 255f, MidpointRounding.AwayFromZero);
			V32 = (int)Math.Round(v * 255f, MidpointRounding.AwayFromZero);
		}
		public bool Equals(HSV24Color color) => h == color.h && s == color.s && v == color.v;
		public override bool Equals(object? color) => color is HSV24Color c && color is not null && Equals(c);
		public static HSV24Color Difference(HSV24Color left, HSV24Color right)
		{
			left.H = (byte)Math.Abs(left.h - right.h);
			left.S = (byte)Math.Abs(left.s - right.s);
			left.V = (byte)Math.Abs(left.v - right.v);
			return left;
		}
		public HSV24Color Difference(HSV24Color color)
		{
			color.H = (byte)Math.Abs(h - color.h);
			color.S = (byte)Math.Abs(s - color.s);
			color.V = (byte)Math.Abs(v - color.v);
			return color;
		}
		public HSV24Color Lerp(HSV24Color to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
		public static HSV24Color Lerp(HSV24Color from, HSV24Color to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
		public HSV24Color LerpUnclamped(HSV24Color to, float val) => LerpUnclamped(this, to, val);
		public static HSV24Color LerpUnclamped(HSV24Color from, HSV24Color to, float val)
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
		public byte Max() => Math.Max(h, Math.Max(s, v));
		public byte Min() => Math.Min(h, Math.Min(s, v));
		public string ToString(string? format, IFormatProvider? formatProvider) => $"<{h},{s},{v}>";
		public static HSV24Color operator +(HSV24Color left, HSV24Color right)
		{
			left.H += right.h;
			left.S += right.s;
			left.V += right.v;
			return left;
		}
		public static HSV24Color operator +(HSV24Color left, byte right)
		{
			left.H += right;
			left.S += right;
			left.V += right;
			return left;
		}
		public static HSV24Color operator -(HSV24Color left, HSV24Color right)
		{
			left.H -= right.h;
			left.S -= right.s;
			left.V -= right.v;
			return left;
		}
		public static HSV24Color operator -(HSV24Color left, byte right)
		{
			left.H -= right;
			left.S -= right;
			left.V -= right;
			return left;
		}
		public static HSV24Color operator *(HSV24Color left, HSV24Color right)
		{
			left.H *= right.h;
			left.S *= right.s;
			left.V *= right.v;
			return left;
		}
		public static HSV24Color operator *(HSV24Color left, byte right)
		{
			left.H *= right;
			left.S *= right;
			left.V *= right;
			return left;
		}
		public static HSV24Color operator /(HSV24Color left, HSV24Color right)
		{
			left.H /= right.h;
			left.S /= right.s;
			left.V /= right.v;
			return left;
		}
		public static HSV24Color operator /(HSV24Color left, byte right)
		{
			left.H /= right;
			left.S /= right;
			left.V /= right;
			return left;
		}
		public static HSV24Color operator -(HSV24Color color)
		{
			color.h = (byte)(255 - color.h);
			color.s = (byte)(255 - color.s);
			color.v = (byte)(255 - color.v);
			return color;
		}
		public static bool operator ==(HSV24Color left, HSV24Color right) => left.Equals(right);
		public static bool operator !=(HSV24Color left, HSV24Color right) => !left.Equals(right);
		public static implicit operator Vector4(HSV24Color color) => new(color.h, color.s, color.v, float.NaN);
		public static implicit operator HSV24Color(Vector4 color)
		{
			return new(
				(byte)Math.Round(color.X, MidpointRounding.AwayFromZero),
				(byte)Math.Round(color.Y, MidpointRounding.AwayFromZero),
				(byte)Math.Round(color.Z, MidpointRounding.AwayFromZero));
		}
		public static explicit operator HSVColor(HSV24Color color) => new(color.h / 255f, color.s / 255f, color.v / 255f);
		public static explicit operator Color32(HSV24Color color) => (Color32)(Color)(HSVColor)color;   // Yuck
		public static explicit operator HSL24Color(HSV24Color color) => (HSL24Color)(HSLColor)(HSVColor)color;
		public override int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), v.GetHashCode());
	}
}
