using System;

namespace FClub.Model
{
	public class Product
	{
		private int m_id;

		public Product(int id, string name, decimal price, bool active, bool canBeBoughtOnCredit)
		{
			Id = id;
			Name = name;
			Price = price;
			Active = active;
			CanBeBoughtOnCredit = canBeBoughtOnCredit;
		}

		public int Id
		{
			get => m_id;
			private set
			{
				if (value < 1)
				{
					throw new ArgumentException(nameof(value), "Value cannot be less than 1");
				}

				m_id = value;
			}
		}

		public string Name { get; }
		public decimal Price { get; }

		public virtual bool Active { get; }
		public virtual bool CanBeBoughtOnCredit { get; }
	}
}
