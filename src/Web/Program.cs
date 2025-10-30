using System.Reflection;
using System.Text;
using Core.Infrastructure.Data;
using Core.Interfaces;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
builder.Services.Configure<CustomAuthenticationService.AutenticacionServiceOptions>(
    builder.Configuration.GetSection(CustomAuthenticationService.AutenticacionServiceOptions.SectionName)
);
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddScoped<BankAccountService>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

/* Configuración de conexión con SQLite */
var connection = new SqliteConnection("Data Source=WebApiBankAccount.db");
connection.Open();

using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE;";
    command.ExecuteNonQuery();
}

builder.Services.AddDbContext<ApplicationDbContext>(DbContextOptions => DbContextOptions.UseSqlite(connection));

/*
#region Swagger config for Entra
//builder.Services.AddSwaggerGen();
string instance = builder.Configuration["AzureAd:Instance"]!;
string tenantId = builder.Configuration["AzureAd:TenantId"]!;
string clientid = builder.Configuration["AzureAd:ClientId"]!;
string ApplicationIdURI = builder.Configuration["AzureAd:ApplicationIdURI"]!;
string scope = "default";
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Configure OAuth2
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{instance}{tenantId}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"{instance}{tenantId}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                {
                    { $"{ApplicationIdURI}/{scope}", "Access API" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { $"{ApplicationIdURI}/{scope}" }
        }
    });
});
#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
*/


# region Swagger custom token config
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ConsultaAlumnosApiBearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ConsultaAlumnosApiBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definición
                }, new List<string>() }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    setupAction.IncludeXmlComments(xmlPath);

});

builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticación que tenemos que elegir después en PostMan para pasarle el token
    .AddJwtBearer(options => //Acá definimos la configuración de la autenticación. le decimos qué cosas queremos comprobar. La fecha de expiración se valida por defecto.
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AutenticacionService:Issuer"],
            ValidAudience = builder.Configuration["AutenticacionService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AutenticacionService:SecretForKey"]))
        };
    }
);
# endregion

var app = builder.Build();

/*
#region Swagger pipeline config for Entra
string spaClientId = builder.Configuration["AzureAd:SpaClientId"]!;
app.UseSwagger();
var oAuthRedirectUrl = builder.Configuration["AzureAd:RedirectUri"];
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.OAuthClientId($"{spaClientId}");
    c.OAuthUsePkce();  // Recommended for B2C
    c.OAuth2RedirectUrl(oAuthRedirectUrl); // Same as the one registered in Azure B2C
    c.OAuthScopes($"{ApplicationIdURI}/{scope}"); //Selects this scope by default.
});
#endregion
*/

# region Swagger pipeline config for custom token
app.UseSwagger();
app.UseSwaggerUI();
# endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
