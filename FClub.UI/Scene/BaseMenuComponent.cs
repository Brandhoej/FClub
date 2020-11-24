namespace FClub.UI.Scene
{
	public abstract class BaseMenuComponent<TInput> : IMenuComponent<TInput> where TInput : ISceneInput
	{
		public abstract int X { get; }
		public abstract int Y { get; }

		public abstract BaseMenuComponent<TInput> GetFocusComponent();
		public abstract void Focus(bool newState);
		public abstract void AddChild(IMenuComponent<TInput> child);
		public abstract void HandleInput(TInput input);
		public abstract void Render();
		public abstract void ReRender();
	}
}
