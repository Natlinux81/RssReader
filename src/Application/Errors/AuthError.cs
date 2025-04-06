using Application.Common.Results;

namespace Application.Errors;

public static class AuthError
{
    public static Error InvalidRegisterRequest => new(ErrorTypeConstant.ValidationError, "Invalid register request");

    public static Error UserAlreadyExists => new(ErrorTypeConstant.ValidationError, "User already exists");

    public static Error InvalidLoginRequest => new(ErrorTypeConstant.ValidationError, "Invalid login request");

    public static Error UserNotFound => new(ErrorTypeConstant.NotFound, "User not found");

    public static Error InvalidPassword => new(ErrorTypeConstant.ValidationError, "Invalid Password");
    
    public static Error InvalidRefreshToken => new(ErrorTypeConstant.Unauthorized, "Invalid RefreshToken");

    public static Error CreateInvalidLoginRequestError(IEnumerable<string> errors)
    {
        return new Error(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    }

    public static Error CreateInvalidRegisterRequestError(IEnumerable<string> errors)
    {
        return new Error(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    }
}