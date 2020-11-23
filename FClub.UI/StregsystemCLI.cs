using FClub.Model;
using FClub.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FClub.UI
{
	public class StregsystemCLI : IStregsystemUI
	{
		public event stregsystemEvent CommandEntered;
		private readonly ISceneManager<ConsoleScene> m_sceneManager;

		public StregsystemCLI()
		{
			m_sceneManager = new SceneManager<ConsoleScene, IConsoleSceneInput>();
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
				ConsoleKeyInfo _keyInfo = Console.ReadKey(true);

				if (_keyInfo.KeyChar == ':')
				{
					DisplayAdmin();
				}

				IConsoleSceneInput _event = new ConsoleSceneInput();
				m_sceneManager.CurrentScene.HandleInput(_event);
			}
		}

		public void DisplayProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public void DisplayProducts(IEnumerable<Product> products)
		{
			Console.WriteLine("Test");
		}

		public void DisplayUserBuyInterface(User user, IEnumerable<Product> products)
		{
			throw new NotImplementedException();
		}

		public void DisplayUserInformation(User user, IEnumerable<Transaction> transactions)
		{
			throw new NotImplementedException();
		}

		public void DisplayUserBuysProduct(BuyTransaction transaction)
		{
			throw new NotImplementedException();
		}

		public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
		{
			throw new NotImplementedException();
		}

		public void DisplayUserNotFound(string username)
		{
			throw new NotImplementedException();
		}

		public void DisplayProductNotFound(string product)
		{
			throw new NotImplementedException();
		}

		public void DisplayInsufficientCash(User user, Product product)
		{
			throw new NotImplementedException();
		}

		public void DisplayGeneralError(string errorString)
		{
			throw new NotImplementedException();
		}

		public void DisplayTooManyArgumentsError(string command)
		{
			throw new NotImplementedException();
		}

		public void DisplayAdminCommandNotFoundMessage(string adminCommand)
		{
			throw new NotImplementedException();
		}

		private void DisplayAdmin()
		{

		}

		/*
		private void DisplayAdmin()
		{
			Label _adminInfo = new Label("You are now accessing admin controls");
			InlineLabel _inlineLabel = new InlineLabel(":");
			ExecutableTextbox _textbox = new ExecutableTextbox(command => CommandEntered?.Invoke(':' + command.Split(' ')[0], string.Join(' ', command.Split(' ')[1..^0])));
			Spacer spacer = new Spacer();
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty));
			IScene _scene = new ConsoleScene(_adminInfo, _inlineLabel, _textbox, spacer, _button);
			_textbox.Down = _button;
			_button.Up = _textbox;
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_textbox);
		}

		public void DisplayProducts(IEnumerable<Product> products)
		{
			IMenuComponent _productMenu = new ProductMenu(products);
			IMenuComponent _quickbuy = new InlineLabel("Quickbuy> ");
			IMenuComponent _textbox = new ExecutableTextbox(command =>
			{
				if (command.Split(' ').Length == 1)
				{
					CommandEntered?.Invoke("/users", command);
				}
				else
				{
					CommandEntered?.Invoke("/buy", command);
				}
			})
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_productMenu, _quickbuy, _textbox);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayUserBuysProduct(BuyTransaction transaction)
		{
			Label _label = new Label($"'{transaction.User.Username}' has bought '{transaction.Product.Name}' at a price of '{transaction.Product.Price}'");
			Button _button1 = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			Spacer spacer = new Spacer();
			Button _button2 = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty));
			_button1.Down = _button2;
			_button2.Up = _button1;
			IScene _scene = new ConsoleScene(_label, _button1, spacer, _button2);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
		{
			Label _label = new Label($"'{transaction.User.Username}' has bought '{transaction.Product.Name}' at a price of '{transaction.Product.Price} {count}' time(s)");
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_label, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayUserBuyInterface(User user, IEnumerable<Product> products)
		{
			Label _labelUser = new Label($"User: '{user}'");
			Label _labelbalance = new Label($"Balance: '{user.Balance}'");
			ButtonProductMenu _buttonProductMenu = new ButtonProductMenu(products, product =>
			{
				CommandEntered?.Invoke("/buy", $"{user.Username} {product.Id}");
				CommandEntered?.Invoke("/users", $"{user.Username}");
			});
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty));
			Spacer _spacer = new Spacer();
			Button _userInfo = new Button("User info", () => CommandEntered?.Invoke("/users/info", user.Username));
			IScene _scene = new ConsoleScene(_labelUser, _labelbalance, _buttonProductMenu, _userInfo, _spacer, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_buttonProductMenu.buttons.First());

			Button _lastButton = _buttonProductMenu.buttons.Last();
			_lastButton.Down = _userInfo;
			_userInfo.Up = _lastButton;
			_userInfo.Down = _button;
			_button.Up = _userInfo;
		}

		public void DisplayUserInformation(User user, IEnumerable<Transaction> transactions)
		{
			Label _labelUser = new Label($"User: '{user}'");
			Label _labelbalance = new Label($"Balance: '{user.Balance}'");
			BaseMenuComponent _transactionsContainter = new BaseMenuComponent();
			foreach (Transaction transaction in transactions)
			{
				_transactionsContainter.AddChild(new Label(transaction.GetType().Name));
			}
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/users", $"{user.Username}"));
			IScene _scene = new ConsoleScene(_labelUser, _labelbalance, _transactionsContainter, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
			_scene.SetFocus(_button);
		}

		public void DisplayInsufficientCash(User user, Product product)
		{
			Label _label = new Label($"'{user}' has insufficient funds for '{product.Name}' price: '{product.Price}'");
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_label, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayProductNotFound(string product)
		{
			Label _label = new Label($"Product '{product}' not found");
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_label, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayTooManyArgumentsError(string command)
		{
			Label _label = new Label($"Too many arguments in '{command}'");
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_label, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayAdminCommandNotFoundMessage(string adminCommand)
		{
			Label _label = new Label($"Command not found '{adminCommand}'");
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_label, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayGeneralError(string errorString)
		{
			Label _label = new Label($"Error: '{errorString}'");
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_label, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayUserNotFound(string username)
		{
			Label _label = new Label($"User '{username}' not found");
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_label, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}

		public void DisplayProduct(Product product)
		{
			Label _label = new Label($"Product: '{product.Name}', Active: '{product.Active}', Price: '{product.Price}' CanBeBoughtOnCredit: '{product.CanBeBoughtOnCredit}'");
			Button _button = new Button("Back", () => CommandEntered?.Invoke("/products", string.Empty))
			{
				Focus = true
			};
			IScene _scene = new ConsoleScene(_label, _button);
			m_sceneManager.Swap(_scene);
			m_sceneManager.Render();
		}
		*/

		public void Stop()
		{
			Running = false;
		}
	}
}
