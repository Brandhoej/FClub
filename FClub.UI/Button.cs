using System;

namespace FClub.UI
{
	public class Button : BaseMenuComponent
	{
		public Button(string text, Action onClicked)
		{
			OnClicked = onClicked;
			AddChild(new InlineLabel(text));
		}

		public Action OnClicked { get; }

		protected override void OnEvent(IMenuComponentEvent menuComponentEvent)
		{
			if (Focus && menuComponentEvent.KeyInfo.Key == ConsoleKey.Enter)
			{
				OnClicked?.Invoke();
			}
			base.OnEvent(menuComponentEvent);
		}
	}
}
