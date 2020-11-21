using FClub.BLL;
using FClub.Model;
using FClub.UI;
using FClub.Controller.Command;
using FClub.Controller.Command.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Controller
{
	public interface IStregsystemController
	{
		public bool Execute(string input);
	}

	public class StregsystemController : IStregsystemController
	{
		private readonly IStregsystemUI m_stregsystemUI;
		private readonly IStregsystem m_stregsystem;
		private readonly IStregsystemCommandParser m_parser;

		public StregsystemController(IStregsystemUI stregsystemUI, IStregsystem stregsystem)
		{
			m_stregsystemUI = stregsystemUI;
			m_stregsystem = stregsystem;
			m_parser = new StregsystemCommandParser()
			{
				/* Formatting:
				 * ':' is a command with NO view change expected 
				 * '/' is a command with view change expected */

				// Admin
				new StregsystemCommand(":q", parameters			 => Stop(parameters)),
				new StregsystemCommand(":quit", parameters		 => Stop(parameters)),
				new StregsystemCommand(":activate", parameters   => ActiveProduct(parameters)),
				new StregsystemCommand(":deactivate", parameters => DeactivateProduct(parameters)),
				new StregsystemCommand(":crediton", parameters	 => ActivateProductCredit(parameters)),
				new StregsystemCommand(":creditoff", parameters  => DeactivateProductCredit(parameters)),
				new StregsystemCommand(":addcredits", parameters => AddCreditsToUser(parameters)),

				// User
				new StregsystemCommand("/user", parameters => User(parameters)),
				new StregsystemCommand("/buy", parameters  => Buy(parameters)),
			};

			m_stregsystemUI.CommandEntered += StregsystemUI_CommandEntered;
		}

		public bool Execute(string input)
		{
			StregsystemCommand _cmd = m_parser.Parse(input);
			return _cmd.Run(input);
		}

		private bool Buy(params object[] parameters)
		{
			// Single buy
			if (parameters.Length == 2)
			{
				BuyTransaction _buyTransaction = SingleBuy(parameters);
				if (_buyTransaction != null)
				{
					m_stregsystemUI.DisplayUserBuysProduct(_buyTransaction);
					return true;
				}
			}
			else if (parameters.Length == 3)
			{
				BuyTransaction _buyTransaction = MultiBuy(parameters);
				if (_buyTransaction != null)
				{
					m_stregsystemUI.DisplayUserBuysProduct(_buyTransaction);
					return true;
				}
			}
			return false;
		}

		private BuyTransaction SingleBuy(params object[] parameters)
		{
			if (parameters[0] is string == false)
			{
				m_stregsystemUI.DisplayGeneralError("Username parameter is not of type 'string'");
				return default;
			}

			User _user = m_stregsystem.GetUserByUsername(parameters[0].ToString());
			if (_user == null)
			{
				m_stregsystemUI.DisplayUserNotFound(parameters[0].ToString());
				return default;
			}

			if (parameters[1] is double == false)
			{
				m_stregsystemUI.DisplayGeneralError("ProductId parameter is not of type 'int'");
				return default;
			}

			Product product = m_stregsystem.GetProductById((int)parameters[1]);
			if (product == null)
			{
				m_stregsystemUI.DisplayProductNotFound(parameters[1].ToString());
				return default;
			}

			try
			{
				BuyTransaction _buyTransaction = m_stregsystem.BuyProduct(_user, product);
				m_stregsystemUI.DisplayUserBuysProduct(_buyTransaction);
				return _buyTransaction;
			}
			catch (InsufficientCreditsException)
			{
				m_stregsystemUI.DisplayInsufficientCash(_user, product);
				return default;
			}
		}

		private BuyTransaction MultiBuy(params object[] parameters)
		{
			if (parameters[0] is string == false)
			{
				m_stregsystemUI.DisplayGeneralError("Username parameter is not of type 'string'");
				return default;
			}

			User _user = m_stregsystem.GetUserByUsername(parameters[0].ToString());
			if (_user == null)
			{
				m_stregsystemUI.DisplayUserNotFound(parameters[0].ToString());
				return default;
			}

			if (parameters[1] is double == false)
			{
				m_stregsystemUI.DisplayGeneralError("AmountToBuy parameter is not of type 'int'");
				return default;
			}

			if (parameters[2] is double == false)
			{
				m_stregsystemUI.DisplayGeneralError("ProductId parameter is not of type 'int'");
				return default;
			}

			Product _product = m_stregsystem.GetProductById((int)parameters[2]);
			if (_product == null)
			{
				m_stregsystemUI.DisplayProductNotFound(parameters[2].ToString());
				return default;
			}

			try
			{
				int _amount = (int)parameters[1];
				BuyTransaction _buyTransaction = default;
				for (int i = 0; i < _amount; i++)
				{
					_buyTransaction = m_stregsystem.BuyProduct(_user, _product);
					m_stregsystemUI.DisplayUserBuysProduct(_buyTransaction);
				}
				return _buyTransaction;
			}
			catch (InsufficientCreditsException)
			{
				m_stregsystemUI.DisplayInsufficientCash(_user, _product);
				return default;
			}
		}

		private bool User(params object[] parameters)
		{
			if (parameters.Length != 1)
			{
				m_stregsystemUI.DisplayGeneralError("ProductId parameter is not of type 'int'");
				return false;
			}

			if (parameters[0] is string == false)
			{
				return false;
			}

			User _user = m_stregsystem.GetUserByUsername((string)parameters[0]);
			if (_user == null)
			{
				m_stregsystemUI.DisplayUserNotFound((string)parameters[0]);
			}

			m_stregsystemUI.DisplayUserInfo(_user);
			return true;
		}

		private bool AddCreditsToUser(params object[] parameters)
		{
			/* parser does not support decimal we therefore 
			 * check for double and then explicitly convert it */
			if (parameters.Length != 2 ||
				parameters[0] is string == false ||
				parameters[1] is double == false)
			{
				return false;
			}

			User _user = m_stregsystem.GetUserByUsername((string)parameters[0]);
			if (_user == null)
			{
				return false;
			}

			try
			{
				m_stregsystem.AddCreditsToAccount(_user, (decimal)parameters[1]);
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool ActivateProductCredit(params object[] parameters)
		{
			if (parameters.Length != 1 ||
				parameters[0] is double == false)
			{
				return false;
			}

			Product _product = m_stregsystem.GetProductById((int)parameters[0]);
			if (_product == null)
			{
				return false;
			}

			try
			{
				_product.CanBeBoughtOnCredit = true;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool DeactivateProductCredit(params object[] parameters)
		{
			if (parameters.Length != 1 ||
				parameters[0] is double == false)
			{
				return false;
			}

			Product _product = m_stregsystem.GetProductById((int)parameters[0]);
			if (_product == null)
			{
				return false;
			}

			try
			{
				_product.CanBeBoughtOnCredit = false;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool ActiveProduct(params object[] parameters)
		{
			if (parameters.Length != 1 ||
				parameters[0] is double == false)
			{
				return false;
			}

			Product _product = m_stregsystem.GetProductById((int)parameters[0]);
			if (_product == null)
			{
				return false;
			}

			try
			{
				_product.Active = false;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool DeactivateProduct(params object[] parameters)
		{
			if (parameters.Length != 1 ||
				parameters[0] is double == false)
			{
				return false;
			}

			Product _product = m_stregsystem.GetProductById((int)parameters[0]);
			if (_product == null)
			{
				return false;
			}

			try
			{
				_product.Active = false;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool Stop(params object[] parameters)
		{
			if (parameters.Length > 0)
			{
				return false;
			}

			try
			{
				m_stregsystemUI.Stop();
			}
			catch
			{
				return false;
			}
			return true;
		}
		
		private void StregsystemUI_CommandEntered(string input)
		{
			StregsystemCommand _cmd = m_parser.Parse(input);
			if (_cmd == null)
			{
				m_stregsystemUI.DisplayAdminCommandNotFoundMessage(input);
			}
			else if (!_cmd.Run(input))
			{
				m_stregsystemUI.DisplayGeneralError($"No success when running '{input}'");
			}
		}
	}
}
