namespace Jetstream.Proxmox.Sample.WebAPI.Api.Wrapper;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public string[] Messages { get; set; }
}

public class ApiResult<T> : ApiResult
{
    public T Data { get; set; }
}