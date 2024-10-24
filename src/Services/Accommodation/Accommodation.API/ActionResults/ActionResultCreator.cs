namespace Accommodation.API.ActionResults;

public class ActionResultCreator : IActionResultCreator
{
    public IActionResult GetResponse<T>(Result<T> response, CancellationToken cancellationToken) =>
        new ResponseActionResult
        {
            Content = JsonSerializer.Serialize(response, BaseSerializer.Options),
            ContentType = "application/json",
            StatusCode = response.Error is null ? StatusCodes.Status200OK : (int)response.Error.StatusCode
        };
}
