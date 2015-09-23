using System;
using System.Collections.Generic;
using System.Windows;
using Api.Item;
using WpfApplication1.Gw;

namespace WpfApplication1 {
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    private dynamic data;

    private int page = 1;

    public MainWindow() {
      InitializeComponent();
      tbCurrentPage.Text = "1";
      TbTotalPage.Text = "0";
    }

    private void ListBox_OnInitialized(object sender, EventArgs e) {
      data = getJsonRequest();
      var pageSum = "";

      var items = new List<ItemList>();
      this.fillListBox(items, data);
      ListBox.ItemsSource = items;
    }

    private void ListBox_OnInitialized1(object sender, EventArgs e) {
      data = getJsonRequest();
      var pageSum = "";

      var items1 = new List<ItemList>();
      this.fillListBox(items1, data);
      ListBox1.ItemsSource = items1;
    }

    private void ListBox_OnInitialized2(object sender, EventArgs e) {
      data = getJsonRequest();
      var pageSum = "";

      var items2 = new List<ItemList>();
      this.fillListBox(items2, data);
      ListBox2.ItemsSource = items2;
    }


    private void ListBox_OnLoaded(object sender, RoutedEventArgs e) {
      data = getJsonRequest();
      TbTotalPage.Text = data.last_page;
    }


    private void search_Click(object sender, RoutedEventArgs e) {
      updateListBox(0);
    }

    private dynamic getJsonRequest(string querry = "*") {
      var foo = new Search();
      foo.page = 1;
      data = foo.Request(querry);

      return data;
    }

    private void fillListBox(List<ItemList> items, dynamic data) {
      for (var i = 0; i < this.data.results.Count; i++) {
        items.Add(new ItemList {
          Title = this.data.results[i].name.ToString(),
          Image = "Images/" + this.data.results[i].data_id + ".png",
          Rarity = this.data.results[i].rarity.ToString(),
          restriction_level = this.data.results[i].restriction_level.ToString(),
          max_offer_unit_price = this.formatMoney(this.data.results[i].max_offer_unit_price.ToString()),
          min_sale_unit_price = this.formatMoney(this.data.results[i].min_sale_unit_price.ToString()),
          sale_availability = this.data.results[i].sale_availability.ToString()
        });
      }
    }

    private void next_Click(object sender, RoutedEventArgs e) {
      updateListBox(1);
    }

    private void prev_Click(object sender, RoutedEventArgs e) {
      updateListBox(-1);
    }

    private string formatMoney(string money) {
      var mee = Reverse(money);

      if (mee.Length > 2) {
        mee = mee.Insert(2, " ");
      }

      if (mee.Length > 5) {
        mee = mee.Insert(5, " ");
      }

      return Reverse(mee);
    }

    private string Reverse(string s) {
      var charArray = s.ToCharArray();
      Array.Reverse(charArray);
      return new string(charArray);
    }

    /// <summary>
    /// </summary>
    /// <param name="calcPage"></param>
    private void updateListBox(int calcPage) {
      var searchTerm = SearchTermTextBox.Text;
      if (searchTerm.Equals("")) {
        searchTerm = "*";
      }

      var foo = new Search();
      foo.page = getPage(calcPage);

      data = foo.Request(searchTerm);
      tbCurrentPage.Text = foo.page.ToString();
      TbTotalPage.Text = data.last_page;

      var items = new List<ItemList>();
      this.fillListBox(items, data);
      ListBox.ItemsSource = items;
      ListBox.ScrollIntoView(ListBox.Items[0]);
    }


    private int getPage(int calcPage) {
      var maxPage = Convert.ToInt32(TbTotalPage.Text);
      var currentPage = Convert.ToInt32(tbCurrentPage.Text);
      var page = 0;

      if (currentPage > 1 || currentPage < maxPage) {
        page = currentPage + calcPage;
      }
      else {
        page = 1;
      }

      return page;
    }


    private void goTo_Click(object sender, RoutedEventArgs e) {

      var currentPage = Convert.ToInt32(tbCurrentPage.Text);
      var foo = new Search();
      foo.page = currentPage;
      var searchTerm = SearchTermTextBox.Text;
      if (searchTerm.Equals(""))
      {
        searchTerm = "*";
      }
      data = foo.Request(searchTerm);
      tbCurrentPage.Text = currentPage.ToString();
      TbTotalPage.Text = data.last_page;

      var items = new List<ItemList>();
      this.fillListBox(items, data);
      ListBox.ItemsSource = items;
      ListBox.ScrollIntoView(ListBox.Items[0]);

      Console.WriteLine(tbCurrentPage.Text);
    }
  }
}