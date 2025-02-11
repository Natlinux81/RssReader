using Application.Common.Results;

namespace Application.Errors;

public static class AuthError
{
    public static Error InvalidRegisterRequest =>
        new Error(ErrorTypeConstant.ValidationError, "Invalid register request");
    public static Error UserAlreadyExists =>
        new Error(ErrorTypeConstant.ValidationError, "User already exists");
    public static Error InvalidLoginRequest =>
        new Error(ErrorTypeConstant.ValidationError, "Invalid login request");
    public static Error UserNotFound => new Error(ErrorTypeConstant.NotFound, "User not found");
    public static Error InvalidPassword =>
        new Error(ErrorTypeConstant.ValidationError, "Invalid Password");

    public static Error CreateInvalidLoginRequestError(IEnumerable<string> errors) =>
        new Error(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    
    public static Error CreateInvalidRegisterRequestError(IEnumerable<string> errors) =>
        new Error(ErrorTypeConstant.ValidationError, string.Join(", ", errors));

}