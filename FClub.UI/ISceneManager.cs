namespace FClub.UI
{
	public interface ISceneManager<TScene> : IRenderable
	{
		TScene CurrentScene { get; }

		void Add(string name, TScene scene);
		void Swap(string name);
	}
}
