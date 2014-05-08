using System.Windows.Controls;

namespace DevRainSolutions.TechCompanies.Pages
{
    public partial class Search
    {
        public Search()
        {
            InitializeComponent();
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null)
            {
                var bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpr != null) bindingExpr.UpdateSource();
            }
        }
    }
}