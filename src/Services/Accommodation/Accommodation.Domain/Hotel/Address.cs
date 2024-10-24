using System.Globalization;

namespace Accommodation.Domain.Hotel;

//ValueObject
public record Address
{
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string StateOrRegion { get; private set; }
    public string PostCode { get; private set; }

    public static Result<Address> Create(string country, string city, string street, string stateOrRegion, string postCode)
    {
        if (string.IsNullOrWhiteSpace(country) || !IsValidCountryName(country)) 
            return Result.Failure<Address>(AddressErrors.EmptyValue("Country is empty or invalid"));
                
        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Address>(AddressErrors.EmptyValue("City is empty"));
        
        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Address>(AddressErrors.EmptyValue("Street is empty"));
        
        if (string.IsNullOrWhiteSpace(stateOrRegion))
            return Result.Failure<Address>(AddressErrors.EmptyValue("State/Region is empty"));
        
        if (string.IsNullOrWhiteSpace(postCode))
            return Result.Failure<Address>(AddressErrors.EmptyValue("Post Code is empty"));
        
        Address address = new (country, street, city, stateOrRegion, postCode);
        return new Result<Address>(address, true, null);
    }

    private static bool IsValidCountryName(string country) =>
        new List<string>(CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new RegionInfo(c.Name).EnglishName))
            .Contains(country);
    
    private Address()
    {
    }

    private Address(string country, string city, string street, string stateOrRegion, string postCode)
    {
        Country = country;
        City = city;
        Street = street;
        StateOrRegion = stateOrRegion;
        PostCode = postCode;
    }
}
