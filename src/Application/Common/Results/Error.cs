namespace Application.Common.Results
{
    public sealed record Error(string code, string Message)
    {
        internal static Error None => new(ErrorTypeConstant.None, string.Empty);
    }
}