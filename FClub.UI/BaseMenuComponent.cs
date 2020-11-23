using System;
using System.Collections.Generic;

namespace FClub.UI
{
	public class BaseMenuComponent<TInput> : IMenuComponent<TInput> where TInput : ISceneInput
	{
		/*private readonly ICollection<IMenuComponent> m_children;

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
		{ }*/
		public int X => throw new NotImplementedException();

		public int Y => throw new NotImplementedException();

		public void AddChild(IMenuComponent<TInput> child)
		{
			throw new NotImplementedException();
		}

		public void HandleInput(TInput input)
		{
			throw new NotImplementedException();
		}

		public void Render()
		{
			throw new NotImplementedException();
		}

		public void ReRender()
		{
			throw new NotImplementedException();
		}
	}
}
