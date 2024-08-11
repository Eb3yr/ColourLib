using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
	public struct HSL24Color : IColorB<HSL24Color>
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
		public HSL24Color(byte h, byte s, byte l)
		{
			this.h = h;
			this.s = s;
			this.l = l;
		}
		public HSL24Color(int h, int s, int l)
		{
			H32 = h;
			S32 = s;
			L32 = l;
		}
		public bool Equals(HSL24Color color) => h == color.h && s == color.s && l == color.l;
		public override bool Equals(object? color) => color is HSL24Color c && color is not null && Equals(c);
		public static HSL24Color Difference(HSL24Color from, HSL24Color to)
		{
			throw new NotImplementedException();
		}
		public HSL24Color Difference(HSL24Color color)
		{
			throw new NotImplementedException();
		}
		public HSL24Color Lerp(HSL24Color to, float val)
		{
			throw new NotImplementedException();
		}
		public static HSL24Color Lerp(HSL24Color from, HSL24Color to, float val)
		{
			throw new NotImplementedException();
		}
		public HSL24Color LerpUnclamped(HSL24Color to, float val)
		{
			throw new NotImplementedException();
		}
		public static HSL24Color LerpUnclamped(HSL24Color from, HSL24Color to, float val)
		{
			throw new NotImplementedException();
		}
		public byte Max()
		{
			throw new NotImplementedException();
		}
		public byte Min()
		{
			throw new NotImplementedException();
		}
		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			throw new NotImplementedException();
		}
		public static HSL24Color operator +(HSL24Color left, HSL24Color right)
		{
			throw new NotImplementedException();
		}
		public static HSL24Color operator +(HSL24Color left, float right)
		{
			throw new NotImplementedException();
		}
		public static HSL24Color operator -(HSL24Color color)
		{
			throw new NotImplementedException();
		}
		public static HSL24Color operator -(HSL24Color left, HSL24Color right)
		{
			throw new NotImplementedException();
		}
		public static HSL24Color operator -(HSL24Color left, float right)
		{
			throw new NotImplementedException();
		}

		public static HSL24Color operator *(HSL24Color left, HSL24Color right)
		{
			throw new NotImplementedException();
		}

		public static HSL24Color operator *(HSL24Color left, float right)
		{
			throw new NotImplementedException();
		}

		public static HSL24Color operator /(HSL24Color left, HSL24Color right)
		{
			throw new NotImplementedException();
		}

		public static HSL24Color operator /(HSL24Color left, float right)
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(HSL24Color left, HSL24Color right)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(HSL24Color left, HSL24Color right)
		{
			throw new NotImplementedException();
		}

		public static implicit operator Vector4(HSL24Color color)
		{
			throw new NotImplementedException();
		}

		public static implicit operator HSL24Color(Vector4 color)
		{
			throw new NotImplementedException();
		}
		public static explicit operator HSLColor(HSL24Color color) => new(color.h / 255f, color.s / 255f, color.l / 255f);
		public override int GetHashCode() => HashCode.Combine(h.GetHashCode(), s.GetHashCode(), l.GetHashCode());
	}
}
