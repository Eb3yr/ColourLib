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
    public interface IHaveFourFields { }
    public interface IColor<T> : IFormattable where T : IColor<T>, new() 
    {
        public abstract float this[int i] { get; set; }
        public abstract H Convert<H>() where H : IColor<H>, new();
        public static abstract H Convert<H>(T color) where H : IColor<H>, new();
        //public abstract bool TryConvert<H>(out H color);
        //public static abstract bool TryConvert<H>(T inColor, out H outColor);
        public T Lerp(T colorTo, float val) => LerpUnclamped(colorTo, Math.Clamp(val, 0f, 1f));
        public static T Lerp(T colorFrom, T colorTo, float val) => LerpUnclamped(colorFrom, colorTo, Math.Clamp(val, 0f, 1f));
        public Vector4 LerpUnclamped(T colorTo, float val)
        {
            return new()
            {
                X = (this[0] * (1.0f - val)) + (colorTo[0] * val),
                Y = (this[1] * (1.0f - val)) + (colorTo[1] * val),
                Z = (this[2] * (1.0f - val)) + (colorTo[2] * val),
                W = (colorTo is IHaveFourFields) ? (this[3] * (1.0f - val)) + (colorTo[3] * val) : float.NaN
            };
        }
        public static Vector4 LerpUnclamped(T colorFrom, T colorTo, float val)
        {
            return new()
            {
                X = (colorFrom[0] * (1.0f - val)) + (colorTo[0] * val),
                Y = (colorFrom[1] * (1.0f - val)) + (colorTo[1] * val),
                Z = (colorFrom[2] * (1.0f - val)) + (colorTo[2] * val),
                W = (colorTo is IHaveFourFields) ? (colorFrom[3] * (1.0f - val)) + (colorTo[3] * val) : float.NaN
            };
        }
        //public static abstract T InverseLerp(T left, T right, T val);  // I'm not sure about this one. Val must be on the line between the two, and even then I'm not sure about its utility. 
        // LerpMap function that returns a new Func that'll apply the map observed by the original LerpMap() args that resulted in the Func's instantiation?
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