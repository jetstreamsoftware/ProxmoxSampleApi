using FluentResults;

namespace Jetstream.Proxmox.Sample.WebAPI.Api.Wrapper;

public static class ApiResultFactory
{
    public static ApiResult ToApiResult(this Result result)
    {
        return new ApiResult
        {
            IsSuccess = result.IsSuccess,
            Messages = result.Errors.Select(error => error.Message).ToArray()
        };
    }

    public static ApiResult<T> ToApiResult<T>(this Result<T> result) where T : class
    {
        return new ApiResult<T>
        {
            IsSuccess = result.IsSuccess,
            Messages = result.Errors.Select(error => error.Message).ToArray(),
            Data = result.Value
        };
    }
}