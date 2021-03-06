﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Api.Item;
using Api.Transactions.current;
using Api.Transactions.history;
using WpfApplication1.Gw;


namespace WpfApplication1 {
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    private dynamic data;

    private int page = 1;

    public MainWindow() {
      CachedImage.FileCache.AppCacheDirectory = string.Format("{0}\\Images2\\",
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
      InitializeComponent();
      tbCurrentPage.Text = "1";
      TbTotalPage.Text = "0";
    }

    private void ListBox_OnInitialized(object sender, EventArgs e) {
      data = getSearchResult(1);
      var pageSum = "";

      var items = new List<ItemList>();
      ListBoxProvider provider = new ListBoxProvider(items, data);
      provider.fillListBox();
      ListBox.ItemsSource = items;
    }

    private void ListBox_OnInitialized1(object sender, EventArgs e) {
      var pageSum = "";

      var test = new Api.Transactions.current.Sells();
      data = test.Request();
      
      var items1 = new List<ItemList>();
      ListBoxProvider provider = new ListBoxProvider(items1, data);

      provider.fillListBox();

      ListBox1.ItemsSource = items1; 
      
    }

    private void ListBox_OnInitialized2(object sender, EventArgs e) {
      var pageSum = "";

      var test = new Api.Transactions.history.Sells();
      data = test.Request();

      var items1 = new List<ItemList>();
      ListBoxProvider provider = new ListBoxProvider(items1, data);

      provider.fillListBox();

      ListBox2.ItemsSource = items1;
    }


    private void ListBox_OnLoaded(object sender, RoutedEventArgs e) {
      data = getSearchResult(1);
      TbTotalPage.Text = data.last_page;
    }


    private void search_Click(object sender, RoutedEventArgs e) {
      data = getSearchResult(1);
      updateListBox(data);
    }
    
    private void next_Click(object sender, RoutedEventArgs e) {
      data = getSearchResult(1);
      updateListBox(data);
    }

    private void prev_Click(object sender, RoutedEventArgs e) {
      data = getSearchResult(-1);
      updateListBox(data);
    }


    private void updateListBox(dynamic data) {
      var items = new List<ItemList>();
      ListBoxProvider provider = new ListBoxProvider(items, data);
      provider.fillListBox();
      ListBox.ItemsSource = items;
      ListBox.ScrollIntoView(ListBox.Items[0]);
    }


    private dynamic getSearchResult(int calcPage) {
      var searchTerm = "*";
      if (SearchTermTextBox != null && !SearchTermTextBox.Text.Equals("")) {
        searchTerm = SearchTermTextBox.Text;
      }
      
      var search = new Search();
      search.page = getPage(calcPage);
      data = search.Request(searchTerm);

      if (TbTotalPage != null) {
        TbTotalPage.Text = data.last_page;
      }

      if (tbCurrentPage != null) {
        tbCurrentPage.Text = search.page.ToString();
      }
      return data;
    }


    private int getPage(int calcPage) {
      int maxPage;
      int currentPage;
      
      if (TbTotalPage != null && !TbTotalPage.Text.Equals("")) {
        maxPage = Convert.ToInt32(TbTotalPage.Text);
      }
      else {
        maxPage = 0;
      }

      if (tbCurrentPage != null && !tbCurrentPage.Text.Equals("")) {
        currentPage = Convert.ToInt32(tbCurrentPage.Text);
      }
      else {
        currentPage = 0;
      }

      var page = 0;

      if (currentPage >= 1 && currentPage < maxPage) {
        page = currentPage + calcPage;
      }
      else {
        page = 1;
      }

      return page;
    }


    private void goTo_Click(object sender, RoutedEventArgs e) {
      data = getSearchResult(0);
      updateListBox(data);

      ListBox.ScrollIntoView(ListBox.Items[0]);
    }
    
    private void MenuItem_Settings(object sender, RoutedEventArgs e) {
      var settings = new ConfigurationWindow();
      settings.Show();
    }
    private void MenuItem_Close(object sender, RoutedEventArgs e) {
      Application.Current.Shutdown();
    }
  }
}