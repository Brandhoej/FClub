using FClub.Model;
using System;
using System.Collections.Generic;
using FClub.UI.Scene;
using FClub.UI.Scene.Console;
using FClub.UI.Scene.Console.Prefabs;

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
			IConsoleScene _consoleScene = new ConsoleScene();
			ConsoleLabeledProductMenu _consoleLabeledProductMenu = new ConsoleLabeledProductMenu(products, input =>
			{
				string[] _inputSplit = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				if (_inputSplit.Length == 1)
				{
					CommandEntered("/users", input);
				}
				else
				{
					CommandEntered("/buy", input);
				}
			});
			_consoleScene.AddMenu(_consoleLabeledProductMenu);

			m_sceneManager.SetScene(_consoleScene);
			m_sceneManager.Render();
			_consoleScene.SetFocus(_consoleLabeledProductMenu);
		}

		public void DisplayUserBuyInterface(User user, IEnumerable<Product> products)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleInteractableProductMenu consoleInteractableProductMenu = new ConsoleInteractableProductMenu(_scene, products, 
				product => CommandEntered("/buy", $"{user.Username} {product.Id}"), 
				() => CommandEntered("/products", string.Empty),
				() => CommandEntered("/users/info", user.Username));
			_scene.AddMenu(consoleInteractableProductMenu);
			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(consoleInteractableProductMenu);
		}

		public void DisplayUserInformation(User user, IEnumerable<Transaction> transactions)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/users", user.Username));
			_scene.AddMenu(new ConsoleUserInformationMenu(user, transactions));
			_scene.AddMenu(new ConsoleLabel());

			_scene.AddMenu(_backButton);
			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		public void DisplayUserBuysProduct(BuyTransaction transaction)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/users", transaction.User.Username));
			_scene.AddMenu(new ConsoleLabel(transaction));
			_scene.AddMenu(_backButton);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/users", transaction.User.Username));
			_scene.AddMenu(new ConsoleLabel(transaction));
			_scene.AddMenu(new ConsoleLabel(count));
			_scene.AddMenu(_backButton);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		public void DisplayUserNotFound(string username)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/products", string.Empty));
			_scene.AddMenu(new ConsoleLabel($"User not found: {username}"));
			_scene.AddMenu(_backButton);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		public void DisplayProductNotFound(string product)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/products", string.Empty));
			_scene.AddMenu(new ConsoleLabel($"Product not found: {product}"));
			_scene.AddMenu(_backButton);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		public void DisplayInsufficientCash(User user, Product product)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/users", user.Username));
			_scene.AddMenu(new ConsoleLabel($"User: {user.Username} has insufficient cash for {product.Name}"));
			_scene.AddMenu(_backButton);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		public void DisplayGeneralError(string errorString)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/products", string.Empty));
			_scene.AddMenu(new ConsoleLabel($"Error: {errorString}"));
			_scene.AddMenu(_backButton);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		public void DisplayTooManyArgumentsError(string command)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/products", string.Empty));
			_scene.AddMenu(new ConsoleLabel($"Command has too many arguments: '{command}'"));
			_scene.AddMenu(_backButton);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		public void DisplayAdminCommandNotFoundMessage(string adminCommand)
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/products", string.Empty));
			_scene.AddMenu(new ConsoleLabel($"Admin command not found: {adminCommand}"));
			_scene.AddMenu(_backButton);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_backButton);
		}

		private void DisplayAdmin()
		{
			ConsoleScene _scene = new ConsoleScene();
			ConsoleButton _backButton = new ConsoleButton("Back", () => CommandEntered("/products", string.Empty));
			ConsoleButtonTextField _consoleButtonTextField = new ConsoleButtonTextField(input =>
			{
				string[] _split = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				CommandEntered($":{_split[0]}", string.Join(' ', _split[1..^0]));
			});
			_scene.AddMenu(new ConsoleLabel("Admin UI"));
			_scene.AddMenu(new ConsoleLabel(":", false));
			_scene.AddMenu(_consoleButtonTextField);
			_scene.AddMenu(new ConsoleLabel());
			_scene.AddMenu(_backButton);

			_scene.SetNavigationFor(_consoleButtonTextField, ISceneNavigationDirection.Down, _backButton);
			_scene.SetNavigationFor(_backButton, ISceneNavigationDirection.Up, _consoleButtonTextField);

			m_sceneManager.SetScene(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_consoleButtonTextField);
		}

		public void Stop()
		{
			Running = false;
		}
	}
}
