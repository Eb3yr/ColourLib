using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct HSVColor : IColor<HSVColor>
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
        public H Convert<H>() where H : IColor<H>, new()
        {
            return Convert<H>(this);
        }
        public static H Convert<H>(HSVColor HSVColor) where H : IColor<H>, new()
        {
            throw new NotImplementedException();
        }
        public string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{h}, {s}, {v}>";
        public HSVColor Lerp(HSVColor colorTo, float val) => LerpUnclamped(colorTo, Math.Clamp(val, 0f, 1f));
        public static HSVColor Lerp(HSVColor from, HSVColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));

        public Vector4 LerpUnclamped(HSVColor to, float val)
        {
            return new()
            {
                X = (h * (1.0f - val)) + (to.h * val),
                Y = (s * (1.0f - val)) + (to.s * val),
                Z = (v * (1.0f - val)) + (to.v * val),
                W = float.NaN
            };
        }

        public static Vector4 LerpUnclamped(HSVColor from, HSVColor to, float val)
        {
            return new()
            {
                X = (from.h * (1.0f - val)) + (to.h * val),
                Y = (from.s * (1.0f - val)) + (to.s * val),
                Z = (from.v * (1.0f - val)) + (to.v * val),
                W = float.NaN
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

        public static implicit operator Vector4(HSVColor color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator HSVColor(Vector4 color)
        {
            throw new NotImplementedException();
        }
    }
}
