using System.Windows;
using System.Windows.Controls;

namespace CodeCamp.RIA.UI.Helpers
{
	/// <summary>
	/// Helper that updates binding right after the UI control content was changed 
	/// (not after the control's focus was lost).
	/// </summary>
	public static class BindingHelper
	{
		public static bool GetUpdateSourceOnChange(DependencyObject obj)
		{
			return (bool)obj.GetValue(UpdateSourceOnChangeProperty);
		}

		public static void SetUpdateSourceOnChange(DependencyObject obj, bool value)
		{
			obj.SetValue(UpdateSourceOnChangeProperty, value);
		}

		public static readonly DependencyProperty UpdateSourceOnChangeProperty =
			DependencyProperty.RegisterAttached("UpdateSourceOnChange", typeof(bool),
			typeof(BindingHelper), new PropertyMetadata(false, OnPropertyChanged));

		private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var textBox = obj as TextBox;
			if (textBox != null)
			{
				if ((bool)e.NewValue)
				{
					textBox.TextChanged += TextBox_TextChanged;
				}
				else
				{
					textBox.TextChanged -= TextBox_TextChanged;
				}
			}
		}

		private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = sender as TextBox;
			if (textBox != null)
			{
				var binding = textBox.GetBindingExpression(TextBox.TextProperty);
				if (binding != null)
				{
					binding.UpdateSource();
				}
			}
		}
	}
}
