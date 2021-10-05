// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using StandardLibrary.Downloader.General;
using StandardLibrary.Downloader.Specific;

#endregion

namespace TestingHelper.Downloader
{
    [ExcludeFromCodeCoverage]
    public class
        DownloaderFromLocalFileWithIcaoCode : IDownloader<DownloadResultWithIcaoCode<string>,
            UrlDownloadRequestWithIcaoCode>
    {
        private static string GetString(UrlDownloadRequest request, out HttpStatusCode responseCode)
        {
            responseCode = HttpStatusCode.OK;
            try
            {
                string a = request.Url.AbsolutePath;
                string b = a.Substring(1, a.Length - 1);
                string s = File.ReadAllText(b);
                return s;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                responseCode = HttpStatusCode.NotFound;
                return null;
            }
        }

        public Task<List<DownloadResultWithIcaoCode<string>>> DownloadForManyRequestsAsync(
            IEnumerable<UrlDownloadRequestWithIcaoCode> downloadRequests)
        {
            return Task.Run(() =>
            {
                List<DownloadResultWithIcaoCode<string>> toReturn = new List<DownloadResultWithIcaoCode<string>>();
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (UrlDownloadRequestWithIcaoCode request in downloadRequests)
                {
                    toReturn.Add(
                        new DownloadResultWithIcaoCode<string>(GetString(request, out HttpStatusCode responseCode),
                            new TimeSpan(), responseCode, request.AirportIcaoCode));
                }

                return toReturn;
            });
        }
    }
}