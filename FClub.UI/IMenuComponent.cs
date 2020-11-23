namespace FClub.UI
{
	public interface IMenuComponent<TInput> where TInput : ISceneInput
	{
		public int X { get; }
		public int Y { get; }

		void AddChild(IMenuComponent<TInput> child);
		void HandleInput(TInput input);

		void Render();
		void ReRender();
	}
}
