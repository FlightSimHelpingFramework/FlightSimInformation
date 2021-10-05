// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StandardLibrary.Codes;
using StandardLibrary.Downloader.General;
using StandardLibrary.Downloader.Specific;
using StandardLibrary.Implementations.Downloader.Specific;

#endregion

namespace StandardLibrary.Implementations.Downloader.General
{
    /// <summary>
    ///     Standard implementation of downloader by <see cref="UrlDownloadRequest" />.
    /// </summary>
    /// <remarks>
    ///     This class may be used in case there is a need to download some data (represented as a <see cref="string" /> as a
    ///     result). (Without specifying ICAO code).
    /// </remarks>
    public class StandardUrlDownloader : IDownloader<DownloadResult<string>,
        UrlDownloadRequest>
    {
        #region Implementation of IDownloader<DownloadResult<string>,UrlDownloadRequest>

        /// <inheritdoc />
        public Task<List<DownloadResult<string>>> DownloadForManyRequestsAsync(
            IEnumerable<UrlDownloadRequest> downloadRequests)
        {
            List<UrlDownloadRequestWithIcaoCode> requestsWithIcao = downloadRequests
                .Select(x => new UrlDownloadRequestWithIcaoCode(x.Url, new IcaoCode("NONE")))
                .ToList();

            return Task.Run(async () =>
            {
                return (await new StandardUrlDownloaderWithIcaoCode().DownloadForManyRequestsAsync(requestsWithIcao))
                    .Select(x => new DownloadResult<string>(x.DownloadedData, x.DownloadingTime, x.ResponseCode))
                    .ToList();
            });
        }

        #endregion
    }
}