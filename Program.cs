global using FastEndpoints;
global using FluentValidation;
using FastEndpoints.Swagger;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniDevTo.Infrastructure.Auth;
using MiniDevTo.Infrastructure.Database;
using MiniDevTo.Messaging.Consumers;
using MiniDevTo.Messaging.Services;
using MiniDevTo.Services.Auth;
using System.Text;
using NSwag; //jwt authetication

var builder = WebApplication.CreateBuilder(args);
//jwt
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));
var jwtOptions=builder.Configuration.GetSection("JwtSettings").Get<JwtOptions>();


builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,

        ValidateAudience =true,
        ValidAudience = jwtOptions.Audience,

        ValidateIssuerSigningKey =true,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),

        ValidateLifetime =true
    };
});




builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthorization();
builder.Services.AddScoped<ITokenService, TokenService>();
//end jwt
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddFastEndpoints();

//swagger jwt
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "MiniDevTo API";
        s.Version = "v1";

        // Configuração do JWT Bearer no Swagger
        s.AddAuth("Bearer", new()
        {
            Type = OpenApiSecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Insira o token JWT no formato: Bearer {seu_token}"
        });
    };

    o.ShortSchemaNames = true;
});

//bdContext
builder.Services.AddDbContext<AppDbContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(x=>
{
    x.AddConsumers(typeof(ArticleApprovedConsumer).Assembly);

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();

app.UseAuthentication();//jwt
app.UseAuthorization();//jwt

app.UseFastEndpoints(); 
app.UseSwaggerGen();
app.Run();
