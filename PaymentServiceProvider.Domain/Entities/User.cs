namespace PaymentServiceProvider.Domain.Entities;

public class User : Entity
{
    // TODO Add address
    public User(string document, string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        Document = document;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Document { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}