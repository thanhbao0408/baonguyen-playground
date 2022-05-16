namespace BN.CleanArchitecture.Core.Domain.ValueObjects;

// Learn more: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objectsact class ValueObject
// https://dev.to/antoniofalcao/best-practice-c-9-records-as-ddd-value-objects-with-ef-core-6-502p
public record ValueObject
{
    // This implementation was kept only to specify the signature of its derivations,
    // but it is no longer useful for it, since the comparison operator overrides are no longer necessary.
}

/* -- Example of implementing this using 

public record Address : Abstractions.ValueObject
{
    // Empty constructor in this case is required by EF Core,
    // because has a complex type as a parameter in the default constructor.
    private Address() { }

    public Address(Street street, string zipCode)
        => (Street, ZipCode) = (street, zipCode);

    public Street Street { get; private init; }
    public string ZipCode { get; private init; }
}

public class Person : Entity<Guid>
{
    public Person(string name, int age) 
        => (Name, Age) = (name, age);

    public string Name { get; }
    public int Age { get; }
    public Address Address { get; private set; }

    public void DefineAddress(Address address)
    {
        if (address is null) throw new BusinessException("Home address must be informed");
        if(address.Equals(Address)) return;
        Address = address;
    }
}
*/