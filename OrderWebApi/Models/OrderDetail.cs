using Microsoft.AspNetCore.Routing.Constraints;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderWebApi.Models
{
    public class OrderDetail
    {
        [BsonElement("product_id"), BsonRepresentation(BsonType.Int32)]
        public int ProductId { get; set; }
        [BsonElement("quantity"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Quantity { get; set; }
        [BsonElement("unit_price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal UnitPrice { get; set; }
    }
}
