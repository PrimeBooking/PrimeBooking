namespace PrimeBooking.API.ActionResults;

public interface IActionResultCreator
{
    IActionResult GetResponse<T>(Result<T> response, CancellationToken cancellationToken);
}
