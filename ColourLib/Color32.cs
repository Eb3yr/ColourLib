using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct Color32 : IColorB<Color32>
    {
        private byte r;
        private byte g;
        private byte b;
        private byte a;
        public byte R
        {
            get => r;
            set { r = value; }
        }
        public byte G
        {
            get => g;
            set { g = value; }
        }
        public byte B
        {
            get => b;
            set { b = value; }
        }
        public byte A
        {
            get => a;
            set { a = value; }
        }
        public byte this[int i]
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
                switch (i)
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
		public Color32(int r, int g, int b, int a = 255)
		{
			R = (byte)r;
			G = (byte)g;
			B = (byte)b;
			A = (byte)a;
		}
		public Color32(float r, float g, float b, float a = 1f)
		{
			R = (byte)Math.Round(r * 255f, MidpointRounding.AwayFromZero);
			G = (byte)Math.Round(g * 255f, MidpointRounding.AwayFromZero);
			B = (byte)Math.Round(b * 255f, MidpointRounding.AwayFromZero);
			A = (byte)Math.Round(a * 255f, MidpointRounding.AwayFromZero);
		}
		public byte Max() => Max(false);
		public byte Min() => Min(false);
		public byte Max(bool compareAlpha) => compareAlpha ? Math.Max(r, Math.Max(g, Math.Max(b, a))) : Math.Max(r, Math.Max(g, b));
		public byte Min(bool compareAlpha) => compareAlpha ? Math.Min(r, Math.Min(g, Math.Min(b, a))) : Math.Min(r, Math.Min(g, b));
		public Color32 Difference(Color32 color)
		{
			throw new NotImplementedException();
		}

		public static Color32 Difference(Color32 from, Color32 to)
		{
			throw new NotImplementedException();
		}

		public Color32 Lerp(Color32 to, float val)
		{
			throw new NotImplementedException();
		}

		public static Color32 Lerp(Color32 from, Color32 to, float val)
		{
			throw new NotImplementedException();
		}

		public Color32 LerpUnclamped(Color32 to, float val)
		{
			throw new NotImplementedException();
		}

		public static Color32 LerpUnclamped(Color32 from, Color32 to, float val)
		{
			throw new NotImplementedException();
		}

		public string ToString(string? format, IFormatProvider? formatProvider) => $"<{r}, {g}, {b}, {a}>";
		public bool Equals(Color32 other)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator +(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator -(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator *(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator /(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator +(Color32 left, float right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator -(Color32 left, float right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator *(Color32 left, float right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator /(Color32 left, float right)
		{
			throw new NotImplementedException();
		}

		public static Color32 operator -(Color32 color)
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(Color32 left, Color32 right)
		{
			throw new NotImplementedException();
		}

		public static implicit operator Vector4(Color32 color)
		{
			throw new NotImplementedException();
		}

		public static implicit operator Color32(Vector4 color)
		{
			throw new NotImplementedException();
		}
	}
}
