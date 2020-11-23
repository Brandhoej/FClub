using System;

namespace FClub.UI
{
	public class ExecutableTextbox : Textbox
	{
		public event Action<string> OnEnter;

		public ExecutableTextbox(Action<string> onEnter)
		{
			OnEnter += onEnter;
		}

		protected override void OnEvent(IMenuComponentEvent menuComponentEvent)
		{
			if (Focus && menuComponentEvent.KeyInfo.Key == ConsoleKey.Enter)
			{
				OnEnter?.Invoke(Text);
			}
			else
			{
				base.OnEvent(menuComponentEvent);
			}
		}
	}
}
