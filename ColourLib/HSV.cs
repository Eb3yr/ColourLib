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
        public float this[int i] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private float h;
        private float s;
        private float v;
        //public HSVColor(float H, float S, float V)
        //{
        //    this.H = Math.Clamp(H, 0f, 1f);
        //    this.S = Math.Clamp(S, 0f, 1f);
        //    this.V = Math.Clamp(V, 0f, 1f);
        //}

        public H Convert<H>() where H : IColor<H>, new()
        {
            return Convert<H>(this);
        }
        public static H Convert<H>(HSVColor HSVColor) where H : IColor<H>, new()
        {
            throw new NotImplementedException();
        }
        public string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{h}, {s}, {v}>";

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
