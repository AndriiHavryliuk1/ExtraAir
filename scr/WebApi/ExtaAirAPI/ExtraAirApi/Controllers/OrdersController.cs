using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.Command.Orders;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Controllers
{
	public class OrdersController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Orders
		public object GetOrders([FromUri] int userId, [FromUri] string type)
		{
			switch (type)
			{
				case "All":
				{
					return IoC.Get<IGetOrders>().GetAllOrders(userId);
				}
				case "Future":
				{
					return IoC.Get<IGetOrders>().GetFutureOrders(userId);
				}
				case "Last":
				{
					return IoC.Get<IGetOrders>().GetLastOrders(userId);
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
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return NotFound();
			}

			return Ok(order);
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