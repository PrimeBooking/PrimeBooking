using System.ComponentModel.DataAnnotations;
using PrimeBooking.Domain.Informative.Common;

namespace PrimeBooking.Domain.Informative.Hotel;

//ValueObject
public record ContactInformation
{
    [Phone(ErrorMessage = "Invalid Contact Number")]
    public string Phone { get; private set; }
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; private set; }
    public Address Address { get; private set; }
    
    private ContactInformation()
    {
    }
    
    public ContactInformation(string phone, string email, Address address)
    {
        Phone = phone;
        Email = email;
        Address = address;
    }
}