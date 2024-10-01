namespace PrimeBooking.Domain.Informative.Hotel;

//ValueObject
public record Address
{
    public string Country { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostCode { get; private set; }

    private Address()
    {
    }

    public Address(string country, string street, string city, string state, string postCode)
    {
        Country = country;
        Street = street;
        City = city;
        State = state;
        PostCode = postCode;
    }
}