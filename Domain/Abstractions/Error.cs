namespace Domain.Abstractions;

public class Error
{
    private Error(string code, string description, ErrorType errorType)
    {
        Description = description;
        Code = code;
        Type = errorType;
    }
    
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);
    
    public static Error Unauthorized(string code, string description) =>
        new(code, description, ErrorType.Unauthorized);
    
    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);
    
    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);
    
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);
    
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        Unauthorized = 4,
    }
}