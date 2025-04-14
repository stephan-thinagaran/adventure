using System;

namespace Adventure.AdventureWork.Person.Domain;

public class Person
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Person() { }
    #pragma warning restore CS8618

    public Person(
        int businessEntityId,
        string personType,
        bool nameStyle,
        string? title,
        string firstName,
        string? middleName,
        string lastName,
        string? suffix,
        int emailPromotion,
        string? emailAddress, // Added related data
        Guid rowguid,
        DateTime modifiedDate)
    {
        BusinessEntityId = businessEntityId;
        PersonType = personType ?? throw new ArgumentNullException(nameof(personType));
        NameStyle = nameStyle;
        Title = title;
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        MiddleName = middleName;
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Suffix = suffix;
        EmailPromotion = emailPromotion;
        EmailAddress = emailAddress; // Allow null email
        Rowguid = rowguid;
        ModifiedDate = modifiedDate;
    }

    // --- Properties ---

    /// <summary>
    /// Primary key for Person records. Foreign key to BusinessEntity.BusinessEntityID.
    /// Maps to: Person.Person.BusinessEntityID
    /// </summary>
    public int BusinessEntityId { get; private set; } // Or public init;

    /// <summary>
    /// Type of person (e.g., SC = Store Contact, IN = Individual customer, EM = Employee).
    /// Maps to: Person.Person.PersonType
    /// </summary>
    public string PersonType { get; private set; } = string.Empty;

    /// <summary>
    /// 0 = The data in FirstName and LastName are stored in western style (first name, last name).
    /// 1 = Eastern style (last name, first name).
    /// Maps to: Person.Person.NameStyle
    /// </summary>
    public bool NameStyle { get; private set; }

    /// <summary>
    /// A courtesy title. For example, Mr. or Ms.
    /// Maps to: Person.Person.Title (nullable)
    /// </summary>
    public string? Title { get; private set; }

    /// <summary>
    /// First name of the person.
    /// Maps to: Person.Person.FirstName
    /// </summary>
    public string FirstName { get; private set; } = string.Empty;

    /// <summary>
    /// Middle name or initial of the person.
    /// Maps to: Person.Person.MiddleName (nullable)
    /// </summary>
    public string? MiddleName { get; private set; }

    /// <summary>
    /// Last name of the person.
    /// Maps to: Person.Person.LastName
    /// </summary>
    public string LastName { get; private set; } = string.Empty;

    /// <summary>
    /// Surname suffix. For example, Sr. or Jr.
    /// Maps to: Person.Person.Suffix (nullable)
    /// </summary>
    public string? Suffix { get; private set; }

    /// <summary>
    /// 0 = Contact does not wish to receive email promotions.
    /// 1 = Contact wishes to receive email promotions from AdventureWorks.
    /// 2 = Contact wishes to receive email promotions from AdventureWorks and selected partners.
    /// Maps to: Person.Person.EmailPromotion
    /// </summary>
    public int EmailPromotion { get; private set; }

    // --- Potentially related data often fetched with a Person ---

    /// <summary>
    /// Primary email address for the person.
    /// Typically fetched via join from Person.EmailAddress.
    /// Maps to: Person.EmailAddress.EmailAddress (nullable, assuming you fetch one primary email)
    /// </summary>
    public string? EmailAddress { get; private set; }

    // --- Metadata ---

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// Maps to: Person.Person.rowguid
    /// </summary>
    public Guid Rowguid { get; private set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// Maps to: Person.Person.ModifiedDate
    /// </summary>
    public DateTime ModifiedDate { get; private set; }

    // --- Potential Domain Methods (Add as needed) ---
    // Example:
    // public string GetFullName()
    // {
    //    // Logic considering NameStyle, Title, Suffix etc.
    // }
}
