using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using WpfApplication1.Gw;
using Api.Item;

namespace WpfApplication1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int page = 1;
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("Main");
            tbCurrentPage.Text = "1";
            TbTotalPage.Text = "0";
            // wg reihenfolge
            //TbTotalPage.Text = pageSum;

        }

        private void ListBox_OnInitialized(object sender, EventArgs e)
        {
            var data = this.getJsonRequest();

            string pageSum = "";
            
            var items = new List<ItemList>();
            this.fillListBox(items, data);
          
            ListBox.ItemsSource = items;

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchTermTextBox.Text;

            Search foo = new Search();
            foo.page = 1;
            tbCurrentPage.Text = "1";
            dynamic data = foo.Request(searchTerm);
            TbTotalPage.Text = data.last_page.ToString();

            var items = new List<ItemList>();
            this.fillListBox(items, data);
            ListBox.ItemsSource = items;
        }
        
        private dynamic getJsonRequest(string querry = "*")
        {
            Search foo = new Search();
            foo.page = 1;
            dynamic data =  foo.Request(querry);

            return data;
        }

        private void fillListBox(List<ItemList> items, dynamic data)
        {
            for (var i = 0; i < data.results.Count; i++)
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(data.results[i].img.ToString());
                items.Add(new ItemList()
                {
                    Title = data.results[i].name.ToString(),
                    Image = imageBytes,
                    Rarity = data.results[i].rarity.ToString(),
                    restriction_level = data.results[i].restriction_level.ToString(),
                    max_offer_unit_price = data.results[i].max_offer_unit_price.ToString(),
                    min_sale_unit_price = data.results[i].min_sale_unit_price.ToString(),
                    sale_availability = data.results[i].sale_availability.ToString(),
                });

            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            
            var searchTerm = SearchTermTextBox.Text;
            if (searchTerm.Equals(""))
            {
                searchTerm = "*";
            }
            
            Search foo = new Search();

            //todo Convert.ToInt32(data.last_page)
            if (Convert.ToInt32(tbCurrentPage.Text) < 10)
            {
                foo.page = Convert.ToInt32(tbCurrentPage.Text) + 1;
            }
            else
            {
                foo.page = 1;
            }
            dynamic data = foo.Request(searchTerm);
            tbCurrentPage.Text = foo.page.ToString();
            TbTotalPage.Text = data.last_page.ToString();
            var items = new List<ItemList>();
            this.fillListBox(items, data);
            ListBox.ItemsSource = items;
        }
        private void prev_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchTermTextBox.Text;
            if (searchTerm.Equals(""))
            {
                searchTerm = "*";
            }

            Search foo = new Search();

            if (Convert.ToInt32(tbCurrentPage.Text) > 1)
            {
                foo.page = Convert.ToInt32(tbCurrentPage.Text) - 1;
            }
            else
            {
                foo.page = 1;
            }

            dynamic data = foo.Request(searchTerm);
            tbCurrentPage.Text = foo.page.ToString();
            TbTotalPage.Text = data.last_page.ToString();

            var items = new List<ItemList>();
            this.fillListBox(items, data);
            ListBox.ItemsSource = items;
        }
    }
}
