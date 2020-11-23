using FClub.BLL;
using FClub.Model;
using FClub.UI;
using FClub.Controller.Command;
using FClub.Controller.Command.Parser;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System;
using System.Diagnostics;

namespace FClub.Controller
{
	public class StregsystemController : IStregsystemController
	{
		private readonly IStregsystemCommandParser m_parser;
		private readonly IStregsystem m_stregsystem;
		private readonly IStregsystemUI m_stregsystemUI;

		public StregsystemController(IStregsystem stregsystem, IStregsystemUI stregsystemUI)
		{
			m_stregsystem = stregsystem;
			m_stregsystemUI = stregsystemUI;
			m_parser = new StregsystemCommandParser(GetType());
			m_stregsystemUI.CommandEntered += StregsystemUI_CommandEntered;

			SetEndpoints();
		}

		private void SetEndpoints()
		{
			MethodInfo[] _methodInfos = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
			foreach (MethodInfo _methodInfo in _methodInfos)
			{
				SetEndpoints(_methodInfo);
			}
		}

		private void SetEndpoints(MethodInfo methodInfo)
		{
			if (methodInfo.GetCustomAttributes(typeof(RouteAttribute)) is RouteAttribute[] _routes)
			{
				SetEndpoints(methodInfo, _routes);
			}
		}

		private void SetEndpoints(MethodInfo methodInfo, RouteAttribute[] routes)
		{
			foreach (RouteAttribute _route in routes)
			{
				m_parser.Add(methodInfo.Name, _route.Path);
			}
		}

		public IStregsystemCommandResult Execute(string name, string parameters)
		{
			StregsystemCommand cmd = m_parser.Parse(name, parameters);
			return cmd?.Run(this, parameters) ?? default;
		}

		[Route("/users")]
		public IStregsystemCommandResult Users(string username)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			m_stregsystemUI.DisplayUserBuyInterface(_user, m_stregsystem.ActiveProducts);
			return new Ok();
		}

		[Route("/users/info")]
		public IStregsystemCommandResult UsersInfo(string username)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			m_stregsystemUI.DisplayUserInformation(_user, m_stregsystem.GetTransactions(_user, 10));
			return new Ok();
		}

		[Route("/buy")]
		public IStregsystemCommandResult Buy(string username, int productId, int amount = 1)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			Product _product = m_stregsystem.GetProductById(productId);
			try
			{
				BuyTransaction _buyTransaction = m_stregsystem.BuyProduct(_user, _product, amount);
				if (amount == 1)
				{
					m_stregsystemUI.DisplayUserBuysProduct(_buyTransaction);
				}
				else
				{
					m_stregsystemUI.DisplayUserBuysProduct(amount, _buyTransaction);
				}
			}
			catch (InsufficientCreditsException _insufficientCreditsException)
			{
				m_stregsystemUI.DisplayInsufficientCash(_insufficientCreditsException.User, _insufficientCreditsException.Product);
			}
			catch (Exception exception)
			{
				m_stregsystemUI.DisplayGeneralError(exception.Message);
			}
			return new Ok();
		}

		[Route("/products")]
		public IStregsystemCommandResult Products()
		{
			m_stregsystemUI.DisplayProducts(m_stregsystem.ActiveProducts);
			return new Ok();
		}

		[Route(":addcredits")]
		public IStregsystemCommandResult AddCreditsToUser(string username, decimal amount)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			m_stregsystem.AddCreditsToAccount(_user, amount);
			return new Ok();
		}

		[Route(":crediton")]
		public IStregsystemCommandResult ActivateProductCredit(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			_product.CanBeBoughtOnCredit = true;
			return new Ok();
		}

		[Route(":creditoff")]
		public IStregsystemCommandResult DeactivateProductCredit(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			_product.CanBeBoughtOnCredit = false;
			return new Ok();
		}

		[Route(":activate")]
		public IStregsystemCommandResult ActivateProduct(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			_product.Active = true;
			return new Ok();
		}

		[Route(":deactivate")]
		public IStregsystemCommandResult DeactivateProduct(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			_product.Active = false;
			return new Ok();
		}

		[Route(":q")]
		[Route(":quit")]
		public IStregsystemCommandResult Stop()
		{
			m_stregsystemUI.Stop();
			return new Ok();
		}

		private string StregsystemUI_CommandEntered(string name, string parameters)
		{
			return (Execute(name, parameters) ?? new Error()).ToString();
		}
	}
}
