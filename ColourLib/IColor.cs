using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public interface IColor<T> : IFormattable where T : IColor<T>, new()
    {
        public abstract float this[int i] { get; set; }
        public abstract H Convert<H>() where H : IColor<H>, new();
        public static abstract H Convert<H>(T color) where H : IColor<H>, new();
        //public abstract bool TryConvert<H>(out H color);
        //public static abstract bool TryConvert<H>(T inColor, out H outColor);
        public abstract T Lerp(T to, float val);
        public static abstract T Lerp(T from, T to, float val);
        public abstract Vector4 LerpUnclamped(T to, float val);
        public static abstract Vector4 LerpUnclamped(T from, T to, float val);
        //public static abstract bool InverseLerp(T left, T right, T val, out float lerpVal); // Returns false if val is not along the line between left and right. 
        public static abstract T operator +(T left, T right);
        public static abstract T operator -(T left, T right);
        public static abstract T operator *(T left, T right);
        public static abstract T operator /(T left, T right);
        public static virtual T operator +(T color) => color;
        public static abstract T operator -(T color);   // inverts colour values. For example Color.R has a domain [0f, 1f], therefore -Color will have a red component of (1f - Color.R) 
        public static abstract implicit operator Vector4(T color);
        public static abstract implicit operator T(Vector4 color);
    }
}