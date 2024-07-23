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
    public interface IColorB<T> : IColor<T> where T : IColor<T>
    {
        public abstract byte this[int i] { get; set; }
    }
    public interface IColorF<T> : IColor<T> where T : IColor<T>
    {
        public abstract float this[int i] { get; set; }
    }
    public interface IColor<T> : IFormattable, IEquatable<T> where T : IColor<T>
    {
        public abstract T Difference(T color);
        public static abstract T Difference(T from, T to);
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
        public static abstract bool operator ==(T left, T right);
        public static abstract bool operator !=(T left, T right);
        public static abstract implicit operator Vector4(T color);
        public static abstract implicit operator T(Vector4 color);
    }
}