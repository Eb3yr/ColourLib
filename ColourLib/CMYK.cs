using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct CMYK : IColor<CMYK>, IHaveFourFields
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
        public CMYK(float C, float M, float Y, float K = 0f)
        {
            this.C = C;
            this.M = M;
            this.Y = Y;
            this.K = K;
        }

        public H Convert<H>() where H : IColor<H>, new()
        {
            throw new NotImplementedException();
        }

        public static H Convert<H>(CMYK color) where H : IColor<H>, new()
        {
            throw new NotImplementedException();
        }
        public string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{c}, {m}, {y}, {k}>";
        public static CMYK operator +(CMYK left, CMYK right)
        {
            throw new NotImplementedException();
        }

        public static CMYK operator -(CMYK left, CMYK right)
        {
            throw new NotImplementedException();
        }

        public static CMYK operator *(CMYK left, CMYK right)
        {
            throw new NotImplementedException();
        }

        public static CMYK operator /(CMYK left, CMYK right)
        {
            throw new NotImplementedException();
        }

        public static CMYK operator -(CMYK color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Vector4(CMYK color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator CMYK(Vector4 color)
        {
            throw new NotImplementedException();
        }
    }
}
