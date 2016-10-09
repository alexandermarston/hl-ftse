using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ftse_scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            string htmlCode = "";

            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://www.hl.co.uk/shares/stock-market-summary/ftse-100");
            }

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(htmlCode);

            foreach (HtmlNode cell in doc.DocumentNode.SelectNodes("//table[@summary='Market index']//tbody/tr"))
            {
                var name = doc.DocumentNode.SelectSingleNode(cell.XPath + "/td[2]").InnerText.ToString().Replace(" plc", " Plc");
                var price = doc.DocumentNode.SelectSingleNode(cell.XPath + "/td[3]").InnerText;
                var change_amount = doc.DocumentNode.SelectSingleNode(cell.XPath + "/td[4]").InnerText;
                var change_percent = doc.DocumentNode.SelectSingleNode(cell.XPath + "/td[5]").InnerText;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(name + " ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(change_amount + " (" + change_percent + ") ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(price);

                Console.WriteLine();
            }


            Console.Read();
        }
    }
}
