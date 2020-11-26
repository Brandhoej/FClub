using System;
using System.Collections.Generic;

namespace FClub.UI.Scene.Console
{
	public interface IConsoleMenuBuilder
	{
		IConsoleMenuBuilder AddForeach<TIn>(IEnumerable<TIn> enumerable, Action<TIn> action);
		IConsoleMenuBuilder AddMenu(ConsoleBaseMenuComponent component);
		IConsoleMenuBuilder MultipleNext(int amount);
		IConsoleMenuBuilder AddButton(string text, Action onClick);
		IConsoleMenuBuilder AddLabel(string text = "", bool endLine = true);
		IConsoleMenuBuilder AddButtonTextField(Action<string> onClick);
		IConsoleMenuBuilder AddLineSpacer();
		IConsoleMenuBuilder AddUniNavigationTo(SceneNavigationDirection direction);
		IConsoleMenuBuilder AddBiNavigationTo(SceneNavigationDirection direction);
		IConsoleMenuBuilder AddUniNavigationFrom(SceneNavigationDirection direction, ConsoleBaseMenuComponent source);
		IConsoleMenuBuilder AddBiNavigationFrom(SceneNavigationDirection direction, ConsoleBaseMenuComponent source);
		IConsoleMenuBuilder AddUniNavigationTo(SceneNavigationDirection direction, ConsoleBaseMenuComponent destination);
		IConsoleMenuBuilder AddBiNavigationTo(SceneNavigationDirection direction, ConsoleBaseMenuComponent destination);
		IConsoleMenuBuilder IgnoreNext();
		IConsoleMenuBuilder Focus();
		ConsoleBaseMenuComponent Build();
	}
}
