namespace Accommodation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(IEventStoreRepository<Hotel> eventStoreRepository, IActionResultCreator actionResultCreator) : Controller
{
    
    [HttpGet("{hotelId}")]
    public async Task<IActionResult> Get(string hotelId)
    {
        Result<StreamEvent[]> result = await eventStoreRepository.ReadEventsAsync(hotelId);
        return actionResultCreator.GetResponse(result, default);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateHotelCommand command)
    {
        Result<Hotel> hotel = Hotel.Create(new HotelId(Guid.NewGuid()), command.Name, command.Capacity, ContactInformation.Create(command.Phone, command.Email, null).Value, command.Facilities, command.Stars);
        Result<AppendEventsResult> result = await eventStoreRepository.AppendEventsAsync(hotel.Value!, 0);
        return actionResultCreator.GetResponse(result, default);
    }
    
}


//TODO:
// publishEndpoint.Send... > (MassTransit) -> Consumer (domain/command)
// Consumer -> "Validate" Command (with state), IMediator -> CommandHandler (Application, create Event + validate it) -> EventStoreRepository


// src -> Services -> Accommodation -> Accommodation.Domain (Hotel and Room)
// Booking -> 
// Payment -> 
// Feedback -> 
