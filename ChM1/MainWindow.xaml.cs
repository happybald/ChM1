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
using LiveCharts;
using LiveCharts.Wpf;

namespace ChM1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<MyTable> result = new List<MyTable>(3);
            var x0 = new List<double>() { 0 };
            var y0 = new List<double>() { 0 };
            var y1 = new List<double>() { 0 };
            double h = 0.1;
            for (int i = 1; i < 0.7 / h + h; i++)
            {
                x0.Add(x0[i - 1] + h);
                y0.Add((y0[i - 1] + h * Math.Sqrt(1 - y0[i - 1] * y0[i - 1] + h * h)) / (1 + h * h));
                y1.Add((y0[i - 1] - h * Math.Sqrt(1 - y0[i - 1] * y0[i - 1] + h * h)) / (1 + h * h));
                result.Add(new MyTable(x0[i - 1], y0[i - 1], y1[i - 1]));
            }


            var siny = new List<double>() { 0 };
            var sinx = x0;
            for (int i = 1; i < 0.7 / h + h; i++)
            {
                siny.Add(Math.Sin(sinx[i]));
            }
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "+D",
                    Values = new ChartValues<double>(y0)
                },
                new LineSeries
                {
                    Title = "-D",
                    Values = new ChartValues<double>(y1)
                },
                new LineSeries
                {
                    Title = "sin(x)",
                    Values = new ChartValues<double>(siny)
                }
            };
            Labels = x0.Select(x => x.ToString()).ToArray();
            YFormatter = value => value.ToString("F");
            DataContext = this;
            grid.ItemsSource = result;

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        double fx(double y)
        {
            return Math.Sqrt(1 - Math.Pow(y, 2));
        }

        class MyTable
        {
            public MyTable(double x, double y1, double y2)
            {
                this.X = x.ToString("F3");
                this.Y1 = y1.ToString("F10");
                this.Y2 = y2.ToString("F10");
                this.Y = Math.Sin(x).ToString("F10");
                this.D1 = (Convert.ToDouble(this.Y) - y1).ToString("F10");
                this.D2 = (Convert.ToDouble(this.Y) - y2).ToString("F10");
            }
            public string X { get; set; }
            public string Y1 { get; set; }
            public string Y2 { get; set; }
            public string Y { get; set; }
            public string D1 { get; set; }
            public string D2 { get; set; }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            List<MyTable> result = new List<MyTable>(3);
            var x0 = new List<double>() { Convert.ToDouble(xstart.Text.ToString()) };
            var y0 = new List<double>() { 0 };
            var y1 = new List<double>() { 0 };
            double h = Convert.ToDouble(hkrok.Text.ToString());
            for (int i = 1; i < Convert.ToDouble(xfinish.Text.ToString()) / h + h; i++)
            {
                x0.Add(x0[i - 1] + h);
                y0.Add((y0[i - 1] + h * Math.Sqrt(1 - y0[i - 1] * y0[i - 1] + h * h)) / (1 + h * h));
                y1.Add((y0[i - 1] - h * Math.Sqrt(1 - y0[i - 1] * y0[i - 1] + h * h)) / (1 + h * h));
                result.Add(new MyTable(x0[i - 1], y0[i - 1], y1[i - 1]));
            }


            var siny = new List<double>() { 0 };
            var sinx = x0;
            for (int i = 1; i < Convert.ToDouble(xfinish.Text.ToString()) / h + h; i++)
            {
                siny.Add(Math.Sin(sinx[i]));
            }
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "+D",
                    Values = new ChartValues<double>(y0)
                },
                new LineSeries
                {
                    Title = "-D",
                    Values = new ChartValues<double>(y1)
                },
                new LineSeries
                {
                    Title = "sin(x)",
                    Values = new ChartValues<double>(siny)
                }
            };
            Labels = x0.Select(x => x.ToString()).ToArray();
            YFormatter = value => value.ToString("F");
            DataContext = this;
            grid.ItemsSource = result;
        }
    }
}
