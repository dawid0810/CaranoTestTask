using System;

namespace Common.Results
{
    public class Result
    {
        public Exception Error { get; }

        public bool IsSuccess => Error == null;

        public Result()
        {
        }
        
        public Result(Exception error)
        {
            Error = error;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        public Result(T value) : base()
        {
            Value = value;
        }

        public Result(T value, Exception error) : base(error)
        {
            Value = value;
        }
    }
}