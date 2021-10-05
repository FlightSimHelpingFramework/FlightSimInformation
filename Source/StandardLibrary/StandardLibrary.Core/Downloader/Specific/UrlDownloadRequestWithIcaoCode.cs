// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Codes;
using StandardLibrary.Downloader.General;

#endregion

namespace StandardLibrary.Downloader.Specific
{
    /// <summary>
    ///     Request for downloading data by URL. Request contains airport ICAO code <see cref="IcaoCode" />.
    /// </summary>
    public class UrlDownloadRequestWithIcaoCode : UrlDownloadRequest, IIcaoCodeContainer
    {
        /// <summary>
        ///     URL download request with information about airport ICAO code constructor.
        /// </summary>
        /// <param name="url">URL for downloading data.</param>
        /// <param name="icaoCode">Airport ICAO code to which this request refers to.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="url" /> or <paramref name="icaoCode" /> is <c>null</c>.
        /// </exception>
        public UrlDownloadRequestWithIcaoCode([NotNull] Uri url, [NotNull] IcaoCode icaoCode) : base(url)
        {
            NullChecking.ThrowExceptionIfNull(icaoCode, nameof(IcaoCode));

            AirportIcaoCode = icaoCode;
        }

        #region Overrides of Object

        /// <summary>Gives string representation.</summary>
        /// <returns>String representing current object.</returns>
        public override string ToString()
        {
            return $"Download request for airport {AirportIcaoCode} with URL: {Url}";
        }

        #endregion


        /// <summary>
        ///     Airport ICAO code to which this request refers to.
        /// </summary>
        [NotNull]
        public IcaoCode AirportIcaoCode { get; }
    }
}