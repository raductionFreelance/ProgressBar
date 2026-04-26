using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgressBarT3
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
                if (!int.TryParse(FibonacciCountTextBox.Text, out int value) || value < 0)
                {
                    MessageBox.Show("Будь ласка, введіть коректне ціле число (0 або більше).");
                    return;
                }

                await Task.Run(() => {
                    List<long> fibonacciNumbers = new List<long>();

                    fibonacciNumbers.Add(0);
                    fibonacciNumbers.Add(1);

                    long fib1 = 0;
                    long fib2 = 1;

                    long num = 0;

                    while (num <= value)
                    {
                        num = fib1 + fib2;
                        if (num <= value)
                        {
                            fibonacciNumbers.Add(num);
                        }
                        long temp = fib1;
                        fib1 = fib2;
                        fib2 = temp + fib2;
                    }

                    Dispatcher.Invoke(() =>
                    {
                        res.Text = fibonacciNumbers.Count.ToString();
                    });
                });
        }
    }
}