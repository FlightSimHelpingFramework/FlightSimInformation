// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Net;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Codes;
using StandardLibrary.Downloader.General;

#endregion

namespace StandardLibrary.Downloader.Specific
{
    /// <summary>
    ///     Class representing the result of downloading data of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Downloaded data type.</typeparam>
    public class DownloadResultWithIcaoCode<T> : DownloadResult<T>, IIcaoCodeContainer
    {
        /// <summary>
        ///     Constructor of <see cref="DownloadResult{T}" />
        /// </summary>
        /// <param name="downloadedData">Downloaded data of type <typeparamref name="T" />.</param>
        /// <param name="downloadingTime">Time spent to download <paramref name="downloadedData" />.</param>
        /// <param name="responseCode">Response code which represents result status.</param>
        /// <param name="airportIcaoCode">Airport ICAO code to which this result refers to.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="airportIcaoCode" /> is <c>null</c>.
        /// </exception>
        public DownloadResultWithIcaoCode([CanBeNull] T downloadedData, TimeSpan downloadingTime,
            HttpStatusCode responseCode, [NotNull] IcaoCode airportIcaoCode) : base(downloadedData,
            downloadingTime, responseCode)
        {
            NullChecking.ThrowExceptionIfNull(airportIcaoCode, nameof(airportIcaoCode));
            AirportIcaoCode = airportIcaoCode;
        }

        #region Implementation of IIcaoCodeContainer

        /// <inheritdoc />
        public IcaoCode AirportIcaoCode { get; }

        #endregion
    }
}