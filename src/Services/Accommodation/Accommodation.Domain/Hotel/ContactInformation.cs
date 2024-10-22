namespace Accommodation.Domain.Hotel;

//ValueObject
public record ContactInformation
{
    [Phone(ErrorMessage = "Invalid Contact Number")]
    public string Phone { get; private set; }
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; private set; }
    public Address? Address { get; private set; }
    
    public static Result<ContactInformation> Create(string phone, string email, Address? address)
    {
        Result<Address>? addressResult = default;
        if (string.IsNullOrEmpty(phone)) 
            return Result.Failure<ContactInformation>(ContactInformationErrors.EmptyValue("Phone number is empty"));
        
        if (string.IsNullOrEmpty(email)) 
            return Result.Failure<ContactInformation>(ContactInformationErrors.EmptyValue("Email address is empty"));

        if (address is not null)
        {
            addressResult = Address.Create(address.Country, address.City, address.Street, address.StateOrRegion, address.PostCode);
            
            if (addressResult.IsFailure) 
                return Result.Failure<ContactInformation>(addressResult.Error ?? throw new ArgumentNullException(nameof(addressResult.Error), "Error is nullable"));
        }

        var contact = new ContactInformation(phone, email, addressResult?.Value);
        return new Result<ContactInformation>(contact, true, null);
    }
    
    private ContactInformation()
    {
    }
    
    [JsonConstructor]
    public ContactInformation(string phone, string email, Address? address)
    {
        Phone = phone;
        Email = email;
        Address = address;
    }
}
