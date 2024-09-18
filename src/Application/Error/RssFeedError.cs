using Application.Common.Results;

namespace Application
{
    public static class RssFeedError
    {
       public static Error InvalidRssFeedRequest => new(ErrorTypeConstant.ValidationError, "Invalid RSS-Feed request");
       public static Error RssFeedAlreadyExists => new (ErrorTypeConstant.ValidationError, "RSS-Feed already exists");      
       
    }
}
