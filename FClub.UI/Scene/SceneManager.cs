using System.Collections.Generic;

namespace FClub.UI.Scene
{
	public class SceneManager<TScene, TSceneInput> : ISceneManager<TScene> where TSceneInput : ISceneInput where TScene : IScene<TSceneInput>
	{
		private IDictionary<string, TScene> m_scenes;

		public SceneManager()
		{
			m_scenes = new Dictionary<string, TScene>();
		}

		public TScene CurrentScene { get; private set; }

		public void Add(string name, TScene scene)
		{
			m_scenes.Add(name, scene);
		}

		public void Render()
		{
			CurrentScene.Render();
		}

		public void SetScene(TScene scene)
		{
			CurrentScene = scene;
		}

		public void Swap(string name)
		{
			if (m_scenes.TryGetValue(name, out TScene _scene))
			{
				CurrentScene = _scene;
			}
		}
	}
}
