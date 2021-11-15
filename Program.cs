using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace AssesmentTask2
{
   
    public class Program
    {
        public static void Main(string[] args)
        {

            string content = @"{""time"":{""updated"":""Nov 15, 2021 05:27:00 UTC"",""updatedISO"":""2021-11-15T05:27:00+00:00"",""updateduk"":""Nov 15, 2021 at 05:27 GMT""},""disclaimer"":""This data was produced from the CoinDesk Bitcoin Price Index (USD). Non-USD currency data converted using hourly conversion rate from openexchangerates.org"",""chartName"":""Bitcoin"",""bpi"":{""USD"":{""code"":""USD"",""symbol"":""&#36;"",""rate"":""65,849.7367"",""description"":""United States Dollar"",""rate_float"":65849.7367},""GBP"":{""code"":""GBP"",""symbol"":""&pound;"",""rate"":""49,040.6695"",""description"":""British Pound Sterling"",""rate_float"":49040.6695},""EUR"":{""code"":""EUR"",""symbol"":""&euro;"",""rate"":""57,481.2887"",""description"":""Euro"",""rate_float"":57481.2887}}}";
            
            dynamic stuff = JsonConvert.DeserializeObject(content);
            dynamic objParsedata = Newtonsoft.Json.Linq.JObject.Parse(content);


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
