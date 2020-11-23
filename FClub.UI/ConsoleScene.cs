using System;
using System.Collections.Generic;
using System.Linq;

namespace FClub.UI
{
	public interface ISceneInput
	{

	}

	public interface IConsoleSceneInput : ISceneInput
	{
		ConsoleKeyInfo ConsoleKeyInfo { get; set; }
		IMenuComponent<IConsoleSceneInput> Target { get; set; }
	}

	public class ConsoleSceneInput : IConsoleSceneInput
	{
		public ConsoleKeyInfo ConsoleKeyInfo { get; set; }
		public IMenuComponent<IConsoleSceneInput> Target { get; set; }
	}

	public enum ISceneNavigationDirection
	{
		Up, Down, Left, Right
	}

	public interface ISceneNavigationHandler<TSceneInput> where TSceneInput : ISceneInput
	{
		IMenuComponent<TSceneInput> Focused { get; }

		void Addnavigation(IMenuComponent<TSceneInput> from, ISceneNavigationDirection direction, IMenuComponent<TSceneInput> to);
		void SetFocus(IMenuComponent<TSceneInput> menuComponent);
		void Move(ISceneNavigationDirection direction);
	}

	public class SceneNavigationHandler<TSceneInput> : ISceneNavigationHandler<TSceneInput> where TSceneInput : ISceneInput
	{
		private ICollection<Edge> m_edges;

		public SceneNavigationHandler()
		{
			m_edges = new List<Edge>();
		}

		public IMenuComponent<TSceneInput> Focused { get; private set; }

		public void Addnavigation(IMenuComponent<TSceneInput> from, ISceneNavigationDirection direction, IMenuComponent<TSceneInput> to)
		{
			m_edges.Add(new Edge(from, direction, to));
		}

		public void Move(ISceneNavigationDirection direction)
		{
			IMenuComponent<TSceneInput> _newTarget = m_edges.Where(edge => edge.From == Focused && edge.Direction == direction).First().To;
			SetFocus(_newTarget);
		}

		public void SetFocus(IMenuComponent<TSceneInput> menuComponent)
		{
			Focused = menuComponent;
		}

		private class Edge
		{
			public Edge(IMenuComponent<TSceneInput> from, ISceneNavigationDirection direction, IMenuComponent<TSceneInput> to)
			{
				From = from;
				Direction = direction;
				To = to;
			}

			public IMenuComponent<TSceneInput> From { get; }
			public ISceneNavigationDirection Direction { get; }
			public IMenuComponent<TSceneInput> To { get; }
		}
	}

	public class ConsoleScene : IConsoleScene
	{
		private ICollection<IMenuComponent<IConsoleSceneInput>> m_menuComponents;
		private ISceneNavigationHandler<IConsoleSceneInput> m_sceneNavigationHandler;

		public ConsoleScene()
		{
			m_menuComponents = new List<IMenuComponent<IConsoleSceneInput>>();
			m_sceneNavigationHandler = new SceneNavigationHandler<IConsoleSceneInput>();
		}

		public IScene<IConsoleSceneInput> AddMenus(params IMenuComponent<IConsoleSceneInput>[] menuComponents)
		{
			foreach (IMenuComponent<IConsoleSceneInput> _menuComponent in menuComponents)
			{
				m_menuComponents.Add(_menuComponent);
			}
			return this;
		}

		public IScene<IConsoleSceneInput> AddMenu(IMenuComponent<IConsoleSceneInput> menuComponent)
		{
			m_menuComponents.Add(menuComponent);
			return this;
		}

		public IScene<IConsoleSceneInput> SetNavigationFor(IMenuComponent<IConsoleSceneInput> from, ISceneNavigationDirection direction, IMenuComponent<IConsoleSceneInput> to)
		{
			m_sceneNavigationHandler.Addnavigation(from, direction, to);
			return this;
		}

		public void HandleInput(IConsoleSceneInput input)
		{
			input.Target = m_sceneNavigationHandler.Focused;

			switch (input.ConsoleKeyInfo.Key)
			{
				case ConsoleKey.UpArrow: m_sceneNavigationHandler.Move(ISceneNavigationDirection.Up); break;
				case ConsoleKey.DownArrow: m_sceneNavigationHandler.Move(ISceneNavigationDirection.Down); break;
				case ConsoleKey.RightArrow: m_sceneNavigationHandler.Move(ISceneNavigationDirection.Right); break;
				case ConsoleKey.LeftArrow: m_sceneNavigationHandler.Move(ISceneNavigationDirection.Left); break;
				default: HandleDefaultInput(input); break;
			}
		}

		private void HandleDefaultInput(IConsoleSceneInput input)
		{
			foreach (IMenuComponent<IConsoleSceneInput> _menuComponent in m_menuComponents)
			{
				_menuComponent.HandleInput(input);
			}
		}

		public void Render()
		{
			foreach (IMenuComponent<IConsoleSceneInput> _menuComponent in m_menuComponents)
			{
				_menuComponent.Render();
			}
		}

		public void SetFocus(IMenuComponent<IConsoleSceneInput> menuComponent)
		{
			m_sceneNavigationHandler.SetFocus(menuComponent);
		}
	}
}
