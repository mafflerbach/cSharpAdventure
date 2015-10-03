using System;
using System.Collections.Generic;
using CachedImage;

namespace WpfApplication1.Gw {
  public class ListBoxProvider {
    private dynamic data;
    private List<ItemList> items;


    public ListBoxProvider(List<ItemList> items, dynamic data) {
      this.data = data;
      this.items = items;
    }

    public void fillListBox() {
      for (var i = 0; i < this.data.results.Count; i++) {
        var image = new Image();
        image.ImageUrl = this.data.results[i].img;

        items.Add(new ItemList {
          Title = this.data.results[i].name.ToString(),
          Image = image.ImageUrl,
          Rarity = this.data.results[i].rarity.ToString(),
          restriction_level = this.data.results[i].restriction_level.ToString(),
          max_offer_unit_price = this.formatMoney(this.data.results[i].max_offer_unit_price.ToString()),
          min_sale_unit_price = this.formatMoney(this.data.results[i].min_sale_unit_price.ToString()),
          sale_availability = this.data.results[i].sale_availability.ToString(),
          data_id = this.data.results[i].data_id.ToString()
        });
      }
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
  }
}