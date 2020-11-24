using System;

namespace FClub.UI.Scene.Console
{
	public class ConsoleButtonTextField : ConsoleTextField
	{
		public event Action<string> OnClicked;

		public ConsoleButtonTextField(Action<string> onClicked)
			: base()
		{
			OnClicked = onClicked;
		}

		protected override void OnEnterClicked()
		{
			if (IsFocused)
			{
				OnClicked?.Invoke(Text);
			}
		}
	}
}
