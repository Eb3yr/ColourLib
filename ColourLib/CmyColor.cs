using System.Numerics;
using System.Text.Json.Serialization;

namespace ColourLib
{
	public struct CmyColor : IColorF<CmyColor>, IEquatable<CmyColor>
	{
		[JsonInclude]
		private float c;
		[JsonInclude]
		private float m;
		[JsonInclude]
		private float y;
		[JsonIgnore]
		public float C
		{
			readonly get => c;
			set => c = float.Clamp(value, 0f, 1f);
		}
		[JsonIgnore]
		public float M
		{
			readonly get => m;
			set => m = float.Clamp(value, 0f, 1f);
		}
		[JsonIgnore]
		public float Y
		{
			readonly get => y;
			set => y = float.Clamp(value, 0f, 1f);
		}

		public float this[int i]
		{
			readonly get => i switch
			{
				0 => c,
				1 => m,
				2 => m,
				_ => throw new IndexOutOfRangeException()
			};
			set
			{
				switch(i)
				{
					case 0:
						C = value; break;
					case 1:
						M = value; break;
					case 2:
						Y = value; break;
					default:
						throw new IndexOutOfRangeException();
				}
			}
		}

		public CmyColor(float C, float M, float Y)
		{
			this.C = C;
			this.M = M;
			this.Y = Y;
		}

		public CmyColor(CmyColor color) : this(color.c, color.m, color.y) { }

		public readonly bool Equals(CmyColor color) => color.c == c && color.m == m & color.y == y;

		public override readonly bool Equals(object? color) => color is CmyColor c && color is not null && Equals(c);

		public readonly CmyColor Difference(CmyColor color) => Difference(this, color);

		public static CmyColor Difference(CmyColor left, CmyColor right)
		{
			right.c = Math.Abs(right.c - left.c);
			right.m = Math.Abs(right.m - left.m);
			right.y = Math.Abs(right.y - left.y);
			return right;
		}

		public static bool InverseLerp(Vector4 left, Vector4 right, Vector4 val, out float lerpVal)
		{
			lerpVal = 0f;
			Vector4 ratioVec = (val - right) / (left - val);
			float? previous = null;
			for (int i = 0; i < 4; i++)
			{
				if (float.IsNaN(ratioVec[i]))
					continue;

				if (previous is null)
				{
					previous = ratioVec[i];
					lerpVal = (val[i] - left[i]) / (right[i] - left[i]);
				}
				else if (ratioVec[i] != previous)
				{
					lerpVal = 0f;
					return false;
				}
			}
			if (float.IsInfinity(lerpVal))
			{
				lerpVal = 0f;
				return false;
			}
			if (lerpVal > 1f || lerpVal < 0f)
			{
				lerpVal = 0f;
				return false;
			}
			return true;
		}

		public readonly CmyColor Lerp(CmyColor to, float val) => LerpUnclamped(this, to, Math.Clamp(val, 0f, 1f));

		public static CmyColor Lerp(CmyColor from, CmyColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));

		public readonly CmyColor LerpUnclamped(CmyColor to, float val) => LerpUnclamped(this, to, val);

		public static CmyColor LerpUnclamped(CmyColor from, CmyColor to, float val)
		{
			to.C = (from.c * (1.0f - val)) + (to.c * val);
			to.M = (from.m * (1.0f - val)) + (to.m * val);
			to.Y = (from.y * (1.0f - val)) + (to.y * val);
			return to;
		}

		public readonly float Max() => Math.Max(c, Math.Max(m, y));

		public readonly float Min() => Math.Min(c, Math.Min(m, y));

		public readonly string ToString(string? format, IFormatProvider? formatProvider) => $"<{c},{m},{y}>";

		public static CmyColor operator +(CmyColor left, float right)
		{
			left.C += right;
			left.M += right;
			left.Y += right;
			return left;
		}

		public static CmyColor operator +(CmyColor left, CmyColor right)
		{
			right.C += left.c;
			right.M += left.m;
			right.Y += left.y;
			return right;
		}

		public static CmyColor operator -(CmyColor color)
		{
			color.c = 1f - color.c;
			color.m = 1f - color.m;
			color.y = 1f - color.y;
			return color;
		}

		public static CmyColor operator -(CmyColor left, float right)
		{
			left.C -= right;
			left.M -= right;
			left.Y -= right;
			return left;
		}

		public static CmyColor operator -(CmyColor left, CmyColor right)
		{
			left.C -= right.c;
			left.M -= right.m;
			left.Y -= right.y;
			return left;
		}

		public static CmyColor operator *(CmyColor left, float right)
		{
			left.C *= right;
			left.M *= right;
			left.Y *= right;
			return left;
		}

		public static CmyColor operator *(CmyColor left, CmyColor right)
		{
			left.C *= right.c;
			left.M *= right.m;
			left.Y *= right.y;
			return left;
		}

		public static CmyColor operator /(CmyColor left, float right)
		{
			left.C /= right;
			left.M /= right;
			left.Y /= right;
			return left;
		}

		public static CmyColor operator /(CmyColor left, CmyColor right)
		{
			left.C /= right.c;
			left.M /= right.m;
			left.Y /= right.y;
			return left;
		}

		public static bool operator ==(CmyColor left, CmyColor right) => left.Equals(right);

		public static bool operator !=(CmyColor left, CmyColor right) => !left.Equals(right);

		public static implicit operator Vector3(CmyColor color) => new(color.c, color.m, color.y);

		public static implicit operator Vector4(CmyColor color) => new(color.c, color.m, color.y, float.NaN);

		public static implicit operator CmyColor(Vector3 color) => new(color.X, color.Y, color.Z);

		public static implicit operator CmyColor(Vector4 color) => new(color.X, color.Y, color.Z);

		public static explicit operator Color(CmyColor color) => new(1f - color.c, 1f - color.m, 1f - color.y);

		public static explicit operator CmyColor(Color color) => new(1f - color.R, 1f - color.G, 1f - color.B);

		public readonly override int GetHashCode() => HashCode.Combine(c.GetHashCode(), m.GetHashCode(), y.GetHashCode());
	}
}
