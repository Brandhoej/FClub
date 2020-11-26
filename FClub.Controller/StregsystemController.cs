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
using FClub.UI.Scene;
using FClub.Core;

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
			if (methodInfo.GetCustomAttributes(typeof(RouteAttribute)) is IEnumerable<RouteAttribute> _routes)
			{
				SetEndpoints(methodInfo, _routes);
			}
		}

		private void SetEndpoints(MethodInfo methodInfo, IEnumerable<RouteAttribute> routes)
		{
			foreach (RouteAttribute _route in routes)
			{
				m_parser.Add(methodInfo.Name, _route.Path);
			}
		}

		public IStregsystemCommandResult Execute(string name, string parameters)
		{
			try
			{
				StregsystemCommand cmd = m_parser.Parse(name, parameters);
				IStregsystemCommandResult _result = cmd?.Run(this, parameters) ?? default;
				if (_result is Error _error && !string.IsNullOrWhiteSpace(_error.Message))
				{
					m_stregsystemUI.DisplayGeneralError(_error.Message);
				}
				return _result;
			}
			catch (InvalidOperationException)
			{
				m_stregsystemUI.DisplayGeneralError("Kommando kunne ikke findes. Måske har parameterne de forkerte typer");
				return new Error();
			}
			catch (Exception ex)
			{
				m_stregsystemUI.DisplayGeneralError(ex.Message);
				return new Error();
			}
		}

		[Route("/users")]
		public IStregsystemCommandResult Users(string username)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			if (_user == null)
			{
				m_stregsystemUI.DisplayUserNotFound(username);
				return new Error();
			}

			m_stregsystemUI.DisplayUserBuyInterface(_user, m_stregsystem.ActiveProducts);
			return new Ok();
		}

		[Route("/users/info")]
		public IStregsystemCommandResult UsersInfo(string username)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			if (_user == null)
			{
				m_stregsystemUI.DisplayUserNotFound(username);
				return new Error();
			}

			m_stregsystemUI.DisplayUserInformation(_user, m_stregsystem.GetTransactions(_user, 10));
			return new Ok();
		}

		[Route("/multi-buy")]
		public IStregsystemCommandResult MultiBuy(string username, int amount, int productId)
		{
			return Buy(username, productId, amount);
		}

		[Route("/buy")]
		public IStregsystemCommandResult Buy(string username, int productId)
		{
			return Buy(username, productId, 1);
		}

		private IStregsystemCommandResult Buy(string username, int productId, int amount = 1)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			if (_user == null)
			{
				m_stregsystemUI.DisplayUserNotFound(username);
				return new Error();
			}

			Product _product = m_stregsystem.GetProductById(productId);
			if (_product == null)
			{
				m_stregsystemUI.DisplayProductNotFound(productId.ToString());
				return new Error();
			}

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
			if (_user == null)
			{
				m_stregsystemUI.DisplayUserNotFound(username);
				return new Error();
			}

			try
			{
				m_stregsystem.AddCreditsToAccount(_user, amount);
				return new Ok();
			}
			catch (ArgumentException exception)
			{
				m_stregsystemUI.DisplayGeneralError(exception.Message);
				return new Error();
			}
		}

		[Route(":crediton")]
		public IStregsystemCommandResult ActivateProductCredit(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			if (_product == null)
			{
				m_stregsystemUI.DisplayProductNotFound(productId.ToString());
				return new Error();
			}

			_product.CanBeBoughtOnCredit = true;
			return new Ok();
		}

		[Route(":creditoff")]
		public IStregsystemCommandResult DeactivateProductCredit(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			if (_product == null)
			{
				m_stregsystemUI.DisplayProductNotFound(productId.ToString());
				return new Error();
			}

			_product.CanBeBoughtOnCredit = false;
			return new Ok();
		}

		[Route(":activate")]
		public IStregsystemCommandResult ActivateProduct(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			if (_product == null)
			{
				m_stregsystemUI.DisplayProductNotFound(productId.ToString());
				return new Error();
			}

			_product.Active = true;
			return new Ok();
		}

		[Route(":deactivate")]
		public IStregsystemCommandResult DeactivateProduct(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			if (_product == null)
			{
				m_stregsystemUI.DisplayProductNotFound(productId.ToString());
				return new Error();
			}

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

		private IStregsystemCommandResult StregsystemUI_CommandEntered(string name, string parameters)
		{
			return Execute(name, parameters) ?? new Error("Uhåndteret fejl...");
		}
	}
}
