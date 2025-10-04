namespace OrderMgmt.API.Core.Entities;

public class Order
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid OrderID { get; set; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid UserID { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime OrderDate { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalBill { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}
