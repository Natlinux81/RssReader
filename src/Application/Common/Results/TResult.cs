namespace Application.Common.Results
{
    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        protected internal Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error) => _value = value;

        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("No value for failure result");
    }
}
      
