using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

// Configurar sesiones para el sistema de seguridad
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24); // Sesión de 24 horas
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "SistemaVentasSession";
});

// Configurar cookies para autenticación
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Configurar HTTP Context Accessor para acceder al contexto en servicios
builder.Services.AddHttpContextAccessor();

// Configurar CORS si es necesario
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar cookies para autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Seguridad/Login";
        options.LogoutPath = "/Cuenta/Logout";
        // Puedes agregar más opciones si lo deseas
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Configurar CORS
app.UseCors("AllowAll");

app.UseRouting();

// Configurar sesiones (debe ir antes de UseAuthorization)
app.UseSession();

// Agregar autenticación antes de autorización
app.UseAuthentication();

// Middleware personalizado para verificar sesiones
app.Use(async (context, next) =>
{
    // Excluir rutas que no requieren autenticación
    var path = context.Request.Path.Value?.ToLower();
    var excludedPaths = new[] { "/seguridad/login", "/cuenta/registrar", "/home/index", "/cliente/clientelog" };
    
    if (excludedPaths.Any(p => path?.Contains(p) == true))
    {
        await next();
        return;
    }

    // Verificar si hay una sesión válida
    var token = context.Session.GetString("Token");
    var idUsuario = context.Session.GetInt32("IdUsuario");

    if (string.IsNullOrEmpty(token) || !idUsuario.HasValue)
    {
        // Redirigir al login si no hay sesión
        context.Response.Redirect("/Seguridad/Login");
        return;
    }

    await next();
});

app.UseAuthorization();

// Configurar rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cliente}/{action=Clientelog}/{id?}");

// Rutas adicionales para el sistema de seguridad
app.MapControllerRoute(
    name: "seguridad",
    pattern: "Seguridad/{action=Login}/{id?}",
    defaults: new { controller = "Seguridad" });

app.MapControllerRoute(
    name: "reportes",
    pattern: "Reportes/{action=Index}/{id?}",
    defaults: new { controller = "Reportes" });

app.MapControllerRoute(
    name: "permisos",
    pattern: "MantenedorPermiso/{action=Index}/{id?}",
    defaults: new { controller = "MantenedorPermiso" });

app.Run();
