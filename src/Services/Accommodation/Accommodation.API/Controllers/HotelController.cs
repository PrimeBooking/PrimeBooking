namespace Accommodation.API.Controllers;

/// <summary>
/// Controller for Hotel operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HotelController(IActionResultCreator actionResultCreator, IMediator mediator) : Controller
{
    
    /// <summary>
    /// Creates a new Hotel entry.
    /// </summary>
    /// <param name="command">
    /// Command to create a new Hotel.
    /// </param>
    /// <returns>
    /// Returns ActionResult with EventStoreResponse.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
    {
        // Result<Hotel> hotel = Hotel.Create(new HotelId(Guid.NewGuid()), command.Name, command.Capacity, ContactInformation.Create(command.Phone, command.Email, null).Value, command.Facilities, command.Stars);
        // Result<AppendEventsResult> result = await eventStoreRepository.AppendEventsAsync(hotel.Value!, 0);
        // return actionResultCreator.GetResponse(result, default);

        return Accepted();
    }
}
