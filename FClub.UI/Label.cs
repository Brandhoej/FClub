using System;

namespace FClub.UI
{
	public class Label : BaseMenuComponent
	{
		private string m_text;

		public Label(string text)
		{
			m_text = text;
		}

		protected override void OnRender()
		{
			Console.WriteLine(m_text);
		}
	}
}
