# ApiTemplate Web API Scaffold

🚀 一个现代化的 C# Web API 脚手架模板。开箱即用，支持依赖自动注入、源代码生成器自动生成 Controller、全局异常处理等高级特性，让你能专注于核心业务开发。

## ✨ 特性

- 💉 **自动依赖注入**: 使用 `[RegisterService]` 特性，无需手动在 `Program.cs` 注册。
- ⚙️ **自动生成 Controller**: 基于 C# Source Generators，在接口标注 `[DynamicApi]` 即可自动生成路由与控制器。
- 🛡️ **优雅的异常处理**: 提供 `Check` 工具类，统一拦截并处理业务逻辑异常 (`ApiException`)。
- 📦 **支持 `dotnet new`**: 一键生成你自己的项目，自动完成命名空间和文件名的替换。

## 🚀 快速开始 (本地使用)

### 1. 安装模板

在本项目根目录下执行以下命令，将当前目录注册为本地模板：

```bash
dotnet new install .
```

> 安装成功后，你会在模板列表中看到 `ApiTemplate Web API Scaffold`，短名称为 `apitemp`。

### 2. 创建新项目

在任意你希望创建新项目的目录（例如 `TestApp`），运行以下命令：

```bash
mkdir TestApp
cd TestApp
dotnet new apitemp -n MyNewCompany.AwesomeApi
```

> `dotnet` 会自动生成全新的项目文件。原来的 `ApiTemplate.sln` 会被重命名为 `MyNewCompany.AwesomeApi.sln`，所有代码中的命名空间也会被完全替换为你指定的名称。

### 3. 卸载模板

当你不再需要该模板，或者修改了模板配置想要重新安装时，可以执行以下命令进行卸载：

```bash
dotnet new uninstall .
# 或者指定绝对路径: dotnet new uninstall <当前ApiTemplate项目的绝对路径>
```

## 🛠️ 开发指南

项目内提供了一个专用的 **AI Skill**，帮助其他 AI 助手了解本框架的开发规范。
如果你使用带有 Trae 或类似 AI 辅助的 IDE，请参考 `.trae/skills/api-template-guidelines/SKILL.md` 来指导 AI 编写符合规范的服务和业务代码。
