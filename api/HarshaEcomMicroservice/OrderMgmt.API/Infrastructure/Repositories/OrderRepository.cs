
using MongoDB.Driver;

namespace OrderMgmt.API.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders;
    private readonly string collectionName = "orders";

    public OrderRepository(IMongoDatabase mongoDatabase)
    {
        _orders = mongoDatabase.GetCollection<Order>(collectionName);
    }

    public async Task<Order?> GetOrderByIdAsycn(Guid id)
    {
        var filter = Builders<Order>.Filter.Eq(order => order.OrderID, id);

        var order = await _orders.Find(filter).FirstOrDefaultAsync();

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(GetOrdersFilter filter)
    {
        var filterList = new List<FilterDefinition<Order>>();
        var filterBuilder = Builders<Order>.Filter;

        if (filter.UserId.HasValue)
        {
            filterList.Add(filterBuilder.Eq(o => o.UserID, filter.UserId));
        }
        if (filter.OrderDate.HasValue)
        {
            filterList.Add(filterBuilder.Eq(o => o.OrderDate, filter.OrderDate));
        }

        var mongoFilter = filterList.Any() ? 
            filterBuilder.And(filterList) : 
            filterBuilder.Empty;

        var orders = await _orders.Find(mongoFilter).ToListAsync();

        return orders;
    }

    public async Task AddOrderAsync(Order order)
    {
        await _orders.InsertOneAsync(order);
    }
}
