using System;

namespace FClub.UI.Scene.Console
{
	public class ConsoleButton : ConsoleBaseMenuComponent
	{
		public event Action OnClicked;

		public ConsoleButton(string text, Action onClicked)
		{
			AddChild(new ConsoleLabel(text, false));
			OnClicked += onClicked;
		}

		protected override void OnEnterClicked()
		{
			if (IsFocused)
			{
				OnClicked?.Invoke();
			}
		}
	}
}
