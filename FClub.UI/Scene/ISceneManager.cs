namespace FClub.UI.Scene
{
	public interface ISceneManager<TScene> : IRenderable
	{
		TScene CurrentScene { get; }

		void SetScene(TScene scene);
		void Add(string name, TScene scene);
		void Swap(string name);
	}
}
