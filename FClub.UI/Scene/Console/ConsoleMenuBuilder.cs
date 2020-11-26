using System;
using System.Collections.Generic;
using System.Linq;

namespace FClub.UI.Scene.Console
{
	public class ConsoleMenuBuilder : IConsoleMenuBuilder
	{
		private ConsoleBaseMenuComponent m_current;
		private ConsoleBaseMenuComponent m_root;
		private IScene<IConsoleSceneInput> m_scene;
		private Settings m_settings;
		private bool m_ignoreNext;

		public ConsoleMenuBuilder(IScene<IConsoleSceneInput> scene)
		{
			m_scene = scene;
			m_root = new ConsoleBaseMenuComponent();
			m_settings = new Settings();
		}

		public IConsoleMenuBuilder AddForeach<TIn>(IEnumerable<TIn> enumerable, Action<TIn> action)
		{
			foreach (TIn item in enumerable)
			{
				action(item);
			}
			return this;
		}

		public IConsoleMenuBuilder AddButton(string text, Action onClick)
		{
			AddMenuToRoot(new ConsoleButton(text, onClick));
			return this;
		}

		public IConsoleMenuBuilder AddLabel(string text = "", bool endLine = true)
		{
			AddMenuToRoot(new ConsoleLabel(text, endLine));
			return this;
		}

		public IConsoleMenuBuilder AddLineSpacer()
		{
			AddMenuToRoot(new ConsoleLabel());
			return this;
		}

		public IConsoleMenuBuilder AddMenu(ConsoleBaseMenuComponent component)
		{
			AddMenuToRoot(component);
			return this;
		}

		public IConsoleMenuBuilder AddButtonTextField(Action<string> onClick)
		{
			AddMenuToRoot(new ConsoleButtonTextField(onClick));
			return this;
		}

		public IConsoleMenuBuilder IgnoreNext()
		{
			m_ignoreNext = true;
			return this;
		}

		public IConsoleMenuBuilder AddUniNavigationTo(SceneNavigationDirection direction)
		{
			m_settings.ToDirection = direction;
			return this;
		}

		public IConsoleMenuBuilder AddBiNavigationTo(SceneNavigationDirection direction)
		{
			m_settings.ToDirection = direction;
			m_settings.FromDirection = direction.Opposite();
			return this;
		}

		public IConsoleMenuBuilder AddUniNavigationFrom(SceneNavigationDirection direction, ConsoleBaseMenuComponent source)
		{
			m_scene.SetNavigationFor(source, direction, m_current);
			return this;
		}

		public IConsoleMenuBuilder AddBiNavigationFrom(SceneNavigationDirection direction, ConsoleBaseMenuComponent source)
		{
			m_scene.SetNavigationFor(source, direction, m_current);
			m_scene.SetNavigationFor(m_current, direction.Opposite(), source);
			return this;
		}

		public IConsoleMenuBuilder AddUniNavigationTo(SceneNavigationDirection direction, ConsoleBaseMenuComponent destination)
		{
			m_scene.SetNavigationFor(m_current, direction, destination);
			return this;
		}

		public IConsoleMenuBuilder AddBiNavigationTo(SceneNavigationDirection direction, ConsoleBaseMenuComponent destination)
		{
			m_scene.SetNavigationFor(m_current, direction, destination);
			m_scene.SetNavigationFor(destination, direction.Opposite(), m_current);
			return this;
		}

		public IConsoleMenuBuilder Focus()
		{
			m_scene.SetFocus(m_current);
			return this;
		}

		public ConsoleBaseMenuComponent Build()
		{
			return m_root;
		}

		private void AddMenuToRoot(ConsoleBaseMenuComponent component)
		{
			if (!m_ignoreNext)
			{
				ApplyDirectionalSettings(m_current, component);
				m_current = component;
				int _counter = 0;
				while (_counter++ < m_settings.Amount)
				{
					m_root.AddChild(component);
				}
				m_settings = new Settings();
			}
			else
			{
				m_root.AddChild(component);
				m_ignoreNext = false;
			}
		}

		private void ApplyDirectionalSettings(ConsoleBaseMenuComponent from, ConsoleBaseMenuComponent to)
		{
			if (m_settings == null)
			{
				return;
			}

			if (from != null && to != null)
			{
				if (m_settings.HasToDirection)
				{
					m_scene.SetNavigationFor(from, m_settings.ToDirection, to);
				}

				if (m_settings.HasFromDirection)
				{
					m_scene.SetNavigationFor(to, m_settings.FromDirection, from);
				}
			}
		}

		public IConsoleMenuBuilder MultipleNext(int amount)
		{
			m_settings.Amount = amount;
			return this;
		}

		private class Settings
		{
			private SceneNavigationDirection m_fromDirection;
			private SceneNavigationDirection m_toDirection;
			private int m_amount = 1;

			public bool HasFromDirection { get; private set; }
			public SceneNavigationDirection FromDirection
			{
				get => m_fromDirection;
				set {
					HasFromDirection = true;
					m_fromDirection = value;
				}
			}

			public bool HasToDirection { get; private set; }
			public SceneNavigationDirection ToDirection
			{
				get => m_toDirection;
				set {
					HasToDirection = true;
					m_toDirection = value;
				}
			}

			public int Amount
			{
				get => m_amount;
				set => m_amount = Math.Max(value, 1);
			}
		}
	}
}
