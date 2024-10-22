using Accommodation.Domain.Hotel;

namespace PrimeBooking.Domain.Tests.HotelDomain;

public class ContactInformationDomainTests
{
    [Theory]
    [MemberData(nameof(InvalidContactInformationData))]
    public void Create_WithInvalidBody_ShouldReturnFailureResult(Result<ContactInformation> contactInformation, Error error)
    {
        contactInformation.IsSuccess.Should().BeFalse();
        contactInformation.IsFailure.Should().BeTrue();
        contactInformation.Error.Should().BeEquivalentTo(error);
    }

    public static IEnumerable<object[]> InvalidContactInformationData =>
        new List<object[]>
        {
            new object[]
            {
                ContactInformation.Create("", "", default!),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Phone number is empty",
                    HttpStatusCode.UnprocessableEntity
                )
            },
            new object[]
            {
                ContactInformation.Create(ValidContactInformation.Value!.Phone, "", default!),
                ErrorFactory.BuildError(
                    ErrorCode.Validation,
                    ErrorType.InvalidFormat,
                    "Email address is empty",
                    HttpStatusCode.UnprocessableEntity
                )
            }
        };

    public static Result<ContactInformation> ValidContactInformation =>
        ContactInformation.Create("+14842634807", "test@gmail.com", AddressDomainTests.ValidAddress.Value!);
}