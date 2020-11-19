using System;
using System.Diagnostics;

namespace FClub.Model
{
	public class SeasonalProduct : Product
    {
		public SeasonalProduct(int id, string name, decimal price, bool active, bool canBeBoughtOnCredit, DateTime seasonStartDate, DateTime seasonEndDate)
			: base(id, name, price, active, canBeBoughtOnCredit)
		{
			if (seasonStartDate >= seasonEndDate)
			{
				throw new ArgumentException($"Season start cannot be after or on season end {SeasonStartDate < seasonEndDate}", $"{nameof(seasonStartDate)} {nameof(SeasonEndDate)}");
			}

			SeasonStartDate = seasonStartDate;
			SeasonEndDate = seasonEndDate;
		}

		public DateTime SeasonStartDate { get; }
        public DateTime SeasonEndDate { get; }
    }
}
