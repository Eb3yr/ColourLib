using System.Numerics;
using System.Text.Json.Serialization;

namespace ColourLib
{
    public struct CmykColor : IColorF<CmykColor>
    {
		[JsonInclude]
		private float c;
		[JsonInclude]
		private float m;
		[JsonInclude]
		private float y;
		[JsonInclude]
		private float k;
		[JsonIgnore]
		public float C
        {
			readonly get => c;
            set { c = Math.Clamp(value, 0f, 1f); }
        }
		[JsonIgnore]
		public float M
        {
        readonly get => m;
        set { m = Math.Clamp(value, 0f, 1f); }
        }
		[JsonIgnore]
		public float Y
        {
		readonly get => y;
        set { y = Math.Clamp(value, 0f, 1f); }
        }
		[JsonIgnore]
		public float K
        {
			readonly get => k;
            set { k = Math.Clamp(value, 0f, 1f); }
        }
        public float this[int i]
        {
			readonly get => i switch
            {
                0 => c,
                1 => m,
                2 => y,
                3 => k,
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
                    case 3:
                        K = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        public CmykColor(float C, float M, float Y, float K = 0f)
        {
            this.C = C;
            this.M = M;
            this.Y = Y;
            this.K = K;
        }
        public readonly bool Equals(CmykColor color) => C == color.C && M == color.M && Y == color.Y && K == color.K;
        public readonly override bool Equals(object? color) => color is CmykColor c && color is not null && Equals(c);
        public readonly float Max() => Math.Max(c, Math.Max(m, Math.Max(y, k)));
		public readonly float Min() => Math.Min(c, Math.Min(m, Math.Min(y, k)));
		public CmykColor Difference(CmykColor color)
        {
            throw new NotImplementedException();
        }
        public static CmykColor Difference(CmykColor left, CmykColor right)
        {
            throw new NotImplementedException();
        }
        public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null) => $"<{c},{m},{y},{k}>";
        public readonly CmykColor Lerp(CmykColor colorTo, float val) => LerpUnclamped(colorTo, Math.Clamp(val, 0f, 1f));
        public static CmykColor Lerp(CmykColor from, CmykColor to, float val) => LerpUnclamped(from, to, Math.Clamp(val, 0f, 1f));

        public readonly CmykColor LerpUnclamped(CmykColor to, float val) => LerpUnclamped(this, to, val);

        public static CmykColor LerpUnclamped(CmykColor from, CmykColor to, float val)
        {
            return new()
            {
                C = (from.c * (1.0f - val)) + (to.c * val),
                M = (from.m * (1.0f - val)) + (to.m * val),
                Y = (from.y * (1.0f - val)) + (to.y * val),
                K = (from.k * (1.0f - val)) + (to.k * val)
            };
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
		public static CmykColor operator +(CmykColor left, CmykColor right)
        {
            throw new NotImplementedException();
        }

        public static CmykColor operator -(CmykColor left, CmykColor right)
        {
            throw new NotImplementedException();
        }

        public static CmykColor operator *(CmykColor left, CmykColor right)
        {
            throw new NotImplementedException();
        }

        public static CmykColor operator /(CmykColor left, CmykColor right)
        {
            throw new NotImplementedException();
        }

        public static CmykColor operator -(CmykColor color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Vector4(CmykColor color)
        {
            throw new NotImplementedException();
        }

        public static implicit operator CmykColor(Vector4 color)
        {
            throw new NotImplementedException();
        }

		public static CmykColor operator +(CmykColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static CmykColor operator -(CmykColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static CmykColor operator *(CmykColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static CmykColor operator /(CmykColor left, float right)
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(CmykColor left, CmykColor right)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(CmykColor left, CmykColor right)
		{
			throw new NotImplementedException();
		}

		public override readonly int GetHashCode() => HashCode.Combine(c.GetHashCode(), m.GetHashCode(), y.GetHashCode(), k.GetHashCode());
    }
}
