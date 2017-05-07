using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.API_DTOs.Helper_DTOs;
using ExtraAirCore.Command.Orders;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Controllers
{
	public class OrdersController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Orders
		public object GetOrders([FromUri] int userId, [FromUri] string type = "All", [FromUri]int page = 1, [FromUri]int itemsPerPage = 15, [FromUri]string search = null)
		{
			switch (type)
			{
				case "All":
					{
						var list = IoC.Get<IGetOrders>().GetAllOrders(userId);
						return IoC.Get<IGetOrders>().GetOrdersWithPagination(new PaginFilteringHelper()
						{
							ItemsPerPage = itemsPerPage,
							Page = page,
							Search = search,
							Type = type
						}, list);
					}
				case "Future":
					{
						var list = IoC.Get<IGetOrders>().GetFutureOrders(userId);
						return IoC.Get<IGetOrders>().GetOrdersWithPagination(new PaginFilteringHelper()
						{
							ItemsPerPage = itemsPerPage,
							Page = page,
							Search = search,
							Type = type
						}, list);
					}
				case "Last":
					{
						var list = IoC.Get<IGetOrders>().GetLastOrders(userId);
						return IoC.Get<IGetOrders>().GetOrdersWithPagination(new PaginFilteringHelper()
						{
							ItemsPerPage = itemsPerPage,
							Page = page,
							Search = search,
							Type = type
						}, list);
					}
				default:
					break;
			}

			return null;
		}

		// GET: api/Orders/5
		[ResponseType(typeof(Order))]
		public IHttpActionResult GetOrder(int id)
		{
			return Ok(IoC.Get<IGetOrders>().GetOrder(id));
		}

		// PUT: api/Orders/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutOrder(int id, Order order)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != order.OrderId)
			{
				return BadRequest();
			}

			db.Entry(order).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Orders
		[ResponseType(typeof(Order))]
		public IHttpActionResult PostOrder(Order order)
		{
			var list = db.Tours.ToList();
			order.Tours = list.Where(l => order.Tours.Any(x => x.TourId == l.TourId)).ToList();

			db.Orders.Add(order);
			db.SaveChanges();

			return Ok();
		}

		// DELETE: api/Orders/5
		[ResponseType(typeof(Order))]
		public IHttpActionResult DeleteOrder(int id)
		{
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return NotFound();
			}

			db.Orders.Remove(order);
			db.SaveChanges();

			return Ok(order);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool OrderExists(int id)
		{
			return db.Orders.Count(e => e.OrderId == id) > 0;
		}
	}
}