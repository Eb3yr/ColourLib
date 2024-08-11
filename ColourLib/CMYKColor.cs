using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct CMYKColor : IColorF<CMYKColor>
    {
        private float c;
        private float m;
        private float y;
        private float k;
        public float C
        {
            get => c;
            set { c = Math.Clamp(value, 0f, 1f); }
        }
        public float M
            {
            get => m;
            set { m = Math.Clamp(value, 0f, 1f); }
            }
        public float Y
            {
            get => y;
            set { y = Math.Clamp(value, 0f, 1f); }
            }
        public float K
        {
            get => k;
            set { k = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
        {
            get => i switch
            {
                0 => c,
                1 => m,
                2 => y,
                3 => k,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch(i)
                {
                    case 0:
                        C = value; break;
                    case 1:
                        M = value; break;
                    case 2:
                        Y = value; break;
                    case 3:
                        K = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        public CMYKColor(float C, float M, float Y, float K = 0f)
        {
            this.C = C;
            this.M = M;
            this.Y = Y;
            this.K = K;
        }
        public bool Equals(CMYKColor color) => C == color.C && M == color.M && Y == color.Y && K == color.K;
        public override bool Equals(object? color) => color is CMYKColor c && color is not null && Equals(c);
        public float Max() => Math.Max(c, Math.Max(m, Math.Max(y, k)));
		public float Min() => Math.Min(c, Math.Min(m, Math.Min(y, k)));
		public CMYKColor Difference(CMYKColor color)
        {
            throw new NotImplementedException();
        }
        public static CMYKColor Difference(CMYKColor left, CMYKColor right)
        {
            throw new NotImplementedException();
        }
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{c}, {m}, {y}, {k}>";
        public CMYKColor Lerp(CMYKColor colorTo, float val) => LerpUnclamped(colorTo, Math.Clamp(val, 0f, 1f));
        public static CMYKColor Lerp(CMYKColor from, CMYKColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));

        public CMYKColor LerpUnclamped(CMYKColor to, float val) => LerpUnclamped(this, to, val);

        public static CMYKColor LerpUnclamped(CMYKColor from, CMYKColor to, float val)
        {
            return new()
            {
                C = (from.c * (1.0f - val)) + (to.c * val),
                M = (from.m * (1.0f - val)) + (to.m * val),
                Y = (from.y * (1.0f - val)) + (to.y * val),
                K = (from.k * (1.0f - val)) + (to.k * val)
            };
        }

        public static CMYKColor operator +(CMYKColor left, CMYKColor right)
        {
            throw new NotImplementedException();
        }

        public static CMYKColor operator -(CMYKColor left, CMYKColor right)
        {
            throw new NotImplementedException();
        }

        public static CMYKColor operator *(CMYKColor left, CMYKColor right)
        {
            throw new NotImplementedException();
        }

        public static CMYKColor operator /(CMYKColor left, CMYKColor right)
        {
            throw new NotImplementedException();
        }

        public static CMYKColor operator -(CMYKColor color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Vector4(CMYKColor color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator CMYKColor(Vector4 color)
        {
            throw new NotImplementedException();
        }

		public static CMYKColor operator +(CMYKColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static CMYKColor operator -(CMYKColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static CMYKColor operator *(CMYKColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static CMYKColor operator /(CMYKColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(CMYKColor left, CMYKColor right)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(CMYKColor left, CMYKColor right)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode() => HashCode.Combine(C.GetHashCode(), M.GetHashCode(), Y.GetHashCode(), K.GetHashCode());
    }
}
