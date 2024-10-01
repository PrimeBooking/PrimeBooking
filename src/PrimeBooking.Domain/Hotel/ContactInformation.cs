using System.ComponentModel.DataAnnotations;
using PrimeBooking.Domain.Hotel.Errors;

namespace PrimeBooking.Domain.Hotel;

//ValueObject
public record ContactInformation
{
    [Phone(ErrorMessage = "Invalid Contact Number")]
    public string Phone { get; private set; }
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; private set; }
    public Address Address { get; private set; }
    
    public static Result<ContactInformation> Create(string phone, string email, Address address)
    {
        if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email)) 
            return Result.Failure<ContactInformation>(ContactInformationErrors.EmptyValue("Country is empty or invalid"));
        
        var addressResult = Address.Create(address.Country, address.City, address.Street, address.StateOrRegion, address.PostCode);

        if (addressResult.IsFailure) 
            return Result.Failure<ContactInformation>(addressResult.Error ?? throw new ArgumentNullException(nameof(addressResult.Error), "Error is nullable"));
        
        var contact = new ContactInformation(phone, email, addressResult.Value);
        return new Result<ContactInformation>(contact, true, null);
    }
    
    private ContactInformation()
    {
    }
    
    private ContactInformation(string phone, string email, Address address)
    {
        Phone = phone;
        Email = email;
        Address = address;
    }
}