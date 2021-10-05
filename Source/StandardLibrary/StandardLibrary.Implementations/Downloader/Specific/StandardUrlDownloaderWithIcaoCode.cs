// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Downloader.General;
using StandardLibrary.Downloader.Specific;

#endregion

namespace StandardLibrary.Implementations.Downloader.Specific
{
    /// <summary>
    ///     Standard implementation of downloader by <see cref="UrlDownloadRequestWithIcaoCode" />.
    /// </summary>
    /// <remarks>
    ///     This class may be used in case there is a need to download some data (represented as a <see cref="string" /> as a
    ///     result) which is related to specific airport ICAO code.
    /// </remarks>
    public class
        StandardUrlDownloaderWithIcaoCode : IDownloader<DownloadResultWithIcaoCode<string>,
            UrlDownloadRequestWithIcaoCode>
    {
        /// <summary>
        ///     Gets <see cref="HttpResponseMessage" /> as a result of  downloading data with URL <paramref name="url" />
        /// </summary>
        /// <param name="url">URL for downloading data.</param>
        /// <returns><see cref="HttpResponseMessage" /> from downloading data with URL .</returns>
        private static HttpResponseMessage GetResponseByUrl(Uri url)
        {
            try
            {
                using HttpClient client = new HttpClient();
                return client.GetAsync(url).Result;
            }
            catch
            {
                return null;
            }
        }

        #region Implementation of IDownloader<DownloadResultWithIcaoCode<string>,UrlDownloadRequestWithIcaoCode>

        /// <summary>
        ///     Performs asynchronous downloading by requests at <paramref name="downloadRequests" />.
        /// </summary>
        /// <param name="downloadRequests">Collection of requests of type <see cref="UrlDownloadRequestWithIcaoCode" />.</param>
        /// <returns>
        ///     Task for performing downloading and getting <see cref="List{T}" />  of type
        ///     <see cref="DownloadResultWithIcaoCode{S}" /> of <see cref="string" /> as a result.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="downloadRequests" /> is <c>null</c> or contains <c>null</c>.</exception>
        /// <remarks>
        ///     This materializes <paramref name="downloadRequests" /> before executing main code.
        ///     If it is impossible to get data (result) of any kind from a specific request from
        ///     <paramref name="downloadRequests" /> for any reason, this result is not added to returned collection. Before using
        ///     the resulted collection, ensure it is not empty.
        /// </remarks>
        public Task<List<DownloadResultWithIcaoCode<string>>> DownloadForManyRequestsAsync(
            IEnumerable<UrlDownloadRequestWithIcaoCode> downloadRequests)
        {
            //Protection from multiple enumeration
            IEnumerable<UrlDownloadRequestWithIcaoCode> urlDownloadRequests =
                downloadRequests as UrlDownloadRequestWithIcaoCode[] ?? downloadRequests.ToArray();

            NullChecking.ThrowExceptionIfNullOrContainsNull(urlDownloadRequests, nameof(downloadRequests));

            return Task.Run(() =>
            {
                BlockingCollection<DownloadResultWithIcaoCode<string>> downloadResults =
                    new BlockingCollection<DownloadResultWithIcaoCode<string>>();

                Parallel.ForEach(urlDownloadRequests, new ParallelOptions { MaxDegreeOfParallelism = 20 },
                    request =>
                    {
                        Stopwatch swStopwatch = new Stopwatch();
                        swStopwatch.Start();
                        HttpResponseMessage response = GetResponseByUrl(request.Url);
                        swStopwatch.Stop();
                        if (response != null)
                        {
                            downloadResults.Add(
                                new DownloadResultWithIcaoCode<string>(
                                    response.Content.ReadAsStringAsync().Result,
                                    swStopwatch.Elapsed,
                                    response.StatusCode,
                                    request.AirportIcaoCode)
                            );
                        }
                    });
                return downloadResults.ToList();
            });
        }

        #endregion
    }
}