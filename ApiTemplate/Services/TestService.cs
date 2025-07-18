using ApiTemplate.ServiceRegister;

public interface ITestService
{
    string GetMessage();
}

[RegisterService(ServiceLifetimeType.Transient)]
public class TestService : ITestService
{
    public string GetMessage() => "Hello from TestService!";
}
