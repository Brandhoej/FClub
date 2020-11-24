namespace FClub.UI.Scene
{
	public interface IScene<TInput> : IRenderable where TInput : ISceneInput
	{
		IScene<TInput> AddMenus(params IMenuComponent<TInput>[] menuComponents);
		IScene<TInput> AddMenu(IMenuComponent<TInput> menuComponent);
		void HandleInput(TInput input);
		void SetFocus(IMenuComponent<TInput> menuComponent);
		IScene<TInput> SetNavigationFor(IMenuComponent<TInput> from, ISceneNavigationDirection direction, IMenuComponent<TInput> to);
	}
}
