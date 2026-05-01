using ApiTemplate.Application.IServices;
using ApiTemplate.Application.Services;
using ApiTemplate.Infrastructure.Exceptions;
using ApiTemplate.Infrastructure.IOC;
using ApiTemplate.Infrastructure.Orm;
using ApiTemplate.Init;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;

// 1. 初始化两阶段 Serilog 引导日志（为了在宿主启动失败时也能捕获异常）
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSqlSugar(builder.Configuration);
    builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    builder.Services.AddAutoRegisteredServices();

    // 注册全局异常处理器
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    // 注册 CORS 策略 
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

    // 注册 JWT 认证服务
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? ""))
            };
        });

    builder.Services.AddControllers();




    var app = builder.Build();

    // 自动初始化数据库和表结构
    app.InitializeDatabase();

    // 启用异常处理中间件（一定要放在最前面捕获全局异常）
    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // 启用 CORS（必须在 UseRouting 之后，UseAuthorization 之前）
    app.UseCors("AllowAll");

    // 注意：UseAuthentication 必须在 UseAuthorization 之前
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "应用启动失败");
}
finally
{
    Log.CloseAndFlush();
}
