# HtmlAgilityPackDart
HtmlAgilityPack의 사용법을 보여주는 샘플 코드
```
WebClient wc = new WebClient();

string url = "https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query=%ED%99%98%EC%9C%A8%EC%A1%B0%ED%9A%8C";
string html = wc.DownloadString(url);

HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
doc.LoadHtml(html);

var tables = doc.DocumentNode.SelectNodes("//table");

var usd_ex_rate = tables?[1]
        .SelectNodes("tbody//tr")?[0]
        .SelectNodes("th|td")?[1]
        .InnerText
        ;
Console.WriteLine($"KB국민은행 제공하는 오늘의 미달러 환율은 : {usd_ex_rate??"not found"}");

Console.ReadKey();
```

```
import pandas as pd
url ='https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query=%ED%99%98%EC%9C%A8%EC%A1%B0%ED%9A%8C'
tables = pd.read_html(url)
tables[1].loc[0,"매매기준율"]
```
