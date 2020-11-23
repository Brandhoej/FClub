using System;

namespace FClub.UI
{
	public interface IMenuComponentEvent
	{
		ConsoleKeyInfo KeyInfo { get; }
		bool Changed { get; set; }
	}
}
