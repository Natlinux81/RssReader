using Application.Common.Results;

namespace Application.Error;

public static class RssFeedError
{
    public static Common.Results.Error InvalidRssFeedRequest => new(ErrorTypeConstant.ValidationError, "Invalid RSS-Feed request");
    public static Common.Results.Error RssFeedAlreadyExists => new(ErrorTypeConstant.ValidationError, "RSS-Feed already exists");
    public static Common.Results.Error RssFeedsNotFound => new(ErrorTypeConstant.NotFound, "RSS-Feeds not found");
}