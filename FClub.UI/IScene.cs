using FClub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace FClub.UI
{
	public interface IRenderable
	{
		void Render();
	}

	public interface IMenuComponentEvent
	{
		ConsoleKeyInfo KeyInfo { get; }
		bool Changed { get; set; }
	}

	public class MenuComponentEvent : IMenuComponentEvent
	{
		public MenuComponentEvent(ConsoleKeyInfo key)
		{
			KeyInfo = key;
		}

		public ConsoleKeyInfo KeyInfo { get; }
		public bool Changed { get; set; }
	}

	public interface IMenuComponent
	{
		public int X { get; }
		public int Y { get; }
		bool Focus { get; set; }

		void AddChild(IMenuComponent child);
		void Event(IMenuComponentEvent menuComponentEvent);
		void Render();
		void ReRender();
	}

	public interface IScene : IMenuComponent
	{
		void SetFocus(IMenuComponent menuComponent);
	}

	public interface ISceneManager : IRenderable
	{
		IScene CurrentScene { get; }
		void Add(string name, IScene scene);
		void Swap(IScene scene);
		void Swap(string name);
	}

	public class SceneManager<TScene> : ISceneManager where TScene : IScene
	{
		private IScene m_currentScene;
		private IDictionary<string, IScene> m_scenes;

		public SceneManager()
		{
			m_scenes = new Dictionary<string, IScene>();
		}

		public IScene CurrentScene => m_currentScene;

		public void Add(string name, IScene scene)
		{
			m_scenes.Add(name, scene);
		}

		public void Render()
		{
			m_currentScene?.Render();
		}

		public void Swap(IScene scene)
		{
			m_currentScene = scene;
		}

		public void Swap(string name)
		{
			Swap(m_scenes[name]);
		}
	}

	public interface IConsoleScene : IScene
	{

	}

	public class ConsoleScene : IConsoleScene
	{
		private ICollection<IMenuComponent> m_rootMenuComponents;

		public ConsoleScene(params IMenuComponent[] rootMenuComponent)
		{
			m_rootMenuComponents = new List<IMenuComponent>(rootMenuComponent) ?? throw new ArgumentNullException("Root cannot be null", nameof(rootMenuComponent));
		}

		public bool Focus { get; set; }

		public int X => throw new NotImplementedException();

		public int Y => throw new NotImplementedException();

		public void AddChild(IMenuComponent child)
		{
			m_rootMenuComponents.Add(child);
		}

		public void Event(IMenuComponentEvent menuComponentEvent)
		{
			foreach (IMenuComponent _menuComponent in m_rootMenuComponents)
			{
				_menuComponent.Event(menuComponentEvent);
			}
		}

		public void Render()
		{
			Console.Clear();
			foreach (IMenuComponent _menuComponent in m_rootMenuComponents)
			{
				_menuComponent.Render();
			}
		}

		public void ReRender()
		{
			throw new NotImplementedException();
		}

		public void SetFocus(IMenuComponent menuComponent)
		{
			menuComponent.Focus = true;
			menuComponent.ReRender();
		}
	}

	public class BaseMenuComponent : IMenuComponent
	{
		private readonly ICollection<IMenuComponent> m_children;

		public IMenuComponent Up, Down, Left, Right;

		public BaseMenuComponent(IEnumerable<IMenuComponent> children = null)
		{
			m_children = children == null ? new List<IMenuComponent>() : new List<IMenuComponent>(children);
		}

		public bool Focus { get; set; }

		public int X { get; private set; } = -1;
		public int Y { get; private set; } = -1;

		public void AddChild(IMenuComponent child)
		{
			m_children.Add(child);
		}

		public void Event(IMenuComponentEvent menuComponentEvent)
		{
			if (!menuComponentEvent.Changed)
			{
				switch (menuComponentEvent.KeyInfo.Key)
				{
					case ConsoleKey.UpArrow:	menuComponentEvent.Changed = ChangeFocus(Up);    break;
					case ConsoleKey.DownArrow:	menuComponentEvent.Changed = ChangeFocus(Down);  break;
					case ConsoleKey.LeftArrow:	menuComponentEvent.Changed = ChangeFocus(Left);  break;
					case ConsoleKey.RightArrow: menuComponentEvent.Changed = ChangeFocus(Right); break;
				}
			}
			OnEvent(menuComponentEvent);
			foreach (IMenuComponent _menuComponent in m_children)
			{
				_menuComponent.Event(menuComponentEvent);
			}
		}

		private bool ChangeFocus(IMenuComponent menuComponent)
		{
			if (menuComponent == null ||
				!Focus)
			{
				return false;
			}

			Focus = false;
			ReRender();
			menuComponent.Focus = true;
			menuComponent.ReRender();
			return true;
		}

		public void Render()
		{
			X = Console.CursorLeft;
			Y = Console.CursorTop;

			if (Focus)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}

			OnRender();

			foreach (IMenuComponent _menuComponent in m_children)
			{
				_menuComponent.Render();
			}

			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Black;

		}

		public void ReRender()
		{
			Console.CursorLeft = X;
			Console.CursorTop = Y;
			Render();
		}

		protected virtual void OnEvent(IMenuComponentEvent menuComponentEvent)
		{ }

		protected virtual void OnRender()
		{ }
	}

	public class ProductMenu : BaseMenuComponent
	{
		public ProductMenu(IEnumerable<Product> products)
		{
			AddProductLables(products);
		}

		private void AddProductLables(IEnumerable<Product> products)
		{
			int[] columnWidths = new int[3]
			{
				2, 4, 5
			};

			foreach (Product product in products)
			{
				columnWidths[0] = Math.Max(product.Id.ToString().Length, columnWidths[0]);
				columnWidths[1] = Math.Max(product.Name.ToString().Length, columnWidths[1]);
				columnWidths[2] = Math.Max(product.Price.ToString().Length, columnWidths[2]);
			}

			string format = "{0,-" + columnWidths[0] + "}   {1,-" + columnWidths[1] + "}   {2,-" + columnWidths[2] + "}";
			AddChild(new Label(string.Format(format, "Id", "Name", "Price")));
			foreach (Product product in products)
			{
				AddChild(new Label(string.Format(format, product.Id, product.Name, product.Price)));
			}
		}
	}

	public class ButtonProductMenu : BaseMenuComponent
	{
		public IList<Button> buttons;
		public event Action<Product> OnProductClicked;

		public ButtonProductMenu(IEnumerable<Product> products, Action<Product> onProductClicked)
		{
			AddProductLables(products);
			OnProductClicked = onProductClicked;
		}

		private void AddProductLables(IEnumerable<Product> products)
		{
			int[] columnWidths = new int[3]
			{
				2, 4, 5
			};

			foreach (Product product in products)
			{
				columnWidths[0] = Math.Max(product.Id.ToString().Length, columnWidths[0]);
				columnWidths[1] = Math.Max(product.Name.ToString().Length, columnWidths[1]);
				columnWidths[2] = Math.Max(product.Price.ToString().Length, columnWidths[2]);
			}

			string format = "{0,-" + columnWidths[0] + "}   {1,-" + columnWidths[1] + "}   {2,-" + columnWidths[2] + "}";
			AddChild(new Label(string.Format(format, "Id", "Name", "Price")));
			buttons = new List<Button>();
			foreach (Product product in products)
			{
				Button _button = new Button(string.Format(format, product.Id, product.Name, product.Price), () => OnProductClicked?.Invoke(product));
				buttons.Add(_button);
				AddChild(_button);
				AddChild(new Spacer());
			}

			for (int i = 1; i < buttons.Count - 1; i++)
			{
				buttons[i].Up = buttons[i - 1];
				buttons[i].Down = buttons[i + 1];

				buttons[i - 1].Down = buttons[i];
				buttons[i + 1].Up = buttons[i];
			}
		}
	}

	public class Label : BaseMenuComponent
	{
		private string m_text;

		public Label(string text)
		{
			m_text = text;
		}

		protected override void OnRender()
		{
			Console.WriteLine(m_text);
		}
	}

	public class Spacer : Label
	{
		public Spacer()
			: base(string.Empty)
		{ }
	}

	public class InlineLabel : BaseMenuComponent
	{
		private string m_text;

		public InlineLabel(string text)
		{
			m_text = text;
		}

		protected override void OnRender()
		{
			Console.Write(m_text);
		}
	}

	public class Button : BaseMenuComponent
	{
		public Button(string text, Action onClicked)
		{
			OnClicked = onClicked;
			AddChild(new InlineLabel(text));
		}

		public Action OnClicked { get; }

		protected override void OnEvent(IMenuComponentEvent menuComponentEvent)
		{
			if (Focus && menuComponentEvent.KeyInfo.Key == ConsoleKey.Enter)
			{
				OnClicked?.Invoke();
			}
			base.OnEvent(menuComponentEvent);
		}
	}

	public class Textbox : BaseMenuComponent
	{
		private int m_x = -1, m_y = -1;
		private string m_text = string.Empty;

		protected override void OnRender()
		{
			if (m_x == -1)
			{
				m_x = Console.CursorLeft;
				m_y = Console.CursorTop;
			}

			base.OnRender();
		}

		public string Text => m_text;

		protected override void OnEvent(IMenuComponentEvent menuComponentEvent)
		{
			if (!Focus)
			{
				return;
			}

			if (Regex.IsMatch(menuComponentEvent.KeyInfo.KeyChar.ToString(), "[a-zA-Z0-9 ]"))
			{
				WriteChar(menuComponentEvent.KeyInfo.KeyChar);
			}
			else if (m_text.Length > 0 && menuComponentEvent.KeyInfo.Key == ConsoleKey.Backspace)
			{
				RemoveChar();
			}
		}

		private void WriteChar(char c)
		{
			Console.CursorTop = m_y;
			Console.CursorLeft = m_x + m_text.Length;

			m_text += c;
			Console.Write(c);
		}

		private void RemoveChar()
		{
			Console.CursorTop = m_y;
			Console.CursorLeft = m_x + m_text.Length - 1;
			Console.Write(' ');
			Console.CursorLeft = m_x + m_text.Length - 1;
			m_text = m_text[0..^1];

		}
	}

	public class ExecutableTextbox : Textbox
	{
		public event Action<string> OnEnter;

		public ExecutableTextbox(Action<string> onEnter)
		{
			OnEnter += onEnter;
		}

		protected override void OnEvent(IMenuComponentEvent menuComponentEvent)
		{
			if (Focus && menuComponentEvent.KeyInfo.Key == ConsoleKey.Enter)
			{
				OnEnter?.Invoke(Text);
			}
			else
			{
				base.OnEvent(menuComponentEvent);
			}
		}
	}
}
