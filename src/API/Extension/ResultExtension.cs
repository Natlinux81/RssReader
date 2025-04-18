using Application.Common.Results;

namespace API.Extension;

public static class ResultExtension
{
    public static IResult ToHttpResponse(this Result result)
    {
        if (result.IsSuccess) return Results.Ok(result);

        return MapErrorResponse(result.Error, result);
    }

    public static IResult ToHttpResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return Results.Ok(result);
        return MapErrorResponse(result.Error, result);
    }

    private static IResult MapErrorResponse(Error? error, object result)
    {
        return error?.Code switch
        {
            ErrorTypeConstant.ValidationError => Results.BadRequest(result),
            ErrorTypeConstant.NotFound => Results.NotFound(result),
            ErrorTypeConstant.Forbidden => Results.Forbid(),
            ErrorTypeConstant.Unauthorized => Results.Unauthorized(),
            _ => Results.Problem(error?.Message, statusCode: 500)
        };
    }
}