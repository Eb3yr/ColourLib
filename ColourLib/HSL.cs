using System;
using System.Collections.Generic;
using System.Linq;
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
                0 => H,
                1 => S,
                2 => L,
                _ => throw new ArgumentOutOfRangeException()
            };
            set
            {
                switch (i)
                {
                    case 0:
                        H = value;
                        break;
                    case 1:
                        S = value;
                        break;
                    case 2:
                        L = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public HSLColor(float H, float S, float L)
        {
            this.H = H;
            this.S = S;
            this.L = L;
        }
        public T Convert<T>()
        {
            return Convert<T>(this);
        }
        public static T Convert<T>(HSLColor c)
        {
            throw new NotImplementedException();
        }
        public HSLColor Lerp(HSLColor to, float val)
        {
            return Lerp(this, to, val);
        }
        public static HSLColor Lerp(HSLColor from, HSLColor to, float val)
        {
            throw new NotImplementedException();
        }
        public HSLColor LerpUnclamped(HSLColor to, float val)
        {
            return LerpUnclamped(this, to, val);
        }
        public static HSLColor LerpUnclamped(HSLColor from, HSLColor to, float val)
        {
            throw new NotImplementedException();
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
    }
}
