// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;

#endregion

namespace StandardLibrary.Downloader.General
{
    /// <summary>
    ///     Request for downloading data by URL.
    /// </summary>
    public class UrlDownloadRequest
    {
        /// <summary>
        ///     URL download request constructor.
        /// </summary>
        /// <param name="url">URL for downloading data.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="url" /> is <c>null</c>.
        /// </exception>
        public UrlDownloadRequest([NotNull] Uri url)
        {
            NullChecking.ThrowExceptionIfNull(url, nameof(url));

            Url = url;
        }

        /// <summary>
        ///     URL for downloading data.
        /// </summary>
        [NotNull]
        public Uri Url { get; }

        #region Overrides of Object

        /// <summary>Gives string representation.</summary>
        /// <returns>String representing current object.</returns>
        public override string ToString()
        {
            return $"Download request with URL: {Url}";
        }

        #endregion
    }
}