using Mediator;
using PrimeBooking.Domain.Hotel.Commands;

namespace PrimeBooking.Application.Handlers;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand>
{
    public async ValueTask<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
