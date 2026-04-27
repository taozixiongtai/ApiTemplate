using ApiTemplate.Infrastructure.Exceptions;
using ApiTemplate.Infrastructure.IOC;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoRegisteredServices();

// 注册全局异常处理器
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();




var app = builder.Build();

// 启用异常处理中间件（一定要放在最前面捕获全局异常）
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
