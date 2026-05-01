---
name: "api-template-guidelines"
description: "Provides development guidelines for ApiTemplate project. Invoke when writing new services, controllers, or handling business logic/exceptions."
---

# ApiTemplate 框架开发规范 (ApiTemplate Development Guidelines)

本规范用于指导在 `ApiTemplate` 脚手架项目中进行业务开发。在编写服务、接口或业务逻辑时，请务必严格遵循以下约定。

## 1. 依赖注入与服务注册 (Service Registration)

**不需要手动注册服务**（如在 `Program.cs` 中写 `builder.Services.AddScoped(...)`）。

框架内已经实现了自动扫描注册机制。你只需要在**服务实现类**上使用 `[RegisterService]` 特性即可。

### 使用示例：
```csharp
using ApiTemplate.Application.IServices;
using ApiTemplate.Infrastructure.IOC;

namespace ApiTemplate.Application.Services;

// 使用 RegisterService 特性，并指定生命周期（Scoped, Singleton, Transient）
[RegisterService(ServiceLifetimeType.Scoped)]
public class UserService : IUserService
{
    // ...
}
```

## 2. 控制器生成 (Controller Generation / Dynamic API)

**不需要手动编写 Controller 类**。

本框架使用 C# Source Generators (源代码生成器) 技术自动生成 Controller。你只需要在**服务接口** (Interface) 上配置相关特性即可。
源代码生成器会自动去除接口的 `I` 前缀和 `Service` 后缀，生成对应的控制器（如 `IUserService` 会生成 `UserController`）。

- 在接口上添加 `[DynamicApi]` 特性（可选指定路由前缀，默认按名称推断）。
- 在接口方法上添加 `[ApiAction]` 特性，指定具体路由和 HTTP 请求方法（默认为 POST）。

### 使用示例：
```csharp
using ApiTemplate.Infrastructure.DynamicApi;
using ApiTemplate.Infrastructure.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Application.IServices;

[DynamicApi("api/user")] // 标记为动态 API，将自动生成 UserController
public interface IUserService
{
    // 自动生成 POST /api/user/login 接口
    [ApiAction("login", HttpMethodType.POST)] 
    Task<string> LoginAsync([FromBody] LoginRequest req);
    
    // 自动生成 GET /api/user/info 接口
    [ApiAction("info", HttpMethodType.GET)]
    Task<UserDto> GetUserInfoAsync([FromQuery] int id);
}
```

*注意：生成器会自动将返回结果包装在统一的 `ApiResult` 结构中（如 `ApiResult.Success(result)`），原方法的 Attribute 也会被安全透传到生成的 Action 上。*

## 3. 业务异常处理 (Business Exceptions & Validations)

**不需要手动实例化并 `throw new ApiException(...)`**。

所有的业务逻辑校验与异常抛出，统一使用 `ApiTemplate.Infrastructure.Check.Check` 工具类。使用该类抛出的异常会被 `GlobalExceptionHandler` 全局异常拦截器捕获，并以规范的格式返回给前端。

### 常用 Check 方法：

- `Check.Error("错误信息")`：直接终止执行并抛出业务异常。
- `Check.NotNull(obj, "对象不能为空")`：对象为 null 时抛出异常。
- `Check.NotEmpty(str, "字符串不能为空")`：字符串为空或纯空格时抛出异常。
- `Check.IsTrue(condition, "条件必须为真")`：条件为 false 时抛出异常。
- `Check.IsFalse(condition, "条件必须为假")`：条件为 true 时抛出异常。
- `Check.IsNull(obj, "对象必须为空")`：对象不为 null 时抛出异常。

### 使用示例：
```csharp
using ApiTemplate.Infrastructure.Check;

public async Task<string> LoginAsync(LoginRequest req)
{
    // 1. 校验输入，如果为空直接抛出 ApiException("用户名不能为空")
    Check.NotEmpty(req.Username, "用户名不能为空");
    Check.NotEmpty(req.Password, "密码不能为空");
    
    var user = await userRepo.GetFirstAsync(u => u.Username == req.Username);
    
    // 2. 校验对象，如果是 null 直接抛出 ApiException("用户不存在")
    Check.NotNull(user, "用户不存在");
    
    // 3. 校验条件，如果是 false 直接抛出 ApiException("密码错误")
    Check.IsTrue(user.Password == req.Password, "密码错误");
    
    return "jwt_token";
}
```
