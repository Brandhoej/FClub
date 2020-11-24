namespace FClub.UI.Scene
{
	public interface IMenuComponent<TInput> where TInput : ISceneInput
	{
		public int X { get; }
		public int Y { get; }

		BaseMenuComponent<TInput> GetFocusComponent();
		void Focus(bool newState);
		void AddChild(IMenuComponent<TInput> child);
		void HandleInput(TInput input);
		void Render();
		void ReRender();
	}
}
