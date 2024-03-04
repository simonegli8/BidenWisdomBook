using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System.Linq;
using System.IO;
using System.Xml.Linq;

public class Quote {

  public int ID;
  public string Text;
  public string Title;
  
}

public class QuoteCategory {

  public string Category;

  public List<Quote> Quotes = new List<Quote>();
}

public class QuotesCollection {

  IWebHostEnvironment hostEnvironment = null;
  public QuotesCollection(IWebHostEnvironment hostEnvironment) {
    this.hostEnvironment = hostEnvironment;
  }
   public QuotesCollection() { }

  public List<QuoteCategory> Categories = new List<QuoteCategory>();

  public void Load() {
    //var path = Path.Combine(hostEnvironment.WebRootPath, "docs/BidenQuotes.xml");
    var path = "wwwroot/docs/BidenQuotes.xml";
    var root = XElement.Load(path);
    int id = 0;
    foreach (XElement xcategory in root.Elements("QuoteCategory")) {

      var category = new QuoteCategory() {
        Category = xcategory.Attribute("name").Value
      };
      Categories.Add(category);

      foreach (var xquote in xcategory.Elements("Quote")) {
        var quote = new Quote() {
          ID = id++,
          Text = xquote.Value,
          Title = xquote.Attribute("Title")?.Value ?? ""
        };
        category.Quotes.Add(quote);
      }
    }
  }
}