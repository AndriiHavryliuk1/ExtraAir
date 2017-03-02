using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.Airport;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace Data.Api.Airports
{
	public class GetAirports : IGetAirports
	{
		public IEnumerable<AirportDto> GetAllAirports()
		{
			using (var dbContext = new ExtraAirContext())
			{
				Mapper.Initialize(cfg => cfg.CreateMap<Airport, AirportDto>()
				.ForMember(d => d.Country, o => o.MapFrom(a => a.Address.Country))
				.ForMember(d => d.City, o => o.MapFrom(a => a.Address.City)));
				return dbContext.Airports.ProjectTo<AirportDto>().ToList();
			}
		}

		public IEnumerable<AirportDto> GetAirportsById(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}