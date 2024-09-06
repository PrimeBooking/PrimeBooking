namespace PrimeBooking.Domain.Informative.Hotel;

//ValueObject
public record ContactInformation
{
    //Regex
    public string Phone { get; private set; }
    public Address Address { get; private set; }
    public string Email { get; private set; }
    
    private ContactInformation()
    {
    }
    
    public ContactInformation(string phone, Address address, string email)
    {
        Phone = phone;
        Address = address;
        Email = email;
    }
}