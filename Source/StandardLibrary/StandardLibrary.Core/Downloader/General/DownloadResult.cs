// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Net;
using JetBrains.Annotations;

#endregion

namespace StandardLibrary.Downloader.General
{
    /// <summary>
    ///     Class representing the result of downloading data of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Downloaded data type.</typeparam>
    public class DownloadResult<T>
    {
        /// <summary>
        ///     Constructor of <see cref="DownloadResult{T}" />
        /// </summary>
        /// <param name="downloadedData">Downloaded data of type <typeparamref name="T" />.</param>
        /// <param name="downloadingTime">Time spent to download <paramref name="downloadedData" />.</param>
        /// <param name="responseCode">Response code which represents result status.</param>
        public DownloadResult([CanBeNull] T downloadedData, TimeSpan downloadingTime,
            HttpStatusCode responseCode)
        {
            DownloadedData = downloadedData;
            DownloadingTime = downloadingTime;
            ResponseCode = responseCode;
        }

        /// <summary>
        ///     Object of type <typeparamref name="T" /> which is main download result.
        /// </summary>
        /// <remarks>Check for <c>null</c> before using.</remarks>
        [CanBeNull]
        public T DownloadedData { get; }

        /// <summary>
        ///     Time spent to download data.
        /// </summary>
        public TimeSpan DownloadingTime { get; }

        /// <summary>
        ///     Response code which represents result status.
        /// </summary>
        public HttpStatusCode ResponseCode { get; }
    }
}