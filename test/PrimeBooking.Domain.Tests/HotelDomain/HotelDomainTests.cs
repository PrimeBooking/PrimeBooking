using Accommodation.Domain.Hotel;
using PrimeBooking.Domain.Hotel.Events;

namespace PrimeBooking.Domain.Tests.HotelDomain;

public class HotelDomainTests
{
    [Theory]
    [MemberData(nameof(InvalidHotels))]
    public void Create_WithInvalidBody_ShouldReturnFailureResult(Result<Accommodation.Domain.Hotel.Hotel> hotel, Error error)
    {
        hotel.IsSuccess.Should().BeFalse();
        hotel.IsFailure.Should().BeTrue();
        hotel.Error.Should().BeEquivalentTo(error);
    }

    [Fact]
    public void Create_WithValidBody_ShouldReturnSuccessResult()
    {
        Result<Accommodation.Domain.Hotel.Hotel> hotel = Accommodation.Domain.Hotel.Hotel.Create(
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
        Error expectedError = ErrorFactory.BuildError(
            ErrorCode.Validation,
            ErrorType.InvalidFormat,
            "Guid can't be empty",
            HttpStatusCode.UnprocessableEntity
        );
        
        Result result = ValidHotel.Value!.Delete(new HotelId(Guid.Empty));
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(expectedError);
    }

    [Fact]
    public void Delete_WithValidId_ShouldBeSuccessful()
    {
        Accommodation.Domain.Hotel.Hotel hotel = ValidHotel.Value!;
        
        Result result = hotel.Delete(new HotelId(Guid.NewGuid()));
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        hotel.GetDomainEvents().Should().HaveCount(1);
        hotel.GetDomainEvents().First().Should().BeOfType<HotelDeletedEvent>(); 
    }
    
        
    [Fact]
    public void UpdateContactInformation_WithValidContactInformation_ShouldReturnSuccessResult()
    {
        Accommodation.Domain.Hotel.Hotel hotel = ValidHotel.Value!;
        ContactInformation? contactInformation = ContactInformationDomainTests.ValidContactInformation.Value;
        
        Result<Accommodation.Domain.Hotel.Hotel> result = hotel.UpdateContactInformation(contactInformation!);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Accommodation.Domain.Hotel.Hotel>().ContactInformation.Should().BeEquivalentTo(contactInformation);
        result.Value!.GetDomainEvents().First().Should().BeOfType<ContactInformationUpdatedEvent>();
    }
    
    [Fact]
    public void UpdateCapacity_WithZeroCapacity_ShouldReturnFailureResult()
    {
        Accommodation.Domain.Hotel.Hotel hotel = ValidHotel.Value!;
        Error expectedError = ErrorFactory.BuildError(
            ErrorCode.Validation,
            ErrorType.InvalidFormat,
            "Capacity can't be less or equal zero",
            HttpStatusCode.UnprocessableEntity
        );
        
        Result<Accommodation.Domain.Hotel.Hotel> result = hotel.UpdateCapacity(0);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(expectedError);
    }
    
    [Fact]
    public void UpdateCapacity_WithValidCapacity_ShouldReturnSuccessResult()
    {
        Accommodation.Domain.Hotel.Hotel hotel = ValidHotel.Value!;
        
        Result<Accommodation.Domain.Hotel.Hotel> result = hotel.UpdateCapacity(300);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Accommodation.Domain.Hotel.Hotel>().Capacity.Should().Be(300);
        result.Value!.GetDomainEvents().First().Should().BeOfType<CapacityUpdatedEvent>(); 
    }
    
    [Fact]
    public void AddFacility_WithValidFacility_ShouldReturnSuccessResult()
    {
        Accommodation.Domain.Hotel.Hotel hotel = ValidHotel.Value!;
        Facility[] updatedFacilities = [Facility.Restaurant, Facility.Gym];
        
        Result<Accommodation.Domain.Hotel.Hotel> result = hotel.AddFacility(Facility.Gym);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Accommodation.Domain.Hotel.Hotel>().Facilities.Should().BeEquivalentTo(updatedFacilities);
        result.Value!.GetDomainEvents().First().Should().BeOfType<FacilityAddedEvent>(); 
    }
    
    [Fact]
    public void UpdateFacilities_WithEmptyFacilities_ShouldReturnFailureResult()
    {
        Error expectedError = ErrorFactory.BuildError(
            ErrorCode.Validation,
            ErrorType.InvalidFormat,
            "Facilities can't be empty or null",
            HttpStatusCode.UnprocessableEntity
        );
        
        Result<Accommodation.Domain.Hotel.Hotel> result = ValidHotel.Value!.UpdateFacilities([]);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(expectedError);
    }
        
    [Fact]
    public void UpdateFacilities_WithValidFacilities_ShouldReturnSuccessResult()
    {
        Accommodation.Domain.Hotel.Hotel hotel = ValidHotel.Value!;
        Facility[] updatedFacilities = [Facility.Restaurant, Facility.Gym];
        
        Result<Accommodation.Domain.Hotel.Hotel> result = hotel.UpdateFacilities(updatedFacilities);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Accommodation.Domain.Hotel.Hotel>().Facilities.Should().BeEquivalentTo(updatedFacilities);
        result.Value!.GetDomainEvents().First().Should().BeOfType<FacilitiesUpdatedEvent>(); 
    }
    
    [Fact]
    public void AddStar_WithValidStar_ShouldReturnSuccessResult()
    {
        Accommodation.Domain.Hotel.Hotel hotel = ValidHotel.Value!;
        Star[] updatedStars = [Star.Five, Star.Four];
        
        Result<Accommodation.Domain.Hotel.Hotel> result = hotel.AddStar(Star.Four);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Accommodation.Domain.Hotel.Hotel>().Stars.Should().BeEquivalentTo(updatedStars);
        result.Value!.GetDomainEvents().First().Should().BeOfType<StarAddedEvent>(); 
    }
    
    [Fact]
    public void UpdateStars_WithEmptyStars_ShouldReturnFailureResult()
    {
        Error expectedError = ErrorFactory.BuildError(
            ErrorCode.Validation,
            ErrorType.InvalidFormat,
            "Stars can't be empty or null",
            HttpStatusCode.UnprocessableEntity
        );
        
        Result<Accommodation.Domain.Hotel.Hotel> result = ValidHotel.Value!.UpdateStars([]);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(expectedError);
    }
    
    [Fact]
    public void UpdateStars_WithValidStars_ShouldReturnSuccessResult()
    {
        Accommodation.Domain.Hotel.Hotel hotel = ValidHotel.Value!;
        Star[] updatedStars = [Star.Four, Star.Five];
        
        Result<Accommodation.Domain.Hotel.Hotel> result = hotel.UpdateStars(updatedStars);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.As<Accommodation.Domain.Hotel.Hotel>().Stars.Should().BeEquivalentTo(updatedStars);
        result.Value!.GetDomainEvents().First().Should().BeOfType<StarsUpdatedEvent>(); 
    }
    
    public static IEnumerable<object[]> InvalidHotels =>
        new List<object[]>
        {
            new object[]
            {
                Accommodation.Domain.Hotel.Hotel.Create(new HotelId(Guid.Empty), "", 0, default!, [], []),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Guid can't be empty",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                Accommodation.Domain.Hotel.Hotel.Create(ValidHotel.Value!.Id, "", 0, default!, [], []),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Hotel Name can't be null or empty",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                Accommodation.Domain.Hotel.Hotel.Create(ValidHotel.Value!.Id, ValidHotel.Value!.Name, 0, default!, [], []),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Capacity can't be less or equal zero",
                    HttpStatusCode.UnprocessableEntity
                )
            }
        };
    
    private static Result<Accommodation.Domain.Hotel.Hotel> ValidHotel 
    {
        get
        {
            Result<Accommodation.Domain.Hotel.Hotel> hotel = Accommodation.Domain.Hotel.Hotel.Create(
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
