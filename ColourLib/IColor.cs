using System.Numerics;

namespace ColourLib
{
    public interface IColorB<T> : IColor<T> where T : IColorB<T>
    {
        public abstract byte this[int i] { get; set; }
		public abstract byte Max();
		public abstract byte Min();
		public static abstract T operator +(T left, byte right);
		public static abstract T operator -(T left, byte right);
		public static abstract T operator *(T left, byte right);
		public static abstract T operator /(T left, byte right);
	}
    public interface IColorF<T> : IColor<T> where T : IColorF<T>
    {
        public abstract float this[int i] { get; set; }
		public abstract float Max();
		public abstract float Min();
        public static abstract T operator +(T left, float right);
		public static abstract T operator -(T left, float right);
		public static abstract T operator *(T left, float right);
		public static abstract T operator /(T left, float right);
	}
    public interface IColor<T> : IFormattable, IEquatable<T> where T : IColor<T>
    {
        public abstract T Difference(T color);
        public static abstract T Difference(T left, T right);
        public abstract T Lerp(T to, float val);
        public static abstract T Lerp(T from, T to, float val);
        public abstract T LerpUnclamped(T to, float val);
        public static abstract T LerpUnclamped(T from, T to, float val);
        //public static abstract bool InverseLerp(T left, T right, T val, out float lerpVal); // Returns false if val is not along the line between left and right. 
        public static abstract T operator +(T left, T right);
        public static abstract T operator -(T left, T right);
        public static abstract T operator *(T left, T right);
        public static abstract T operator /(T left, T right);
		
		public static virtual T operator +(T color) => color;   // I don't think this is being inherited by implementing structs. Classes would, but not structs. Sort out later.
        public static abstract T operator -(T color);   // inverts colour values. For example Color.R has a domain [0f, 1f], therefore -Color will have a red component of (1f - Color.R) 
        public static abstract bool operator ==(T left, T right);
        public static abstract bool operator !=(T left, T right);
        public static abstract implicit operator Vector4(T color);
        public static abstract implicit operator T(Vector4 color);
    }
}