using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public interface IColor<T> where T: IColor<T>
    {
        public float this[int i] { get; set; }
        public abstract H Convert<H>();
        public static abstract H Convert<H>(T color);
        public abstract T Lerp(T colorTo, float val);
        public static abstract T Lerp(T colorFrom, T colorTo, float val);
        public abstract T LerpUnclamped(T colorTo, float val);
        public static abstract T LerpUnclamped(T colorFrom, T colorTo, float val);  // I think the best way to implement this would be to return a Vector3. Since Vector3s and 4s will be implicitly castable to and from the colours, it allows the out of range behaviour as long as it isn't cast back to a colour. 
        //public static abstract T InverseLerp(T left, T right, T val);  // I'm not sure about this one. Val must be on the line between the two, and even then I'm not sure about its utility. 
        // LerpMap function that returns a new Func that'll apply the map observed by the original LerpMap() args that resulted in the Func's instantiation?
        public static abstract T operator +(T left, T right);
        public static abstract T operator -(T left, T right);
        public static abstract T operator *(T left, T right);
        public static abstract T operator /(T left, T right);
        public static virtual T operator +(T color) => color;
        public static abstract T operator -(T color);   // inverts colour values. For example Color.R has a domain [0f, 1f], therefore -Color will have a red component of (1f - Color.R) 
    }
}