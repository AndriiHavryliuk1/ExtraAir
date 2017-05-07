using System.Collections.Generic;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.API_DTOs.Helper_DTOs;

namespace ExtraAirCore.Command.Orders
{
	public interface IGetOrders
	{
		IEnumerable<OrderDto> GetAllOrders(int userId);
		IEnumerable<OrderDto> GetFutureOrders(int userId);
		IEnumerable<OrderDto> GetLastOrders(int userId);
		object GetOrdersWithPagination(PaginFilteringHelper helper, IEnumerable<OrderDto> lis);
		OrderDetailsDto GetOrder(int orderId);
	}
}
