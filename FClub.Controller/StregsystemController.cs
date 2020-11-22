using FClub.BLL;
using FClub.Model;
using FClub.UI;
using FClub.Controller.Command;
using FClub.Controller.Command.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace FClub.Controller
{
	public interface IStregsystemController
	{
		IStregsystemCommandResult Execute(string name, string input = "");
	}

	public class Hejsa : IStregsystemCommandResult
	{

	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public class RouteAttribute : Attribute
	{
		public string Path { get; }

		public RouteAttribute(string path)
		{
			Path = path;
		}
	}

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
			m_stregsystemUI.DisplayUserInfo(_user);
			return new Hejsa();
		}

		[Route("/buy")]
		public IStregsystemCommandResult Buy(string username, int productId, int amount = 1)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			Product _product = m_stregsystem.GetProductById(productId);
			m_stregsystem.BuyProduct(_user, _product, amount);
			return new Hejsa();
		}

		[Route("/products")]
		public IStregsystemCommandResult Products()
		{
			m_stregsystemUI.DisplayProducts(m_stregsystem.ActiveProducts);
			return new Hejsa();
		}

		[Route(":addcredits")]
		public IStregsystemCommandResult AddCreditsToUser(string username, decimal amount)
		{
			User _user = m_stregsystem.GetUserByUsername(username);
			m_stregsystem.AddCreditsToAccount(_user, amount);
			return new Hejsa();
		}

		[Route(":crediton")]
		public IStregsystemCommandResult ActivateProductCredit(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			_product.CanBeBoughtOnCredit = true;
			return new Hejsa();
		}

		[Route(":creditoff")]
		public IStregsystemCommandResult DeactivateProductCredit(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			_product.CanBeBoughtOnCredit = false;
			return new Hejsa();
		}

		[Route(":activate")]
		public IStregsystemCommandResult ActivateProduct(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			_product.Active = true;
			return new Hejsa();
		}

		[Route(":deactivate")]
		public IStregsystemCommandResult DeactivateProduct(int productId)
		{
			Product _product = m_stregsystem.GetProductById(productId);
			_product.Active = false;
			return new Hejsa();
		}

		[Route(":q")]
		[Route(":quit")]
		public IStregsystemCommandResult Stop()
		{
			m_stregsystemUI.Stop();
			return new Hejsa();
		}

		private string StregsystemUI_CommandEntered(string name, string parameters)
		{
			return Execute(name, parameters)?.ToString() ?? "Error";
		}
	}
}
