using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConvertNumberToCurrencyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string intPart = "";
            string doublePart = "";

            ServiceReference1.ServiceConverterClient serviceConverter = new ServiceReference1.ServiceConverterClient();

            intPart = TextBoxInt.Text;

            if(TextBoxDouble.Text.Length > 2)
            {
                MessageBox.Show("Double part of the number must be <= 99.");
            }
            else
            {
                if (TextBoxDouble.Text.Length == 0 || Int64.Parse(TextBoxDouble.Text) == 0)
                {
                    doublePart = "0";
                }
                else
                {
                    doublePart = TextBoxDouble.Text;
                }

                string number = intPart + "." + doublePart;
                decimal decimalNumber = Decimal.Parse(number);

                string output = serviceConverter.ConvertCurrencyToWords(decimalNumber);

                MessageBox.Show(output);
            }
            
        }

        /// <summary>
        /// Deleting the mask text from TextBoxt when focus.
        /// </summary>
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
    }
}
