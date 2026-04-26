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

namespace ProgressBars
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

        private async Task FillPb(ProgressBar progressBar)
        {
            Random rnd = new Random();

            int randomNumber = rnd.Next(1, 10);

            int i = 0;
            while (true)
            {
                if (i + randomNumber <= 100)
                {
                    i += randomNumber;

                    progressBar.Value = i;
                    await Task.Delay(randomNumber * 100);

                }
                else
                {
                    int did = 100 - i;
                    i += did;

                    progressBar.Value = i;
                    await Task.Delay(did * 100);
                    break;
                }
            }
        }
        public void CreateProgressBar()
        {
            Random _rnd = new Random();
            Color randomColor = Color.FromRgb(
                (byte)_rnd.Next(0, 256),
                (byte)_rnd.Next(0, 256),
                (byte)_rnd.Next(0, 256)
            );
            ProgressBar progressBar = new ProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Height = 20,
                Margin = new Thickness(0, 5, 0, 5),
                Foreground = new SolidColorBrush(randomColor)
            };
            ProgressBarContainer.Children.Add(progressBar);
            _ = FillPb(progressBar);
        }

        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            CreateProgressBar();
            CreateProgressBar();
        }

        private async void Button_Click2(object sender, RoutedEventArgs e)
        {
            CreateProgressBar();
            CreateProgressBar();
            CreateProgressBar();
        }

        private async void Button_Click3(object sender, RoutedEventArgs e)
        {
            ProgressBarContainer.Children.Clear();
        }

        private int finishCounter = 0;

        private object lockObject = new object();

        private List<string> _finishOrder = new List<string>();

        Random rnd = new Random();

        private async Task FillProgresBar(ProgressBar progressBar)
        {
            
            int i = 0;
            while (true)
            {
                int randomNumber = 0;
                lock (lockObject)
                {
                    randomNumber = rnd.Next(1, 10);
                }
                
                if (i + randomNumber <= 100)
                {
                    i += randomNumber;

                    progressBar.Value = i;
                    await Task.Delay(randomNumber * 100);

                }
                else
                {
                    int did = 100 - i;
                    i += did;

                    progressBar.Value = i;
                    await Task.Delay(did * 100);
                    break;
                }
            }
            lock (lockObject)
            {
                if (progressBar.Value >= 100)
                {
                    finishCounter++;
                    _finishOrder.Add(progressBar.Name);
                }
            }
        }

        private async void Start(object sender, RoutedEventArgs e)
        {
            var tasks = new List<Task>();
            tasks.Add(FillProgresBar(Hourse1));
            tasks.Add(FillProgresBar(Hourse2));
            tasks.Add(FillProgresBar(Hourse3));
            tasks.Add(FillProgresBar(Hourse4));
            tasks.Add(FillProgresBar(Hourse5));

            await Task.WhenAll(tasks);

            string message = "";

            foreach (var item in _finishOrder)
            {   
                message += item + "\n";
            }
            MessageBox.Show(message);

            Hourse1.Value = 0;
            Hourse2.Value = 0;
            Hourse3.Value = 0;
            Hourse4.Value = 0;
            Hourse5.Value = 0;

            _finishOrder.Clear();
        }
    }
}