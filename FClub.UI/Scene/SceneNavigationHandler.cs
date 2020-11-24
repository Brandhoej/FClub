using System.Collections.Generic;

namespace FClub.UI.Scene
{
	public class SceneNavigationHandler<TSceneInput> : ISceneNavigationHandler<TSceneInput> where TSceneInput : ISceneInput
	{
		private readonly ICollection<Edge> m_edges;

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
			foreach (Edge _edge in m_edges)
			{
				if (_edge.From == Focused && _edge.Direction == direction)
				{
					SetFocus(_edge.To);
					break;
				}
			}
		}

		private IMenuComponent<TSceneInput> GetRootFocus(IMenuComponent<TSceneInput> menuComponent)
		{
			IMenuComponent<TSceneInput> _toFocus = menuComponent.GetFocusComponent();
			if (_toFocus != menuComponent)
			{
				return GetRootFocus(_toFocus);
			}
			return _toFocus;
		}

		public void SetFocus(IMenuComponent<TSceneInput> menuComponent)
		{
			IMenuComponent<TSceneInput> _toFocus = GetRootFocus(menuComponent);
			Focused?.Focus(false);
			Focused = _toFocus;
			_toFocus.Focus(true);
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
}
