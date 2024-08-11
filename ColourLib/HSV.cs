using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        public bool Equals(HSVColor color) => H == color.H && S == color.S && V == color.V;
        public override bool Equals(object? color) => color is HSVColor c && color is not null && Equals(c);
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
            throw new NotImplementedException();
        }

        public static HSVColor operator -(HSVColor left, HSVColor right)
        {
            throw new NotImplementedException();
        }

        public static HSVColor operator *(HSVColor left, HSVColor right)
        {
            throw new NotImplementedException();
        }

        public static HSVColor operator /(HSVColor left, HSVColor right)
        {
            throw new NotImplementedException();
        }

        public static HSVColor operator -(HSVColor color)
        {
            throw new NotImplementedException();
        }
        public static bool operator ==(HSVColor left, HSVColor right) => left.Equals(right);
        public static bool operator !=(HSVColor left, HSVColor right) => !left.Equals(right);
        public static implicit operator Vector3(HSVColor color) => new(color.H, color.S, color.V);
        public static implicit operator Vector4(HSVColor color) => new(color.H, color.S, color.V, float.NaN);
        public static implicit operator HSVColor(Vector3 color) => new(color.X, color.Y, color.Z);
        public static implicit operator HSVColor(Vector4 color) => new(color.X, color.Y, color.Z);

		public static HSVColor operator +(HSVColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static HSVColor operator -(HSVColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static HSVColor operator *(HSVColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static HSVColor operator /(HSVColor left, float right)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode() => HashCode.Combine(H.GetHashCode(), S.GetHashCode(), V.GetHashCode());
    }
}
