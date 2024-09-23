using ColourLib;
using System.Reflection;
#region Setup
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#endregion


// ERROR:
// Json serializer is both: ignoring fields, and: trying to serialize properties, including things like Grayscale, and Eating Shit. 

// TODO: HEX

app.MapGet("/color/{to}/{r},{g},{b}", (float r, float g, float b, string to) =>
{
	r = float.Clamp(r, 0f, 1f);
	g = float.Clamp(g, 0f, 1f);
	b = float.Clamp(b, 0f, 1f);

	object res = to.ToLower() switch
	{
		"color" => new Color(r, g, b),
		"color32" => new Color32(r, g, b),
		"hsvcolor" => (HsvColor)new Color(r, g, b),
		"hsv32color" => (Hsv32Color)new Color32(r, g, b),
		"hslcolor" => (HslColor)new Color(r, g, b),
		"hsl32color" => (Hsl32Color)new Color32(r, g, b),
		"hex" => new Color(r, g, b).ToHex(),
		_ => throw new ArgumentException("Can convert only to color, color32, hsvcolor, hsv32color, hslcolor, hsl32color, and hex.")
	};
	return res;
})
.WithName("Color")
.WithOpenApi();


app.MapGet("/color32/{to}/{r},{g},{b}", (int r, int g, int b, string to) =>
{
	r = int.Clamp(r, 0, 255); 
	g = int.Clamp(g, 0, 255);
	b = int.Clamp(b, 0, 255);

	object res = to.ToLower() switch
	{
		"color" => new Color(r, g, b),
		"color32" => new Color32(r, g, b),
		"hsvcolor" => (HsvColor)(Color)new Color32(r, g, b),
		"hsv32color" => (Hsv32Color)new Color32(r, g, b),
		"hslcolor" => (HslColor)(Color)new Color32(r, g, b),
		"hsl32color" => (Hsl32Color)new Color32(r, g, b),
		"hex" => new Color32(r, g, b).ToHex(),
		_ => throw new ArgumentException("Can convert only to color, color32, hsvcolor, hsv32color, hslcolor, hsl32color, and hex.")
	};
	return res;
})
.WithName("Color32")
.WithOpenApi();


app.MapGet("/hsvcolor/{to}/{h},{s},{v}", (float h, float s, float v, string to) =>
{
	h %= 1f;
	s = float.Clamp(s, 0f, 1f);
	v = float.Clamp(v, 0f, 1f);

	object res = to.ToLower() switch
	{
		"color" => (Color)new HsvColor(h, s, v),
		"color32" => (Color32)(Color)new HsvColor(h, s, v),
		"hsvcolor" => new HsvColor(h, s, v),
		"hsv32color" => new Hsv32Color(h, s, v),
		"hslcolor" => (HslColor)new HsvColor(h, s, v),
		"hsl32color" => (Hsl32Color)(HslColor)new HsvColor(h, s, v),
		"hex" => ((Color)new HsvColor(h, s, v)).ToHex(),
		_ => throw new ArgumentException("Can convert only to color, color32, hsvcolor, hsv32color, hslcolor, hsl32color, and hex.")
	};
	return res;
})
.WithName("HsvColor")
.WithOpenApi();


app.MapGet("/hsv32color/{to}/{h},{s},{v}", (int h, int s, int v, string to) =>
{
	h %= 360;
	s = int.Clamp(s, 0, 100);
	v = int.Clamp(v, 0, 100);

	object res = to.ToLower() switch
	{
		"color" => (Color)new Hsv32Color(h, s, v),
		"color32" => (Color32)new Hsv32Color(h, s, v),
		"hsvcolor" => (HsvColor)new Hsv32Color(h, s, v),
		"hsv32color" => new Hsv32Color(h, s, v),
		"hslcolor" => (HslColor)new HsvColor(h, s, v),
		"hsl32color" => (Hsl32Color)new Hsv32Color(h, s, v),
		"hex" => ((Color)(HsvColor)new Hsv32Color(h, s, v)).ToHex(),
		_ => throw new ArgumentException("Can convert only to color, color32, hsvcolor, hsv32color, hslcolor, hsl32color, and hex.")
	};
	return res;
})
.WithName("Hsv32Color")
.WithOpenApi();


app.MapGet("/hslcolor", () =>
{

})
.WithName("HslColor")
.WithOpenApi();


app.MapGet("/hsl32color", () =>
{

})
.WithName("Hsl32Color")
.WithOpenApi();

app.MapGet("/hex", () =>
{

})
.WithName("Hex")
.WithOpenApi();

app.Run();