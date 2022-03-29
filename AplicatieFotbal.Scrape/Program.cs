using AplicatieFotbal.Models;
using AplicatieFotbal.Utility;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace AplicatieFotbal.Scrape
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var matches = GetData.GetMatches(new DateTime(2021, 1, 1), DateTime.Now.AddDays(-1));
        }

    }
}
