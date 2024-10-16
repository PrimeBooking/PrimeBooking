using Microsoft.AspNetCore.Mvc;
using PrimeBooking.Application.Models;
using PrimeBooking.Application.Repositories;
using PrimeBooking.Domain.Common;
using PrimeBooking.Domain.Hotel;
using PrimeBooking.Domain.Hotel.Commands;

namespace PrimeBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(IEventStoreRepository<Hotel> eventStoreRepository) : Controller
{
    [HttpPost]
    public async Task<ActionResult<Result<Hotel>>> Post([FromBody] CreateHotelCommand command)
    {
        Result<Hotel> hotel = Hotel.Create(new HotelId(Guid.NewGuid()), command.Name, command.Capacity, ContactInformation.Create(command.Phone, command.Email, null).Value, command.Facilities, command.Stars);
        Result<AppendEventsResult> result = await eventStoreRepository.AppendEventsAsync(hotel.Value!);
        return Ok(result);
    }
    
}
