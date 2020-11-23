using System;

namespace FClub.UI
{
	public class MenuComponentEvent : IMenuComponentEvent
	{
		public MenuComponentEvent(ConsoleKeyInfo key)
		{
			KeyInfo = key;
		}

		public ConsoleKeyInfo KeyInfo { get; }
		public bool Changed { get; set; }
	}
}
