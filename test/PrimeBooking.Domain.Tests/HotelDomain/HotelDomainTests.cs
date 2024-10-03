namespace PrimeBooking.Domain.Tests.HotelDomain;

public class HotelDomainTests
{
    [Theory]
    [MemberData(nameof(InvalidHotels))]
    public void Create_WithInvalidBody_ShouldReturnFailureResult(Result<Hotel.Hotel> hotel, Error error)
    {
        hotel.IsSuccess.Should().BeFalse();
        hotel.IsFailure.Should().BeTrue();
        hotel.Error.Should().BeEquivalentTo(error);
    }

    [Fact]
    public void Create_WithValidBody_ShouldReturnSuccessResult()
    {
        var hotel = Hotel.Hotel.Create(
            new HotelId(Guid.NewGuid()),
            "Hotel 1", 
            200, 
            ContactInformationDomainTests.ValidContactInformation.Value!, 
            new List<Facility>() { Facility.Restaurant }, 
            new List<Star>() { Star.Five }
        );
        
        hotel.IsSuccess.Should().BeTrue();
        hotel.IsFailure.Should().BeFalse();
        hotel.Error.Should().BeNull();
        hotel.Value.Should().NotBeNull();
        hotel.Value!.GetDomainEvents().Should().HaveCount(1);
        hotel.Value!.GetDomainEvents().First().Should().BeOfType<HotelCreatedEvent>(); 
    }
    
    [Fact]
    public void Delete_WithInvalidId_ShouldReturnFailureResult()
    {
        var expectedError = ErrorFactory.BuildError(
            ErrorCode.Validation,
            ErrorType.InvalidFormat,
            "Guid can't be empty",
            HttpStatusCode.UnprocessableEntity
        );
        
        var result = ValidHotel.Value!.Delete(new HotelId(Guid.Empty));
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(expectedError);
    }

    [Fact]
    public void Delete_WithValidId_ShouldBeSuccessful()
    {
        var hotel = ValidHotel.Value!;
        
        var result = hotel.Delete(new HotelId(Guid.NewGuid()));
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        hotel.GetDomainEvents().Should().HaveCount(1);
        hotel.GetDomainEvents().First().Should().BeOfType<HotelDeletedEvent>(); 
    }
    
        
    [Fact]
    public void UpdateContactInformation_WithValidContactInformation_ShouldReturnSuccessResult()
    {
        var hotel = ValidHotel.Value!;
        var contactInformation = ContactInformationDomainTests.ValidContactInformation.Value;
        
        var result = hotel.UpdateContactInformation(contactInformation!);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Hotel.Hotel>().ContactInformation.Should().BeEquivalentTo(contactInformation);
        result.Value!.GetDomainEvents().First().Should().BeOfType<ContactInformationUpdatedEvent>();
    }
    
    [Fact]
    public void UpdateCapacity_WithZeroCapacity_ShouldReturnFailureResult()
    {
        var hotel = ValidHotel.Value!;
        var expectedError = ErrorFactory.BuildError(
            ErrorCode.Validation,
            ErrorType.InvalidFormat,
            "Capacity can't be less or equal zero",
            HttpStatusCode.UnprocessableEntity
        );
        
        var result = hotel.UpdateCapacity(0);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(expectedError);
    }
    
    [Fact]
    public void UpdateCapacity_WithValidCapacity_ShouldReturnSuccessResult()
    {
        var hotel = ValidHotel.Value!;
        
        var result = hotel.UpdateCapacity(300);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Hotel.Hotel>().Capacity.Should().Be(300);
        result.Value!.GetDomainEvents().First().Should().BeOfType<CapacityUpdatedEvent>(); 
    }
    
    [Fact]
    public void UpdateFacilities_WithEmptyFacilities_ShouldReturnFailureResult()
    {
        var expectedError = ErrorFactory.BuildError(
            ErrorCode.Validation,
            ErrorType.InvalidFormat,
            "Facilities can't be empty or null",
            HttpStatusCode.UnprocessableEntity
        );
        
        var result = ValidHotel.Value!.UpdateFacilities([]);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(expectedError);
    }
        
    [Fact]
    public void UpdateFacilities_WithValidFacilities_ShouldReturnSuccessResult()
    {
        var hotel = ValidHotel.Value!;
        var updatedFacilities = new[] { Facility.Restaurant, Facility.Gym };
        
        var result = hotel.UpdateFacilities(updatedFacilities);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Hotel.Hotel>().Facilities.Should().BeEquivalentTo(updatedFacilities);
        result.Value!.GetDomainEvents().First().Should().BeOfType<FacilitiesUpdatedEvent>(); 
    }
    
    [Fact]
    public void UpdateStars_WithEmptyStars_ShouldReturnFailureResult()
    {
        var expectedError = ErrorFactory.BuildError(
            ErrorCode.Validation,
            ErrorType.InvalidFormat,
            "Stars can't be empty or null",
            HttpStatusCode.UnprocessableEntity
        );
        
        var result = ValidHotel.Value!.UpdateStars([]);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(expectedError);
    }
    
    [Fact]
    public void UpdateStars_WithValidStars_ShouldReturnSuccessResult()
    {
        var hotel = ValidHotel.Value!;
        var updatedStars = new[] { Star.Four, Star.Five };
        
        var result = hotel.UpdateStars(updatedStars);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Hotel.Hotel>().Stars.Should().BeEquivalentTo(updatedStars);
        result.Value!.GetDomainEvents().First().Should().BeOfType<StarsUpdatedEvent>(); 
    }
    
    public static IEnumerable<object[]> InvalidHotels =>
        new List<object[]>
        {
            new object[]
            {
                Hotel.Hotel.Create(new HotelId(Guid.Empty), "", 0, default!, [], []),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Guid can't be empty",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                Hotel.Hotel.Create(ValidHotel.Value!.Id, "", 0, default!, [], []),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Hotel Name can't be null or empty",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                Hotel.Hotel.Create(ValidHotel.Value!.Id, ValidHotel.Value!.Name, 0, default!, [], []),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Capacity can't be less or equal zero",
                    HttpStatusCode.UnprocessableEntity
                )
            }
        };
    
    private static Result<Hotel.Hotel> ValidHotel 
    {
        get
        {
            var hotel = Hotel.Hotel.Create(
                new HotelId(Guid.NewGuid()),
                "Hotel 1", 
                200, 
                ContactInformationDomainTests.ValidContactInformation.Value!, 
                new List<Facility> { Facility.Restaurant }, 
                new List<Star> { Star.Five }
            );
        
            hotel.Value!.ClearDomainEvents(); 

            return hotel;
        }
    }
}