using System.Collections.Generic;
using ExtraAirCore.API_DTOs;

namespace ExtraAirCore.Command.Orders
{
	public interface IGetOrders
	{
		IEnumerable<OrderDto> GetAllOrders(int userId);
		IEnumerable<OrderDto> GetFutureOrders(int userId);
		IEnumerable<OrderDto> GetLastOrders(int userId);
	}
}
