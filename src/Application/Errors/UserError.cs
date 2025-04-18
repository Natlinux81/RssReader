using Application.Common.Results;

namespace Application.Errors;

public static class UserError
{
    public static Error InternalServerError =>
        new(ErrorTypeConstant.InternalServerError, "something went wrong");

    public static Error UserNotFound =>
        new(ErrorTypeConstant.NotFound, "User not found");

    public static Error FailedToAssignRole =>
        new(ErrorTypeConstant.InternalServerError, "failed to assign role");

    public static Error FailedToRevokeRole =>
        new(ErrorTypeConstant.InternalServerError, "failed to revoke role");

    public static Error UserAlreadyHasRole =>
        new(ErrorTypeConstant.ValidationError, "User already has role");

    public static Error UserHasNoRole =>
        new(ErrorTypeConstant.ValidationError, "User already has no role");

    public static Error CreateInvalidUserUpdateRequestError(IEnumerable<string> errors)
    {
        return new Error(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    }

    public static Error CreateInvalidLoginRequestError(IEnumerable<string> errors)
    {
        return new Error(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    }
}