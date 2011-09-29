using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeCamp.RIA.UI.Controls
{
	public class ExtendedTextBox : TextBox
	{
		public event EventHandler EnterKeyDown;

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.Key == Key.Enter)
			{
				OnEnterKeyDown();
			}
		}

		protected void OnEnterKeyDown()
		{
			var handler = EnterKeyDown;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}
}
