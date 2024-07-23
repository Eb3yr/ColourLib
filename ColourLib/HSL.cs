using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct HSLColor : IColorF<HSLColor>
    {
        private float h;
        private float s;
        private float l;
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
        public float L
        {
            get => l;
            set { l = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
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
        public HSLColor(float H, float S, float L)
        {
            this.H = H;
            this.S = S;
            this.L = L;
        }
        public bool Equals(HSLColor color) => H == color.H && S == color.S && L == color.L;
        public override bool Equals(object? color) => color is HSLColor c && color is not null && Equals(c);
        public HSLColor Difference(HSLColor color) => Difference(this, color);
        public static HSLColor Difference(HSLColor left, HSLColor right)
        {
            right.H = Math.Abs(right.H - left.H);
            right.S = Math.Abs(right.S - left.S);
            right.L = Math.Abs(right.L - left.L);
            return right;
        }
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{h}, {s}, {l}>";
        public HSLColor Lerp(HSLColor colorTo, float val) => LerpUnclamped(colorTo, Math.Clamp(val, 0f, 1f));
        public static HSLColor Lerp(HSLColor from, HSLColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));

        public Vector4 LerpUnclamped(HSLColor to, float val)
        {
            return new()
            {
                X = (h * (1.0f - val)) + (to.h * val),
                Y = (s * (1.0f - val)) + (to.s * val),
                Z = (l * (1.0f - val)) + (to.l * val),
                W = float.NaN
            };
        }

        public static Vector4 LerpUnclamped(HSLColor from, HSLColor to, float val)
        {
            return new()
            {
                X = (from.h * (1.0f - val)) + (to.h * val),
                Y = (from.s * (1.0f - val)) + (to.s * val),
                Z = (from.l * (1.0f - val)) + (to.l * val),
                W = float.NaN
            };
        }
        // How do I handle operations on HSL and HSV colour space? For that matter, how do I even handle it in RGB colour space? Why did I decide to do this again?
        public static HSLColor operator +(HSLColor left, HSLColor right)
        {
            throw new NotImplementedException();
        }
        public static HSLColor operator -(HSLColor left, HSLColor right)
        {
            throw new NotImplementedException();
        }
        public static HSLColor operator *(HSLColor left, HSLColor right)
        {
            throw new NotImplementedException();
        }
        public static HSLColor operator /(HSLColor left, HSLColor right)
        {
            throw new NotImplementedException();
        }
        public static HSLColor operator -(HSLColor color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Vector4(HSLColor color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator HSLColor(Vector4 color)
        {
            throw new NotImplementedException();
        }
        public override int GetHashCode() => HashCode.Combine(H.GetHashCode(), S.GetHashCode(), L.GetHashCode());
    }
}
