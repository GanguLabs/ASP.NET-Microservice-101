using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using OrderWebApi.Models;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orderCollection;
        public OrderController()
        {
            var dbhost = "localhost";
            var dbname = "dms_order";
            var connectionstring = $"mongodb://{dbhost}:27017/{dbname}";

            var mongoUrl = MongoUrl.Create(connectionstring);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);

            _orderCollection = database.GetCollection<Order>("order");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetById(string orderId)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
            return await _orderCollection.Find(filterDefinition).SingleOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            await _orderCollection.InsertOneAsync(order);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Order order)
        {
            var filterdefinition  = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filterdefinition, order);
            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> Delete(string orderId)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
            await _orderCollection.DeleteOneAsync(filterDefinition);

            return Ok();
        }
    }
}
