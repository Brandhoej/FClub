using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections;

namespace FClub.UI
{
	public interface IScene<TInput> : IRenderable where TInput : ISceneInput
	{
		IScene<TInput> AddMenus(params IMenuComponent<TInput>[] menuComponents);
		IScene<TInput> AddMenu(IMenuComponent<TInput> menuComponent);
		void HandleInput(TInput input);
		void SetFocus(IMenuComponent<TInput> menuComponent);
	}
}
