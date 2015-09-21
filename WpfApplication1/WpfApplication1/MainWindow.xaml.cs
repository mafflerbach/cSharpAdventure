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
        private dynamic data;
        public MainWindow()
        {
            InitializeComponent();
            tbCurrentPage.Text = "1";
            TbTotalPage.Text = "0";
        }

        private void ListBox_OnInitialized(object sender, EventArgs e)
        {
            this.data = this.getJsonRequest();
            string pageSum = "";
            
            var items = new List<ItemList>();
            this.fillListBox(items, data);
          
            ListBox.ItemsSource = items;

        }


        private void ListBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.data = this.getJsonRequest();
            TbTotalPage.Text = this.data.last_page;
        }


        private void search_Click(object sender, RoutedEventArgs e)
        {
 
            this.updateListBox(0);
        }
        
        private dynamic getJsonRequest(string querry = "*")
        {
            Search foo = new Search();
            foo.page = 1;
            this.data =  foo.Request(querry);

            return this.data;
        }

        private void fillListBox(List<ItemList> items, dynamic data)
        {
            for (var i = 0; i < this.data.results.Count; i++)
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(this.data.results[i].img.ToString());
                items.Add(new ItemList()
                {
                    Title = this.data.results[i].name.ToString(),
                    Image = imageBytes,
                    Rarity = this.data.results[i].rarity.ToString(),
                    restriction_level = this.data.results[i].restriction_level.ToString(),
                    max_offer_unit_price = this.data.results[i].max_offer_unit_price.ToString(),
                    min_sale_unit_price = this.data.results[i].min_sale_unit_price.ToString(),
                    sale_availability = this.data.results[i].sale_availability.ToString(),
                });

            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            this.updateListBox(1);
      
        }
        private void prev_Click(object sender, RoutedEventArgs e)
        {
            this.updateListBox(-1);
        }

        private void updateListBox(int calcPage)
        {
            var searchTerm = SearchTermTextBox.Text;
            if (searchTerm.Equals(""))
            {
                searchTerm = "*";
            }

            Search foo = new Search();
            foo.page = getPage(calcPage);

            this.data = foo.Request(searchTerm);
            tbCurrentPage.Text = foo.page.ToString();
            TbTotalPage.Text = this.data.last_page;

            var items = new List<ItemList>();
            this.fillListBox(items, this.data);
            ListBox.ItemsSource = items;

        }
        

        private int getPage(int calcPage)
        {

            int maxPage = Convert.ToInt32(TbTotalPage.Text);
            int currentPage = Convert.ToInt32(tbCurrentPage.Text);
            int page = 0;
            
            if (currentPage > 1 || currentPage < maxPage)
            {
                page = currentPage + calcPage;
            }
            else
            {
                page = 1;
            }
   
            return page;
        }

    }
}
