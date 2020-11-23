using System;

namespace FClub.UI
{
	public class InlineLabel : BaseMenuComponent
	{
		private string m_text;

		public InlineLabel(string text)
		{
			m_text = text;
		}

		protected override void OnRender()
		{
			Console.Write(m_text);
		}
	}
}
