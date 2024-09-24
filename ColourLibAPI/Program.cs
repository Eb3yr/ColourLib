using ColourLib;

#region Setup
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true /* Offer swagger as a GUI. Unless I'm silly this probably won't cause any problems? */ || app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Colour Conversions");
		c.RoutePrefix = string.Empty;
	});
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

app.MapGet("/rgb32/{to}/{r32},{g32},{b32}", (int r32, int g32, int b32, string to) =>
{
	r32 = int.Clamp(r32, 0, 255); 
	g32 = int.Clamp(g32, 0, 255);
	b32 = int.Clamp(b32, 0, 255);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => new Color(r32, g32, b32),
		"color32" or "rgb32" or "rgba32" => new Color32(r32, g32, b32),
		"hsvcolor" or "hsv" => (HsvColor)(Color)new Color32(r32, g32, b32),
		"hsv32color" or "hsv32" => (Hsv32Color)new Color32(r32, g32, b32),
		"hslcolor" or "hsl" => (HslColor)(Color)new Color32(r32, g32, b32),
		"hsl32color" or "hsl32" => (Hsl32Color)new Color32(r32, g32, b32),
		"hex" or "html" => new Color32(r32, g32, b32).ToHex(),
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

app.MapGet("/hsv32/{to}/{h32},{s32},{v32}", (int h32, int s32, int v32, string to) =>
{
	h32 %= 360;
	s32 = int.Clamp(s32, 0, 100);
	v32 = int.Clamp(v32, 0, 100);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => (Color)new Hsv32Color(h32, s32, v32),
		"color32" or "rgb32" or "rgba32" => (Color32)new Hsv32Color(h32, s32, v32),
		"hsvcolor" or "hsv" => (HsvColor)new Hsv32Color(h32, s32, v32),
		"hsv32color" or "hsv32" => new Hsv32Color(h32, s32, v32),
		"hslcolor" or "hsl" => (HslColor)new HsvColor(h32, s32, v32),
		"hsl32color" or "hsl32" => (Hsl32Color)new Hsv32Color(h32, s32, v32),
		"hex" or "html"=> ((Color)(HsvColor)new Hsv32Color(h32, s32, v32)).ToHex(),
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

app.MapGet("/hsl32/{to}/{h32},{s32},{l32}", (int h32, int s32, int l32, string to) =>
{
	h32 %= 360;
	s32 = int.Clamp(s32, 0, 100);
	l32 = int.Clamp(l32, 0, 100);

	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => (Color)new Hsl32Color(h32, s32, l32),
		"color32" or "rgb32" or "rgba32" => (Color32)new Hsl32Color(h32, s32, l32),
		"hsvcolor" or "hsv" => (HsvColor)(HslColor)new Hsl32Color(h32, s32, l32),
		"hsv32color" or "hsv32" => (Hsv32Color)new Hsl32Color(h32, s32, l32),
		"hslcolor" or "hsl" => (HslColor)new Hsl32Color(h32, s32, l32),
		"hsl32color" or "hsl32" => new Hsl32Color(h32, s32, l32),
		"hex" or "html" => ((Color)new Hsl32Color(h32, s32, l32)).ToHex(),
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("Hsl32Color")
.WithOpenApi();

app.MapGet("/hex/{to}/{hexCode}", (string hexCode, string to) =>
{
	object res = to.ToLower() switch
	{
		"color" or "rgb" or "rgba" => new Color(hexCode),
		"color32" or "rgb32" or "rgba32" => new Color32(hexCode),
		"hsvcolor" or "hsv" => (HsvColor)new Color(hexCode),
		"hsv32color" or "hsv32" => (Hsv32Color)(HsvColor)new Color(hexCode),
		"hslcolor" or "hsl" => (HslColor)new Color(hexCode),
		"hsl32color" or "hsl32" => (Hsl32Color)(HslColor)new Color(hexCode),
		"hex" or "html" => hexCode,
		_ => throw new ArgumentException("Can convert only to rgb, rgb32, hsv, hsv32, hsl, hsl32, and hex.")
	};
	return res;
})
.WithName("Hex")
.WithOpenApi();

app.Run();