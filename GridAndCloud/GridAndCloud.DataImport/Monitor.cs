using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridAndCloud.DataImport
{
    [Serializable]
    public class Monitor
    {
        public string ItemName { get; set; }
        public string Price { get; set; }
        public string Provider { get; set; }
        public string Photos { get; set; }
        public string PhotosLinks { get; set; }
        public string Characteristics { get; set; }
        public string Color { get; set; }

        public Dictionary<string, string> chars 
        { 
            get 
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Characteristics);

                return doc.DocumentNode.SelectSingleNode("//table")
                            .Descendants("tr")
                            .Skip(1)
                            .Where(tr => tr.Elements("td").Count() > 1)
                            .ToDictionary(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).First(), tr => tr.Elements("td").Select(td => td.InnerText.Trim()).Last());
            } 
        }
    }
}
