namespace Accommodation.Application.Handlers;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand>
{
    public async ValueTask<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
