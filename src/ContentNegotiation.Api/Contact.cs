using CsvHelper.Configuration.Attributes;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ContentNegotiation.Api;

//[DataContract]
//public sealed record Contact
//(
//    [property: DataMember(Order = 1, Name = "Identifier")]
//    [Index(1), Name("Identifier")]
//    Guid Id,

//    [property: DataMember(Order = 2, Name = "ContactName")]
//    [Index(2), Name("Contact name")]
//    string Name,

//    [property: DataMember(Order = 3, Name = "Amount")]
//    [Index(3), Name("Amount owed")]
//    decimal AmountOwed
//);

[DataContract]
public sealed record Contact
{
    /// <summary>
    /// The id 
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [DataMember(Order = 1, Name = "Identifier")]
    [Index(1), Name("Identifier")]
    public Guid Id { get; set; }

    /// <summary>
    /// The name
    /// </summary>
    /// <example>John Doe</example>
    [property: DataMember(Order = 2, Name = "ContactName")]
    [Index(2), Name("Contact name")]
    public string Name { get; set; } = "";

    public Contact(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
