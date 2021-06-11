using System;
using System.Globalization;
using System.IO;
using d03.Nasa.Apod;
using d03.Nasa.NeoWs;
using d03.Nasa.NeoWs.Models;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").SetBasePath(Directory.GetCurrentDirectory());
var dataConfiguration = builder.Build();
var apiKey = dataConfiguration.GetSection("ApiKey").Value;

if (args.Length != 2)
{
    Console.WriteLine("Введенные данные некорректны, неверное количество аргументов");
    return;
}

int resultCount;
if (Int32.TryParse(args[1], out resultCount) == false)
{
    Console.WriteLine("Введите корректно второй аргумент");
    return ;
}

if (args[0] == "apod")
{
    var apodClient = new ApodClient(apiKey);
    try
    {
        var resultsApod = await apodClient.GetAsync(resultCount);
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");  
        foreach (var resultApod in resultsApod)
        {
            Console.WriteLine(resultApod);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
else if (args[0] == "neows")
{
    var startDate = dataConfiguration.GetSection("NeoWS").GetSection("StartDate").Value;
    var endDate = dataConfiguration.GetSection("NeoWS").GetSection("EndDate").Value;

    DateTime startDateParse;
    DateTime endDateParse;
    if (DateTime.TryParse(startDate, out startDateParse) == false || DateTime.TryParse(endDate, out endDateParse) == false)
    {
        Console.WriteLine("Конфигурация файла некорректна (дата)");
        return;
    }
    var neowsClient = new NeoWsClient(apiKey);
    try
    {
        var resultsNeoWs = await neowsClient.GetAsync(new AsteroidRequest(startDateParse, endDateParse, resultCount));
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");  
        foreach (var resultNeoWs in resultsNeoWs)
        {
            Console.WriteLine(resultNeoWs);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}