namespace ApiTemplate.Infrastructure.Exceptions;

public class ApiException(string message) : Exception(message);