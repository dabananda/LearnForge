namespace LearnForge.Server.Api.DTOs
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public string Message { get; init; } = string.Empty;
        public List<string> Errors { get; init; } = [];
        public int StatusCode { get; init; }

        public static Result<T> Success(T data, string message = "Success", int statusCode = 200)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static Result<T> Failure(string message = "Failed", List<string>? errors = null, int statusCode = 400)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? [],
                StatusCode = statusCode
            };
        }
    }

    public class Result : Result<object>
    {
        public static Result Success(string message = "Success", int statusCode = 200)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        public new static Result Failure(string message = "Failed", List<string>? errors = null, int statusCode = 400)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? [],
                StatusCode = statusCode
            };
        }
    }
}
