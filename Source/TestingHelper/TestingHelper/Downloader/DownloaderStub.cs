// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using StandardLibrary.Downloader.General;

namespace TestingHelper.Downloader
{
    [ExcludeFromCodeCoverage]
    public class DownloaderStub<TResult, TRequest> : IDownloader<TResult, TRequest>
    {
        public Task<List<TResult>> DownloadForManyRequestsAsync(IEnumerable<TRequest> downloadRequests)
        {
            List<TResult> l = new List<TResult>(1);
            return Task.Run(() => l);
        }
    }
}