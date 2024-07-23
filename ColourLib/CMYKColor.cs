using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{c}, {m}, {y}, {k}>";
        public CMYKColor Lerp(CMYKColor colorTo, float val) => LerpUnclamped(colorTo, Math.Clamp(val, 0f, 1f));
        public static CMYKColor Lerp(CMYKColor from, CMYKColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));

        public Vector4 LerpUnclamped(CMYKColor to, float val)
        {
            return new()
            {
                X = (c * (1.0f - val)) + (to.c * val),
                Y = (m * (1.0f - val)) + (to.m * val),
                Z = (y * (1.0f - val)) + (to.y * val),
                W = (k * (1.0f - val)) + (to.k * val)
            };
        }

        public static Vector4 LerpUnclamped(CMYKColor from, CMYKColor to, float val)
        {
            return new()
            {
                X = (from.c * (1.0f - val)) + (to.c * val),
                Y = (from.m * (1.0f - val)) + (to.m * val),
                Z = (from.y * (1.0f - val)) + (to.y * val),
                W = (from.k * (1.0f - val)) + (to.k * val)
            };
        }

        public bool Equals(CMYKColor other)
        {
            throw new NotImplementedException();
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
    }
}
