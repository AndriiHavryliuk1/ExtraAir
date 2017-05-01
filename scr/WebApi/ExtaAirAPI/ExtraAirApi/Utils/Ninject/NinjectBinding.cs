using Data.Api.Addresses;
using Data.Api.Airports;
using Data.Api.Clients;
using Data.Api.Orders;
using Data.Api.TourDetails;
using Data.Api.Tours;
using ExtraAirCore.Command.Address;
using ExtraAirCore.Command.Airport;
using ExtraAirCore.Command.Orders;
using ExtraAirCore.Command.Tour;
using ExtraAirCore.Command.TourDetails;
using ExtraAirCore.Command.User;
using Ninject.Modules;

namespace ExtraAirApi.Utils.Ninject
{
	public class NinjectBinding : NinjectModule
	{
		public override void Load()
		{
			Bind<IGetAllAddresses>().To<GetAllAddresses>();
			Bind<ISaveAddress>().To<SaveAddress>();

			Bind<IGetUsers>().To<GetClients>();
			Bind<ISaveUser>().To<SaveUser>();

			Bind<IGetTours>().To<GetTours>();
			Bind<IGetAirports>().To<GetAirports>();

			Bind<IGetOrders>().To<GetOrders>();

			Bind<IGetTourDetails>().To<GetTourDetails>();
		}
	}
}