# AntiScrape #

 AntiScrape is an IIS ASP.NET Http Module to help in the fight against website scrapers!

## How does it work? ##

AntiScrape hooks into the IIS ASP.NET request/response pipeline. 

When your website users request a page, it automatically adds secret hidden links to the page that normal users in web browsers won't see. However, website scrapers that are scanning the HTML of the page will see the link and follow it. 

Once the scraper has followed the secret link, they are recorded as being a scraper, and from then on what happens is entirely up to you!

You can either:

- Return custom content and/or HTTP status code for every other request the scraper makes (even for legitimate content), or
- Introduce a configurable random delay for all requests that the scraper makes, slowing the scraper down, or
- Do nothing, other than log the scraping activity.

The settings for AntiScrape are integrated into the web applications web.config.

## How are the scraping requests logged? ##

The module comes with a reference implementation of the data persistence layer for SQL Server, but other implementations can be easily added by implementing the [IDataStorage](https://github.com/SneakyBrian/AntiScrape/blob/master/AntiScrape.Core/Interfaces/IDataStorage.cs "IDataStorage") interface in an assembly that resides in the web applications bin folder (remembering to remove the assembly containing the SQL Server reference implementation). AntiScrape uses [Microsoft Unity](http://msdn.microsoft.com/en-us/library/ff647202.aspx "Microsoft Unity") to resolve the [IDataStorage](https://github.com/SneakyBrian/AntiScrape/blob/master/AntiScrape.Core/Interfaces/IDataStorage.cs "IDataStorage") interface at run-time.