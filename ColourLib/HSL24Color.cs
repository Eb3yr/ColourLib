using System.Numerics;

namespace ColourLib
{
	public struct Hsl24Color : IColorB<Hsl24Color>
	{
		private byte h;
		private byte s;
		private byte l;
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
		public byte L
		{
			get => l;
			set { l = value; }
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
		public int L32
		{
			get => l;
			set => l = (byte)Math.Clamp(value, 0, 255);
		}
		public byte this[int i]
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
		public Hsl24Color(byte h, byte s, byte l)
		{
			this.h = h;
			this.s = s;
			this.l = l;
		}
		public Hsl24Color(int h, int s, int l)
		{
			H32 = h;
			S32 = s;
			L32 = l;
		}
		public Hsl24Color(float h, float s, float l)
		{
			H32 = (int)Math.Round(h * 255f, MidpointRounding.AwayFromZero);
			S32 = (int)Math.Round(s * 255f, MidpointRounding.AwayFromZero);
			L32 = (int)Math.Round(l * 255f, MidpointRounding.AwayFromZero);
		}
		public bool Equals(Hsl24Color color) => h == color.h && s == color.s && l == color.l;
		public override bool Equals(object? color) => color is Hsl24Color c && color is not null && Equals(c);
		public static Hsl24Color Difference(Hsl24Color left, Hsl24Color right)
		{
			left.H = (byte)Math.Abs(left.h - right.h);
			left.S = (byte)Math.Abs(left.s - right.s);
			left.L = (byte)Math.Abs(left.l - right.l);
			return left;
		}
		public Hsl24Color Difference(Hsl24Color color)
		{
			color.H = (byte)Math.Abs(h - color.h);
			color.S = (byte)Math.Abs(s - color.s);
			color.L = (byte)Math.Abs(l - color.l);
			return color;
		}
		public Hsl24Color Lerp(Hsl24Color to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));
		public static Hsl24Color Lerp(Hsl24Color from, Hsl24Color to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));
		public Hsl24Color LerpUnclamped(Hsl24Color to, float val) => LerpUnclamped(this, to, val);
		public static Hsl24Color LerpUnclamped(Hsl24Color from, Hsl24Color to, float val)
		{
			from.H = (byte)Math.Round(from.h * (1.0f - val) + (to.h * val), MidpointRounding.AwayFromZero);
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
		public byte Max() => Math.Max(h, Math.Max(s, l));
		public byte Min() => Math.Min(h, Math.Min(s, l));
		public string ToString(string? format, IFormatProvider? formatProvider) => $"<{h},{s},{l}>";
		public static Hsl24Color operator +(Hsl24Color left, Hsl24Color right)
		{
			left.H += right.h;
			left.S += right.s;
			left.L += right.l;
			return left;
		}
		public static Hsl24Color operator +(Hsl24Color left, byte right)
		{
			left.H += right;
			left.S += right;
			left.L += right;
			return left;
		}
		public static Hsl24Color operator -(Hsl24Color left, Hsl24Color right)
		{
			left.H -= right.h;
			left.S -= right.s;
			left.L -= right.l;
			return left;
		}
		public static Hsl24Color operator -(Hsl24Color left, byte right)
		{
			left.H -= right;
			left.S -= right;
			left.L -= right;
			return left;
		}
		public static Hsl24Color operator *(Hsl24Color left, Hsl24Color right)
		{
			left.H *= right.h;
			left.S *= right.s;
			left.L *= right.l;
			return left;
		}
		public static Hsl24Color operator *(Hsl24Color left, byte right)
		{
			left.H *= right;
			left.S *= right;
			left.L *= right;
			return left;
		}
		public static Hsl24Color operator /(Hsl24Color left, Hsl24Color right)
		{
			left.H /= right.h;
			left.S /= right.s;
			left.L /= right.l;
			return left;
		}
		public static Hsl24Color operator /(Hsl24Color left, byte right)
		{
			left.H /= right;
			left.S /= right;
			left.L /= right;
			return left;
		}
		public static Hsl24Color operator -(Hsl24Color color)
		{
			color.h = (byte)(255 - color.h);
			color.s = (byte)(255 - color.s);
			color.l = (byte)(255 - color.l);
			return color;
		}
		public static bool operator ==(Hsl24Color left, Hsl24Color right) => left.Equals(right);
		public static bool operator !=(Hsl24Color left, Hsl24Color right) => !left.Equals(right);
		public static implicit operator Vector4(Hsl24Color color) => new(color.h, color.s, color.l, float.NaN);
		public static implicit operator Hsl24Color(Vector4 color)
		{
			return new(
				(byte)Math.Round(color.X, MidpointRounding.AwayFromZero),
				(byte)Math.Round(color.Y, MidpointRounding.AwayFromZero),
				(byte)Math.Round(color.Z, MidpointRounding.AwayFromZero));
		}
		public static explicit operator HslColor(Hsl24Color color) => new(color.h / 255f, color.s / 255f, color.l / 255f);
		public static explicit operator Color32(Hsl24Color color) => (Color32)(Color)(HslColor)color;   // Yuck
		public static explicit operator Hsv24Color(Hsl24Color color) => (Hsv24Color)(HsvColor)(HslColor)color;
		public override int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), l.GetHashCode());
	}
}
