using RazorPagesApp.Handlers;
using RazorPagesApp.Services;
using RazorPagesApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient<IClienteService, ClienteService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "");
    client.Timeout = TimeSpan.FromSeconds(30);
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient<IProductoService, ProductoService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "");
    client.Timeout = TimeSpan.FromSeconds(30);
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient<IPedidoService, PedidoService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "");
    client.Timeout = TimeSpan.FromSeconds(30);
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient<ICategoriaService, CategoriaService>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!);
})
.AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient<IUnidadMedidaService, UnidadMedidaService>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!);
})
.AddHttpMessageHandler<AuthenticationDelegatingHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapRazorPages();

app.Run();