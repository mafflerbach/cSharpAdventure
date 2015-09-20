using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows;
using WpfApplication1.Gw;
using GW2NET;
using GW2NET.Common;
using System.IO;
using System.Windows.Controls;

namespace WpfApplication1
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

        private void ListBox_OnInitialized(object sender, EventArgs e)
        {
            var data = this.getJsonRequest();
            var items = new List<ItemList>();
            this.fillListBox(items, data);
            ListBox.ItemsSource = items;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchTermTextBox.Text;

            var data = this.getJsonRequest(searchTerm);
            var items = new List<ItemList>();
            this.fillListBox(items, data);
            ListBox.ItemsSource = items;
        }

        private static bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }

            if ((response.StatusCode == HttpStatusCode.OK ||
                 response.StatusCode == HttpStatusCode.Moved ||
                 response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
                return true;
            }
            else
            {
                return false;
            }
        }




        private dynamic getJsonRequest(string querry = "*")
        {
            var url = "http://www.gw2spidy.com/api/v0.9/json/item-search/"+querry+"/1";
            WebClient client = new WebClient();

            string s = client.DownloadString(url);

            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(s);

            return data;
        }

        private void fillListBox(List<ItemList> items, dynamic data)
        {
            for (var i = 0; i < data.results.Count; i++)
            {
                dynamic item = data.results[i];
                Console.WriteLine(data.results[i].name.ToString());
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
    }



}
