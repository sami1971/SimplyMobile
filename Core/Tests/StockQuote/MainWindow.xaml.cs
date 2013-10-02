using System.Linq;
using System.Windows;
using SimplyMobile.Plugins.StockView;

using Stock = SimplyMobile.Plugins.WcfStockService.StockQuote;

namespace StockQuote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.listStockQuotes.ItemsSource = StockViewModel.StockModel.StockQuotes.Data;
            this.dataGridQuotes.ItemsSource = StockViewModel.StockModel.StockQuotes.Data;
        }

        private async void buttonGetQuote_Click(object sender, RoutedEventArgs e)
        {
            var quote = await StockViewModel.StockModel.RefreshOrAdd(this.textSymbol.Text);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.buttonGetQuote.IsEnabled = this.buttonRefresh.IsEnabled = false;
            var stockSymbols = StockViewModel.StockModel.StockQuotes.Data.Select(a => a.Symbol).ToList();

            foreach (var stockSymbol in stockSymbols)
            {
                await StockViewModel.StockModel.RefreshOrAdd(stockSymbol);
            }

            this.buttonGetQuote.IsEnabled = this.buttonRefresh.IsEnabled = true;
        }
    }
}
