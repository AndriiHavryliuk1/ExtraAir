using System.Collections.Generic;
using ExtraAirCore.API_DTOs;

namespace ExtraAirCore.Command.SearchHistory
{
	public interface IGetSearchHistory
	{
		IEnumerable<SearchHistoryDto> GetSearchHistoryByUser(int? userId);
	}
}