using System;
using System.Collections.Generic;

namespace FClub.UI.Scene.Console
{
	public class ConsoleBaseMenuComponent : BaseMenuComponent<IConsoleSceneInput>
	{
		private int m_x = -1;
		private int m_y = -1;

		private ICollection<IMenuComponent<IConsoleSceneInput>> m_menuComponents;

		public ConsoleBaseMenuComponent()
		{
			m_menuComponents = new List<IMenuComponent<IConsoleSceneInput>>();
		}

		public bool IsFocused { get; private set; }

		public override int X => m_x;

		public override int Y => m_y;

		public override void AddChild(IMenuComponent<IConsoleSceneInput> child)
		{
			m_menuComponents.Add(child);
		}

		public override void Focus(bool newState)
		{
			ChangeFocus(newState);
		}

		private void ChangeFocus(bool value)
		{
			bool _oldFocus = IsFocused;
			IsFocused = value;
			if (IsFocused != _oldFocus)
			{
				if (IsFocused)
				{
					OnFocused();
				}
				ReRender();
			}
		}

		public override void HandleInput(IConsoleSceneInput input)
		{
			switch (input.ConsoleKeyInfo.Key)
			{
				case ConsoleKey.Enter: OnEnterClicked(); break;
				default:
				{
					OnCharEntered(input.ConsoleKeyInfo.KeyChar);
					break;
				}
			}

			OnHandleInput(input);

			foreach (IMenuComponent<IConsoleSceneInput> _menuComponent in m_menuComponents)
			{
				_menuComponent.HandleInput(input);
			}
		}

		public override void Render()
		{
			if (X == -1 && Y == -1)
			{
				m_x = System.Console.CursorLeft;
				m_y = System.Console.CursorTop;
			}

			if (IsFocused)
			{
				HighlightColors();
			}

			OnRender(); 


			foreach (IMenuComponent<IConsoleSceneInput> _menuComponent in m_menuComponents)
			{
				_menuComponent.Render();
			}

			StdColors();
		}

		public override BaseMenuComponent<IConsoleSceneInput> GetFocusComponent()
		{
			return this;
		}

		protected void HighlightColors()
		{
			System.Console.ForegroundColor = ConsoleColor.Black;
			System.Console.BackgroundColor = ConsoleColor.White;
		}

		protected void StdColors()
		{
			System.Console.ForegroundColor = ConsoleColor.White;
			System.Console.BackgroundColor = ConsoleColor.Black;
		}

		public override void ReRender()
		{
			if (X >= 0 && Y >= 0)
			{ 
				System.Console.CursorLeft = X;
				System.Console.CursorTop = Y;
				Render();
			}
		}

		protected virtual void OnFocused() { }
		protected virtual void OnHandleInput(IConsoleSceneInput input) { }
		protected virtual void OnRender() { }
		protected virtual void OnEnterClicked() { }
		protected virtual void OnCharEntered(char character) { }
	}
}
