namespace WpfApplication1.Gw {
  internal interface Api {
    string Response();
    dynamic Request(string url);
  }
}