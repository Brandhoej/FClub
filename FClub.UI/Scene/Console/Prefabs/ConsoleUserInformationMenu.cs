using FClub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FClub.UI.Scene.Console.Prefabs
{
	public class ConsoleUserInformationMenu : ConsoleBaseMenuComponent
	{
		private readonly User m_user;
		private readonly IEnumerable<Transaction> m_transactions;

		public ConsoleUserInformationMenu(User user, IEnumerable<Transaction> transactions)
		{
			m_user = user;
			m_transactions = transactions;
			Build();
		}

		private void Build()
		{
			AddChild(new ConsoleLabel(m_user));
			AddChild(new ConsoleLabel($"balance: {m_user.Balance} dkk"));

			if (m_transactions.Count() > 0)
			{
				AddChild(new ConsoleLabel("Transactions:"));
				foreach (Transaction _transaction in m_transactions)
				{
					if (_transaction is BuyTransaction _buyTransaction)
					{
						AddChild(new ConsoleLabel($"{_transaction.Date} | {_buyTransaction.GetType().Name} | {_buyTransaction.Amount} | {_buyTransaction.Product.Name}"));
					}
					else
					{
						AddChild(new ConsoleLabel($"{_transaction.Date} | {_transaction.GetType().Name} | {_transaction.Amount}"));
					}
				}
			}
			else
			{
				AddChild(new ConsoleLabel("No transactions yet"));
			}
		}
	}
}
