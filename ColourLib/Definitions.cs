#define UNITY
#define GODOT
#define MONOGAME

namespace ColourLib
{
	partial struct Color : IColorF<Color>
	{
		#if UNITY

		#endif

		#if GODOT
		public static implicit operator Godot.Color(Color color) => new(color.r, color.g, color.b, color.a);
		public static implicit operator Color(Godot.Color color) => new(color.R, color.G, color.B, color.A);
		#endif

		#if MONOGAME
		public static implicit operator Microsoft.Xna.Framework.Color(Color color) => new(color.r, color.g, color.b, color.a);
		public static implicit operator Color(Microsoft.Xna.Framework.Color color) => new(color.R, color.G, color.B, color.A);
		#endif
	}
}