using System;
using System.Diagnostics;

namespace FClub.Model
{
	public class SeasonalProduct : Product
	{
		public SeasonalProduct(int id, string name, decimal price, bool canBeBoughtOnCredit, DateTime seasonStartDate, DateTime seasonEndDate)
			: base(id, name, price, false, canBeBoughtOnCredit)
		{
			if (seasonStartDate >= seasonEndDate)
			{
				throw new ArgumentException($"{nameof(seasonStartDate)} {nameof(SeasonEndDate)}", "Season start cannot be after or on season end");
			}

			SeasonStartDate = seasonStartDate;
			SeasonEndDate = seasonEndDate;
		}

		public DateTime SeasonStartDate { get; }
		public DateTime SeasonEndDate { get; }

		public override bool Active
		{
			get => IsActiveAt(DateTime.Now);
			set => throw new AccessViolationException($"Cannot set {nameof(Active)} for a seasonal product");
		}

		public bool IsActiveAt(DateTime dateTime)
		{
			return dateTime >= SeasonStartDate && dateTime <= SeasonEndDate;
		}
	}
}
