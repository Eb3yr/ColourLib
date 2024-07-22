using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct HSLColor : IColor<HSLColor>
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
        public H Convert<H>() where H : IColor<H>, new()
        {
            return Convert<H>(this);
        }
        public static H Convert<H>(HSLColor c) where H : IColor<H>, new()
        {
            throw new NotImplementedException();
        }
        public string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{h}, {s}, {l}>";

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
    }
}
