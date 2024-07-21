using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct Color : IColor<Color>
    {
        private float a;
        private float r;
        private float g;
        private float b;
        public float A
        {
            get => a;
            set { a = Math.Clamp(value, 0f, 1f); }
        }
        public float R
        {
        get => r;
        set { r = Math.Clamp(value, 0f, 1f); }
        }
        public float G
        {
        get => g;
        set { g = Math.Clamp(value, 0f, 1f); }
        }
        public float B
        {
        get => b;
        set { b = Math.Clamp(value, 0f, 1f); }
        }
        public Color(float R, float G, float B, float A = 1f)
        {
            this.A = A;
            this.R = R;
            this.G = G;
            this.B = B;
        }
        public H Convert<H>()
        {
            return Convert<H>(this);
        }
        public static H Convert<H>(Color color)
        {
            throw new NotImplementedException();
        }
        public Color Lerp(Color colorTo, float val)
        {
            return Lerp(this, colorTo, val);
        }
        public static Color Lerp(Color colorFrom, Color colorTo, float val)
        {
            throw new NotImplementedException();
        }
        public static Color operator+(Color val)
        {
            return default;
        }
    }
}
