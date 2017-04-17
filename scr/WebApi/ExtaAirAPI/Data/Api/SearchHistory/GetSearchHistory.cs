using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.SearchHistory;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace Data.Api.SearchHistory
{
	public class GetSearchHistory : IGetSearchHistory
	{
		public IEnumerable<SearchHistoryDto> GetSearchHistoryByUser(int? userId)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var searchHistories = dbContext.TourSearchHistories.Where(x => x.User.UserId == userId).ToList();
				var airportsFromId = searchHistories.Select(x => x.AirportFromId);
				var airportsToId = searchHistories.Select(x => x.AirportToId);
				var airportsFrom =  dbContext.Airports.Where(x => airportsFromId.Any(a => a == x.AirportId)).ToList();
				var airportsTo = dbContext.Airports.Where(x => airportsToId.Any(a => a == x.AirportId)).ToList();

				return searchHistories.Select(x => SearchHistoryHelper(x, airportsFrom, airportsTo)).ToList();
			}
		}

		private SearchHistoryDto SearchHistoryHelper(TourSearchHistory searchHistory, List<Airport> airportsFrom, 
			List<Airport> airportsTo)
		{
			Mapper.Initialize(cfg => cfg.CreateMap<Airport, AirportDto>()
				.ForMember(d => d.Country, o => o.MapFrom(a => a.Address.Country))
				.ForMember(d => d.City, o => o.MapFrom(a => a.Address.City)));
			return new SearchHistoryDto
			{
				UserId = searchHistory.UserId,
				DateStart = searchHistory.DateStart,
				DateSearch = searchHistory.DateSearch,
				PassengerCount = searchHistory.PassengerCount.Value,
				AirportFrom = airportsFrom
				.Select(x => new AirportDto()
					{
						AirportId = x.AirportId,
						Name = x.Name,
						Country = x.Address.Country,
						City = x.Address.City
					}).FirstOrDefault(x => x.AirportId == searchHistory.AirportFromId),
				AirportTo = airportsTo
				.Select(x => new AirportDto()
				{
					AirportId = x.AirportId,
					Name = x.Name,
					Country = x.Address.Country,
					City = x.Address.City
				}).FirstOrDefault(x => x.AirportId == searchHistory.AirportToId)
			};
		}
	}
}