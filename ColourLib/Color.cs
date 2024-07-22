using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct Color : IColor<Color>
    {
        private float r;
        private float g;
        private float b;
        private float a;
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
        public float A
        {
            get => a;
            set { a = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
        {
            get => i switch
            {
                0 => r,
                1 => g,
                2 => b,
                3 => a,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch(i)
                {
                    case 0:
                        R = value; break;
                    case 1:
                        G = value; break;
                    case 2:
                        B = value; break;
                    case 3:
                        A = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        public Color(float R, float G, float B, float A = 1f)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
        public H Convert<H>() where H : IColor<H>, new() => Convert<H>(this);
        public static H Convert<H>(Color color) where H : IColor<H>, new()
        {
            throw new NotImplementedException();
        }
        public string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{r}, {g}, {b}, {a}>";
        public Color Lerp(Color colorTo, float val) => LerpUnclamped(colorTo, Math.Clamp(val, 0f, 1f));
        public static Color Lerp(Color from, Color to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));

        public Vector4 LerpUnclamped(Color to, float val)
        {
            return new()
            {
                X = (r * (1.0f - val)) + (to.r * val),
                Y = (g * (1.0f - val)) + (to.g * val),
                Z = (b * (1.0f - val)) + (to.b * val),
                W = (a * (1.0f - val)) + (to.a * val)
            };
        }

        public static Vector4 LerpUnclamped(Color from, Color to, float val)
        {
            return new()
            {
                X = (from.r * (1.0f - val)) + (to.r * val),
                Y = (from.g * (1.0f - val)) + (to.g * val),
                Z = (from.b * (1.0f - val)) + (to.b * val),
                W = (from.a * (1.0f - val)) + (to.a * val)
            };
        }

        public static Color operator +(Color left, Color right)
        {
            right.R += left.R;
            right.G += left.G;
            right.B += left.B;
            right.A += left.A;
            return right;
        }
        public static Color operator -(Color left, Color right)
        {
            right.R -= left.R;
            right.G -= left.G;
            right.B -= left.B;
            right.A -= left.A;
            return right;
        }
        public static Color operator *(Color left, Color right)
        {
            right.R *= left.R;
            right.G *= left.G;
            right.B *= left.B;
            right.A *= left.A;
            return right;
        }
        public static Color operator /(Color left, Color right)
        {
            right.R /= left.R;
            right.G /= left.G;
            right.B /= left.B;
            right.A /= left.A;
            return right;
        }
        public static Color operator -(Color color)
        {
            color.R = 1f - color.R;
            color.G = 1f - color.G;
            color.B = 1f - color.B;
            color.A = 1f - color.A;
            return color;
        }

        public static implicit operator Vector4(Color color)
        {
            return new()
            {
                X = color.R,
                Y = color.G,
                Z = color.B,
                W = color.A
            };
        }

        public static implicit operator Color(Vector4 color)
        {
            return new()
            {
                R = color.X,
                G = color.Y,
                B = color.Z,
                A = color.W
            };
        }
    }
}
