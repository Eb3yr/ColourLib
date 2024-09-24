using ColourLib;

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

app.MapGet("/rgb/{to}/{r},{g},{b}", (float r, float g, float b, string to) =>
{
	r = float.Clamp(r, 0f, 1f);
	g = float.Clamp(g, 0f, 1f);
	b = float.Clamp(b, 0f, 1f);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => new Color(r, g, b),
		"color32" or "rgb32" or "rgba32" => new Color32(r, g, b),
		"hsvcolor" or "hsv" => (HsvColor)new Color(r, g, b),
		"hsv32color" or "hsv32" => (Hsv32Color)new Color32(r, g, b),
		"hslcolor" or "hsl" => (HslColor)new Color(r, g, b),
		"hsl32color" or "hsl32" => (Hsl32Color)new Color32(r, g, b),
		"hex" or "html" => new Color(r, g, b).ToHex(),
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("Color")
.WithOpenApi();

app.MapGet("/rgb32/{to}/{r},{g},{b}", (int r, int g, int b, string to) =>
{
	r = int.Clamp(r, 0, 255); 
	g = int.Clamp(g, 0, 255);
	b = int.Clamp(b, 0, 255);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => new Color(r, g, b),
		"color32" or "rgb32" or "rgba32" => new Color32(r, g, b),
		"hsvcolor" or "hsv" => (HsvColor)(Color)new Color32(r, g, b),
		"hsv32color" or "hsv32" => (Hsv32Color)new Color32(r, g, b),
		"hslcolor" or "hsl" => (HslColor)(Color)new Color32(r, g, b),
		"hsl32color" or "hsl32" => (Hsl32Color)new Color32(r, g, b),
		"hex" or "html" => new Color32(r, g, b).ToHex(),
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("Color32")
.WithOpenApi();

app.MapGet("/hsv/{to}/{h},{s},{v}", (float h, float s, float v, string to) =>
{
	h %= 1f;
	s = float.Clamp(s, 0f, 1f);
	v = float.Clamp(v, 0f, 1f);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => (Color)new HsvColor(h, s, v),
		"color32" or "rgb32" or "rgba32" => (Color32)(Color)new HsvColor(h, s, v),
		"hsvcolor" or "hsv" => new HsvColor(h, s, v),
		"hsv32color" or "hsv32" => new Hsv32Color(h, s, v),
		"hslcolor" or "hsl" => (HslColor)new HsvColor(h, s, v),
		"hsl32color" or "hsl32" => (Hsl32Color)(HslColor)new HsvColor(h, s, v),
		"hex" or "html" => ((Color)new HsvColor(h, s, v)).ToHex(),
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("HsvColor")
.WithOpenApi();

app.MapGet("/hsv32/{to}/{h},{s},{v}", (int h, int s, int v, string to) =>
{
	h %= 360;
	s = int.Clamp(s, 0, 100);
	v = int.Clamp(v, 0, 100);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => (Color)new Hsv32Color(h, s, v),
		"color32" or "rgb32" or "rgba32" => (Color32)new Hsv32Color(h, s, v),
		"hsvcolor" or "hsv" => (HsvColor)new Hsv32Color(h, s, v),
		"hsv32color" or "hsv32" => new Hsv32Color(h, s, v),
		"hslcolor" or "hsl" => (HslColor)new HsvColor(h, s, v),
		"hsl32color" or "hsl32" => (Hsl32Color)new Hsv32Color(h, s, v),
		"hex" or "html"=> ((Color)(HsvColor)new Hsv32Color(h, s, v)).ToHex(),
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("Hsv32Color")
.WithOpenApi();

app.MapGet("/hsl/{to}/{h},{s},{l}", (float h, float s, float l, string to) =>
{
	h %= 1f;
	s = float.Clamp(s, 0f, 1f);
	l = float.Clamp(l, 0f, 1f);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => (Color)new HslColor(h, s, l),
		"color32" or "rgb32" or "rgba32" => (Color32)(Color)new HslColor(h, s, l),
		"hsvcolor" or "hsv" => (HsvColor)new HslColor(h, s, l),
		"hsv32color" or "hsv32" => (Hsv32Color)new Hsl32Color(h, s, l),
		"hslcolor" or "hsl" => new HslColor(h, s, l),
		"hsl32color" or "hsl32" => (Hsl32Color)new HslColor(h, s, l),
		"hex" or "html" => ((Color)new HslColor(h, s, l)).ToHex(),
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("HslColor")
.WithOpenApi();

app.MapGet("/hsl32/{to}/{h},{s},{l}", (int h, int s, int l, string to) =>
{
	h %= 360;
	s = int.Clamp(s, 0, 100);
	l = int.Clamp(l, 0, 100);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => (Color)new Hsl32Color(h, s, l),
		"color32" or "rgb32" or "rgba32" => (Color32)new Hsl32Color(h, s, l),
		"hsvcolor" or "hsv" => (HsvColor)(HslColor)new Hsl32Color(h, s, l),
		"hsv32color" or "hsv32" => (Hsv32Color)new Hsl32Color(h, s, l),
		"hslcolor" or "hsl" => (HslColor)new Hsl32Color(h, s, l),
		"hsl32color" or "hsl32" => new Hsl32Color(h, s, l),
		"hex" or "html" => ((Color)new Hsl32Color(h, s, l)).ToHex(),
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("Hsl32Color")
.WithOpenApi();

app.MapGet("/hex/{to}/{hex}", (string hex, string to) =>
{
	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => new Color(hex),
		"color32" or "rgb32" or "rgba32" => new Color32(hex),
		"hsvcolor" or "hsv" => (HsvColor)new Color(hex),
		"hsv32color" or "hsv32" => (Hsv32Color)(HsvColor)new Color(hex),
		"hslcolor" or "hsl" => (HslColor)new Color(hex),
		"hsl32color" or "hsl32" => (Hsl32Color)(HslColor)new Color(hex),
		"hex" or "html" => hex,
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("Hex")
.WithOpenApi();

app.Run();