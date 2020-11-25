using FClub.Model;
using System;
using System.Collections.Generic;
using FClub.UI.Scene;
using FClub.UI.Scene.Console;
using FClub.UI.Scene.Console.Prefabs;
using System.Linq;

namespace FClub.UI
{
	public class StregsystemCLI : IStregsystemUI
	{
		public event stregsystemEvent CommandEntered;
		private readonly ISceneManager<IConsoleScene> m_sceneManager;

		public StregsystemCLI()
		{
			m_sceneManager = new SceneManager<IConsoleScene, IConsoleSceneInput>();
		}

		public bool Running { get; private set; }

		
		public void Start()
		{
			Run();
		}

		private void Run()
		{
			Running = true;
			while (Running)
			{
				ConsoleKeyInfo _keyInfo = System.Console.ReadKey(true);

				if (_keyInfo.KeyChar == ':')
				{
					DisplayAdmin();
				}

				IConsoleSceneInput _event = new ConsoleSceneInput()
				{
					ConsoleKeyInfo = _keyInfo
				};
				m_sceneManager.CurrentScene.HandleInput(_event);
			}
		}

		public void DisplayProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public void DisplayProducts(IEnumerable<Product> products)
		{
			void Quickbuy(string input)
			{
				string[] _split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				if (_split.Length == 1)
				{
					CommandEntered("/users", input);
				}
				else
				{
					CommandEntered("/buy", input);
				}
			}

			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);
			_consoleMenuBuilder
				.AddLabel("Du kan \"sætte streger\" på to forskellige måder:")
				.AddLabel("1. Indtast dit brugernavn nedenfor. Du vil så blive præsenteret for en interaktiv menu.")
				.AddLabel("2. Indtast dit brugernavn og et eller flere produkt ID (adskilt med \"space\"). Købet vil blive direkte registreret uden yderligere input. Under feltet vil der vises en bekræftelse af købet.")
				.MultipleNext(1).AddLineSpacer()
				.AddForeach(products, _product => _consoleMenuBuilder.AddLabel(_product.Name))
				.MultipleNext(1).AddLineSpacer()
				.AddLabel("Qucibuy>", false)
				.AddButtonTextField(Quickbuy).Focus();
			
			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayUserBuyInterface(User user, IEnumerable<Product> products)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);
			_consoleMenuBuilder
				.AddLabel(user.ToString())
				.AddLabel($"Du har {user.Balance} kroner til gode!")
				.MultipleNext(1).AddLineSpacer()
				.AddForeach(products, product =>
				{
					_consoleMenuBuilder
						.AddButton(product.Name, () => CommandEntered("/buy", $"{user.Username} {product.Id}"))
						.IgnoreNext().AddLineSpacer()
						.AddBiNavigationTo(SceneNavigationDirection.Down);
				})
				.IgnoreNext().AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/products", string.Empty))
				.IgnoreNext().AddLabel("     ", false)
				.AddBiNavigationTo(SceneNavigationDirection.Right)
				.AddButton("User info", () => CommandEntered("/users/info", user.Username)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayUserInformation(User user, IEnumerable<Transaction> transactions)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel(user.ToString())
				.AddLineSpacer()
				.AddLabel(transactions.Count() == 0 ? "Du har ingen transaktioner" : "Transaktioner:")
				.AddForeach(transactions, transaction => _consoleMenuBuilder.AddLabel(transaction.ToString()))
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/users", user.Username)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayUserBuysProduct(BuyTransaction transaction)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel($"Du har lige købt {transaction.Product.Name} til {transaction.Product.Price} kroner")
				.AddLineSpacer()
				.AddLabel($"Info: {transaction}")
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/users", transaction.User.Username)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel($"Du har lige købt {count} af {transaction.Product.Name} til {transaction.Product.Price} kroner")
				.AddLineSpacer()
				.AddLabel($"Info: {transaction}")
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/users", transaction.User.Username)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayUserNotFound(string username)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel($"Bruger \"{username}\" blev ikke fundet")
				.AddLabel($"Det var sært, {username}.")
				.AddLabel($"Det lader ikke til, at du er registreret som aktivt medlem af F-klubben i TREOENs database.")
				.AddLabel($"Måske tastede du forkert?")
				.AddLabel($"Hvis du ikke er medlem, kan du blive det ved at følge guiden på http://fklub.dk.")
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/products", string.Empty)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayProductNotFound(string product)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel($"Produkt \"{product}\" blev ikke fundet")
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/products", string.Empty)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayInsufficientCash(User user, Product product)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel($"Bruger: '{user.Username}' har ikke nok penge til '{product.Name}' ({product.Price} kroner)")
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/users", user.Username)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayGeneralError(string errorString)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel($"Fejl: '{errorString}'")
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/products", string.Empty)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayTooManyArgumentsError(string command)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel($"For mange argumenter: '{command}'")
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/products", string.Empty)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void DisplayAdminCommandNotFoundMessage(string adminCommand)
		{
			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel($"Admi kommanda: '{adminCommand}'")
				.AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/products", string.Empty)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		private void DisplayAdmin()
		{
			void ExecuteAdminCommand(string input)
			{
				string[] _split = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				CommandEntered($":{_split[0]}", string.Join(' ', _split[1..^0]));
			}

			IConsoleScene _consoleScene = new ConsoleScene();
			IConsoleMenuBuilder _consoleMenuBuilder = new ConsoleMenuBuilder(_consoleScene);

			_consoleMenuBuilder
				.AddLabel("Indsæt admin kommando")
				.AddLabel(":", false)
				.AddButtonTextField(ExecuteAdminCommand)
				.AddBiNavigationTo(SceneNavigationDirection.Down)
				.IgnoreNext().AddLineSpacer()
				.AddButton("Tilbage", () => CommandEntered("/products", string.Empty)).Focus();

			_consoleScene.AddMenu(_consoleMenuBuilder.Build());
			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
		}

		public void Stop()
		{
			Running = false;
		}
	}
}
