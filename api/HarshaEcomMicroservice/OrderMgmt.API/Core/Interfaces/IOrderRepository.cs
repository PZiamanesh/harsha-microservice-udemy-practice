namespace OrderMgmt.API.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetOrderByIdAsycn(Guid id);
    Task<IEnumerable<Order>> GetOrdersAsync(GetOrdersFilter filter);
    Task AddOrderAsync(Order order);
}
