namespace OrderMgmt.API.Core.Entities;

public class OrderItem
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid ProductID { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal UnitPrice { get; set; }

    [BsonRepresentation(BsonType.Int32)]
    public int Quantity { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalPrice { get; set; }
}
