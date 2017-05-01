using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.API_DTOs.Helper_DTOs;
using ExtraAirCore.Command.TourDetails;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace Data.Api.TourDetails
{
	public class GetTourDetails : IGetTourDetails
	{
		public IEnumerable<TourDetailsDto> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public TourDetailsDto Get(TourDetailsHelper helper)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var list = dbContext.TourDetailses.Where(x => x.TourId == helper.TourId && x.DateStart == helper.DateStart
											&& x.DateFinish == helper.DateFinish).ToList().Select(x => MapHelper(x, helper.ComfortType));
				var b = new List<BookedPoint>();
				var economyPass = 0;
				var businessPass = 0;

				if (!list.Any())
				{
					return null;
				}

				foreach (var l in list)
				{
					b.Concat(l.BookedPoints);
					economyPass = l.CurrentPassengerCount.CountOfEconomyPassenger > economyPass ? 
						l.CurrentPassengerCount.CountOfEconomyPassenger : economyPass;
					businessPass += l.CurrentPassengerCount.CountOfBusinessPassenger > businessPass ?
						l.CurrentPassengerCount.CountOfBusinessPassenger : businessPass;
				}
				return new TourDetailsDto
				{
					BookedPoints = b,
					CurrentPassengerCount = new CountPassengerDto
					{
						CountOfEconomyPassenger = economyPass,
						CountOfBusinessPassenger = businessPass
					}
				};
			}
		}


		private static TourDetailsDto MapHelper(ExtraAirCore.Models.EFModels.TourDetails tourDetails, ComfortType comfort)
		{
			return new TourDetailsDto
			{
				CurrentPassengerCount = new CountPassengerDto
				{
					CountOfEconomyPassenger = tourDetails.CurrentCountOfEconomyPassenger,
					CountOfBusinessPassenger = tourDetails.CurrentCountOfBusinessPassenger
				},
				BookedPoints = tourDetails.BookedPlaces.Where(b => b.ComfortType == comfort).Select(b => new BookedPoint
				{
					X = b.PointX,
					Y = b.PointY
				}).ToList()
			};
		}
	}
}