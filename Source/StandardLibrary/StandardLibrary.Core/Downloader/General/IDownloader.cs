// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

#endregion

namespace StandardLibrary.Downloader.General
{
    /// <summary>
    ///     Interface of class, that can asynchronously download data of type <typeparamref name="TDownloadResultType" /> by
    ///     collection of <typeparamref name="TDownloadRequestType" /> requests.
    /// </summary>
    /// <typeparam name="TDownloadResultType">
    ///     Download result type for <see cref="DownloadResult{T}" />.
    /// </typeparam>
    /// <typeparam name="TDownloadRequestType">
    ///     Download request type.
    /// </typeparam>
    public interface IDownloader<TDownloadResultType, in TDownloadRequestType>
    {
        /// <summary>
        ///     Performs asynchronous downloading by requests at <paramref name="downloadRequests" />.
        /// </summary>
        /// <param name="downloadRequests">Collection of requests of type <typeparamref name="TDownloadRequestType" />.</param>
        /// <returns>Task for performing downloading and getting <see cref="List{T}" /> as a result.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="downloadRequests" /> is <c>null</c> or contains <c>null</c>.</exception>
        [NotNull]
        [UsedImplicitly]
        Task<List<TDownloadResultType>> DownloadForManyRequestsAsync(
            [NotNull] IEnumerable<TDownloadRequestType> downloadRequests);
    }
}