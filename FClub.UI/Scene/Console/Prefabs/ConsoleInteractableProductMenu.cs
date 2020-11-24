using FClub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FClub.UI.Scene.Console.Prefabs
{
	public class ConsoleInteractableProductMenu : ConsoleBaseMenuComponent
	{
		private readonly IScene<IConsoleSceneInput> m_scene;
		private readonly IEnumerable<Product> m_products;
		private event Action<Product> m_onProductClicked;
		private event Action m_onExit;
		private event Action m_onUserInfoClicked;
		private BaseMenuComponent<IConsoleSceneInput> m_toFocus;

		public ConsoleInteractableProductMenu(IScene<IConsoleSceneInput> scene, IEnumerable<Product> products, Action<Product> onProductClicked, Action onExit, Action onUserInfoClicked)
		{
			m_scene = scene;
			m_products = products;
			m_onProductClicked += onProductClicked;
			m_onExit += onExit;
			m_onUserInfoClicked += onUserInfoClicked;
			Build();
		}

		public override BaseMenuComponent<IConsoleSceneInput> GetFocusComponent()
		{
			return m_toFocus;
		}

		private void Build()
		{
			IList<ConsoleButton> _buttons = new List<ConsoleButton>();
			foreach (Product _product in m_products)
			{
				ConsoleButton consoleButton = new ConsoleButton($"{_product.Id}   {_product.Name}", () => m_onProductClicked?.Invoke(_product));
				_buttons.Add(consoleButton);
				AddChild(consoleButton);
				AddChild(new ConsoleLabel());
			}

			for (int i = 1; i < _buttons.Count - 1; i++)
			{
				m_scene.SetNavigationFor(_buttons[i], ISceneNavigationDirection.Up, _buttons[i - 1]);
				m_scene.SetNavigationFor(_buttons[i], ISceneNavigationDirection.Down, _buttons[i + 1]);

				m_scene.SetNavigationFor(_buttons[i - 1], ISceneNavigationDirection.Down, _buttons[i]);
				m_scene.SetNavigationFor(_buttons[i + 1], ISceneNavigationDirection.Up, _buttons[i]);
			}

			m_toFocus = _buttons.First();

			ConsoleButton _backButton = new ConsoleButton("Back", () => m_onExit?.Invoke());
			m_scene.SetNavigationFor(_buttons.Last(), ISceneNavigationDirection.Down, _backButton);
			m_scene.SetNavigationFor(_backButton, ISceneNavigationDirection.Up, _buttons.Last());
			AddChild(_backButton);

			AddChild(new ConsoleLabel("      ", false));

			ConsoleButton _userInfoButton = new ConsoleButton("User info", () => m_onUserInfoClicked?.Invoke());
			m_scene.SetNavigationFor(_backButton, ISceneNavigationDirection.Right, _userInfoButton);
			m_scene.SetNavigationFor(_userInfoButton, ISceneNavigationDirection.Left, _backButton);
			AddChild(_userInfoButton);
		}
	}
}
