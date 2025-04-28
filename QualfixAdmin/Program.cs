using Microsoft.Extensions.Options;
using QualfixAdmin.Services.Usuario;
using QualfixAdmin_ioc;



using QualfixAdmin.Services.Login;

using QualfixAdmin_ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using QualfixAdmin.dal.DBContext;
using Microsoft.AspNetCore.Identity;

using QualfixAdmin.model;
using QualfixAdmin.Services.Usuario;
using QualfixAdmin.Services.CreacionRol;
using Microsoft.AspNetCore.Authorization;
using QualfixAdmin.DataModel.Seguridad;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using QualfixAdmin.Services.CreacionTipoLicencia;


using QualfixAdmin.Services.Perfil_Ser;
using QualfixAdmin.Services.Producto;
using QualfixAdmin.Services.Security;
using QualfixAdmin.Services.ClienteService;
using QualfixAdmin.Services.InformacionFiscal;
using QualfixAdmin.Services.ConfiguracionCFDI;
using QualfixAdmin.Services.PaisService;
using QualfixAdmin.Services.EstadoService;
using QualfixAdmin.Services.CiudadService;
using QualfixAdmin.model.QualfixAdminData;
using QualfixAdmin.Services.RegimenFiscalService;
using System.Text.Json.Serialization;

using QualfixAdmin.Services.Ticket;
//using QualfixAdmin.DataModel.Crear_Rol;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Configuration.AddJsonFile("appsettings.json");



var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,         // No validar el emisor
            ValidateAudience = false,       // No validar la audiencia
            ValidateLifetime = true,        // Validar la expiraci�n del token
            IssuerSigningKey = key          // Clave secreta para verificar la firma
        };
    });



//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/login"; // Ruta de inicio de sesi�n en la aplicaci�n de recursos

//        options.AccessDeniedPath = "/Account/AccessDenied"; // Ruta de acceso denegado
//    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.InyectarDependencias(builder.Configuration);
builder.Services.InyectarDependenciasSecurity(builder.Configuration);
//Services.AddScoped<IBloggerRepository, BloggerRepository>();

// comando para ignorar ciclos ademas debe instalar newtonsoft.json
builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<Login>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<Encripts>();
builder.Services.AddScoped<TipoLicencia>();
builder.Services.AddScoped<RegistroUsuarioServices>();
builder.Services.AddScoped<PerfilServicio>();
builder.Services.AddScoped<Program>();
builder.Services.AddScoped<ProductoServices>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<InformacionFiscalService>();
builder.Services.AddScoped<ConfiguracionCFDIService>();
builder.Services.AddScoped<PaisService>();
builder.Services.AddScoped<EstadoService>();
builder.Services.AddScoped<CiudadService>();
builder.Services.AddScoped<RegimeFiscalService>();
builder.Services.AddScoped <TicketServices>();



builder.Services.AddIdentity<User, Rol>()
    .AddEntityFrameworkStores<QualfixAdminSecurityContext>()
    .AddDefaultTokenProviders();


builder.Services.AddRazorPages();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddScoped<RegistroUsuarioServices>();

//builder.Services.AddRazorPages();
builder.Services.AddCors(Options =>
{
    Options.AddPolicy("nuevapolitica", app =>
    {
        app.AllowAnyOrigin()
       .AllowAnyHeader()
       .AllowAnyMethod();
    });
});



AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseCors("nuevapolitica");


app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;
await ConfigureDataInitialization(app);

app.Run();

// M�todo para configurar la inicializaci�n de datos
async Task ConfigureDataInitialization(IHost app)
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    // Obtener los servicios necesarios
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<Rol>>();
    var context = serviceProvider.GetRequiredService<QualfixAdminSecurityContext>();

    // Crear una instancia de DataSeeder y llamar a InitializeDataAsync
    var dataSeeder = new DataSeeder(userManager, roleManager, context);
    dataSeeder.InitializeDataAsync().Wait(); // Esperar a que la inicializaci�n de datos se complete
}