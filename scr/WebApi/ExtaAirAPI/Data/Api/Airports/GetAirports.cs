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
				.ForMember(d => d.City, o => o.MapFrom(a => a.Address.City))
				.ForMember(d => d.Street, o => o.MapFrom(a => a.Address.Street))
				.ForMember(d => d.StreetNumber, o => o.MapFrom(a => a.Address.StreetNumber)));
				return dbContext.Airports.ProjectTo<AirportDto>().ToList();
			}
		}

		public IEnumerable<AirportDto> GetAirportsById(int id)
		{
			using (var dbContext = new ExtraAirContext())
			{
				Mapper.Initialize(cfg => cfg.CreateMap<Airport, AirportDto>()
				.ForMember(d => d.Country, o => o.MapFrom(a => a.Address.Country))
				.ForMember(d => d.City, o => o.MapFrom(a => a.Address.City)));
				var tourIds =
					dbContext.TourToAirports.ToList()
						.Where(x => !x.isInterim && x.DateStart != null && x.AirportId == id)
						.Select(x => x.TourId);
				var toursList = dbContext.TourToAirports.Where(x => !x.isInterim && x.DateFinish != null).ToList();
				var res = new List<AirportDto>();
				foreach (var tourid in tourIds)
				{
					res.AddRange(toursList.Where(x => x.TourId == tourid).Select(x => MapHepler(x.Airport)));
				}
				return res.GroupBy(p => p.AirportId) .Select(g => g.First()).ToList();
			}
		}


		private static AirportDto MapHepler(Airport airport)
		{
			return new AirportDto
			{
				AirportId = airport.AirportId,
				Name = airport.Name,
				City = airport.Address.City,
				Country = airport.Address.Country
			};
		}
	}
}