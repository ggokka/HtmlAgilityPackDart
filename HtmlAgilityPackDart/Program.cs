using HtmlAgilityPack;
using System;
using System.Collections;
using System.Net;
using System.Text;

/// <summary>
/// http://dart.fss.or.kr/report/viewer.do?rcpNo=20190903900115&dcmNo=6868945&eleId=0&offset=0&length=0&dtd=HTML
/// </summary>
namespace HtmlAgilityPackDart
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            int ksc5601Codepage = 51949; 
            Encoding ks_c_5601 = System.Text.Encoding.GetEncoding(ksc5601Codepage);

            Hashtable hashtable = new Hashtable();

            WebClient wc = new WebClient();
            //wc.Encoding = System.Text.Encoding.Default;
            //wc.Encoding = System.Text.Encoding.UTF8;
            //wc.Encoding = System.Text.Encoding.GetEncoding(euckrCodepage);
            wc.Encoding = ks_c_5601;

            string key1 = "20190903900115";
            string html = wc.DownloadString("http://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + key1);
            int location = html.IndexOf("viewDoc('" + key1 + "', ");
            string key2 = html.Substring(location + 27, 7);

            string url = "http://dart.fss.or.kr/report/viewer.do?rcpNo=" + key1 + "&dcmNo=" + key2 + "&eleId=0&offset=0&length=0&dtd=HTML";
            Console.WriteLine(url);
            string html2 = wc.DownloadString(url);
            Console.WriteLine(html2);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html2);
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode tbody in table.SelectNodes("tbody"))
                {
                    foreach (HtmlNode row in tbody.SelectNodes("tr"))
                    {
                        HtmlNodeCollection cell = row.SelectNodes("th|td");
                        if (cell.Count > 1 && cell[0].InnerText.Contains("최근 3년간 동종계약"))
                        {

                            Console.WriteLine("최근 3년간 동종계약: " + cell[1].InnerText.Trim());
                        }
                        if (cell.Count > 1 && cell[0].InnerText.Contains("매출액 대비"))
                        {
                            Console.WriteLine("매출액 대비: " + cell[1].InnerText.Trim());
                        }
                        //Console.WriteLine(cell[0].InnerText);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
