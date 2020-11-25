using System;
using System.Collections.Generic;

namespace FClub.UI.Scene.Console
{
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

		public IScene<IConsoleSceneInput> SetNavigationFor(IMenuComponent<IConsoleSceneInput> from, SceneNavigationDirection direction, IMenuComponent<IConsoleSceneInput> to)
		{
			m_sceneNavigationHandler.Addnavigation(from, direction, to);
			return this;
		}

		public void HandleInput(IConsoleSceneInput input)
		{
			input.Target = m_sceneNavigationHandler.Focused;

			switch (input.ConsoleKeyInfo.Key)
			{
				case ConsoleKey.UpArrow:    m_sceneNavigationHandler.Move(SceneNavigationDirection.Up);    break;
				case ConsoleKey.DownArrow:  m_sceneNavigationHandler.Move(SceneNavigationDirection.Down);  break;
				case ConsoleKey.RightArrow: m_sceneNavigationHandler.Move(SceneNavigationDirection.Right); break;
				case ConsoleKey.LeftArrow:  m_sceneNavigationHandler.Move(SceneNavigationDirection.Left);  break;
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
			System.Console.Clear();
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
