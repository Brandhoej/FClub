using System;

namespace FClub.UI.Scene
{
	public interface IMenuComponentEvent
	{
		ConsoleKeyInfo KeyInfo { get; }
		bool Changed { get; set; }
	}
}
