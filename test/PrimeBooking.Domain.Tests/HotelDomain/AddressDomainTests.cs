using Accommodation.Domain.Hotel;

namespace PrimeBooking.Domain.Tests.HotelDomain;

public class AddressDomainTests
{
    [Theory]
    [MemberData(nameof(InvalidAddresses))]
    public void Create_WithInvalidBody_ShouldReturnFailureResult(Result<Address> address, Error error)
    {
        address.IsSuccess.Should().BeFalse();
        address.IsFailure.Should().BeTrue();
        address.Error.Should().BeEquivalentTo(error);
    }
    
    [Fact]
    public void Create_WithValidBody_ShouldReturnSuccessResult()
    {
        ValidAddress.IsSuccess.Should().BeTrue();
        ValidAddress.IsFailure.Should().BeFalse();
        ValidAddress.Error.Should().BeNull();
        ValidAddress.Value.Should().NotBeNull();
    }

    public static IEnumerable<object[]> InvalidAddresses =>
        new List<object[]>
        {
            new object[]
            {
                Address.Create("Country", "", "", "", ""),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Country is empty or invalid",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                Address.Create(ValidAddress.Value!.Country, "", "", "", ""),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "City is empty",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                Address.Create(ValidAddress.Value!.Country, ValidAddress.Value!.City, "", "", ""),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Street is empty",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                Address.Create(ValidAddress.Value!.Country, ValidAddress.Value!.City, ValidAddress.Value!.Street, "", ""),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "State/Region is empty",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                Address.Create(ValidAddress.Value!.Country, ValidAddress.Value!.City, ValidAddress.Value!.Street, ValidAddress.Value!.StateOrRegion, ""),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Post Code is empty",
                    HttpStatusCode.UnprocessableEntity
                )
            }
        };
    
    public static Result<Address> ValidAddress =>
        Address.Create("France", "Paris", "Bordeaux 12", "France", "1234");
}
