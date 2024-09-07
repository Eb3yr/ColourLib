using System.Numerics;

namespace ColourLib
{
	// Unfortunately, default implementations of methods are not inherited by structs. Some methods are duplicated across different color structs, so should an issue arise all will need updating.
	public interface IColorB<T> : IColor<T> where T : IColorB<T>
    {
        public abstract int this[int i] { get; set; }
        public abstract int Max();
		public abstract int Min();
		public static abstract T operator +(T left, int right);
		public static abstract T operator -(T left, int right);
		public static abstract T operator *(T left, int right);
		public static abstract T operator /(T left, int right);
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
        public static abstract bool InverseLerp(Vector4 left, Vector4 right, Vector4 val, out float lerpVal); // Returns false if val is not along the line between left and right. Vector4 circumvents clamping behaviour of color channels
        public static abstract T operator +(T left, T right);
        public static abstract T operator -(T left, T right);
        public static abstract T operator *(T left, T right);
        public static abstract T operator /(T left, T right);
        public static abstract T operator -(T color);
        public static abstract bool operator ==(T left, T right);
        public static abstract bool operator !=(T left, T right);
        public static abstract implicit operator Vector4(T color);
        public static abstract implicit operator T(Vector4 color);
    }
}