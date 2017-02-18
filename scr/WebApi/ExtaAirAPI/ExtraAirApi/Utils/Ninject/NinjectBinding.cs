﻿using Data.Api.Addresses;
using Data.Api.Client;
using ExtraAirCore.Command.Address;
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
		}
	}
}