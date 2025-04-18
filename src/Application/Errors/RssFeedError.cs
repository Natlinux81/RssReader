using Application.Common.Results;

namespace Application.Errors;

public static class RssFeedError
{
    public static Error InvalidRssFeedRequest => new(ErrorTypeConstant.ValidationError, "Invalid RSS-Feed request");

    public static Error RssFeedAlreadyExists => new(ErrorTypeConstant.ValidationError, "RSS-Feed already exists");

    public static Error RssFeedsNotFound => new(ErrorTypeConstant.NotFound, "RSS-Feeds not found");

    public static Error CreateInvalidRssFeedRequestError(IEnumerable<string> errors)
    {
        return new Error(ErrorTypeConstant.ValidationError, string.Join(",", errors));
    }
}