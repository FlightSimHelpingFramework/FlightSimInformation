// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace TestingHelper.Downloader
{
    [ExcludeFromCodeCoverage]
    public static class Urls
    {
        public static readonly string[] InvalidStringUrlsCollection =
        {
            "127.0.0.1",
            "www.example.com",
            "example.com"
        };


        public static readonly string[] ValidStringUrlsCollection =
        {
            "http://www.example.com",
            "https://www.example.com",

            "http://example1.example.com",
            "https://example1.example.com",

            "http://www.example.com/page",
            "https://www.example.com/page",

            "http://www.example.com/page?id=1&product=2",
            "https://www.example.com/page?id=1&product=2",

            "http://www.example.com/page#start",
            "https://www.example.com/page#start",

            "http://www.example.com:8080",
            "https://www.example.com:8080",

            "http://127.0.0.1",
            "https://127.0.0.1",

            "ftp://127.0.0.1"
        };

        public static readonly Uri[] ValidUrlsCollection =
        {
            new Uri("http://www.example.com"),
            new Uri("https://www.example.com"),

            new Uri("http://example1.example.com"),

            new Uri("http://www.example.com/page"),

            new Uri("http://www.example.com/page?id=1&product=2"),

            new Uri("http://www.example.com/page#start"),

            new Uri("http://www.example.com:8080"),

            new Uri("http://127.0.0.1")
        };
    }
}