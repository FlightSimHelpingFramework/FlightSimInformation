// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Codes;
using StandardLibrary.DataCache.Types;
using StandardLibrary.Sorting;
using WeatherInformation.Core.ServiceTypes;
using WeatherInformation.Core.WeatherStrings;

namespace WeatherInformation.Core.ForCaching.Decorators
{
    /// <summary>
    ///     Decorates <see cref="ITafStringProvider" /> in order to support caching.
    /// </summary>
    public class TafStringCacheSupport : ITafStringProvider
    {
        /// <summary>
        ///     Cache for storing data.
        /// </summary>
        private readonly IDataCache<IcaoCode, TafString> _tafStringCache;

        /// <summary>
        ///     Provider that is being decorated.
        /// </summary>
        private readonly ITafStringProvider _tafStringProvider;

        /// <summary>
        ///     Decorator for adding caching support.
        /// </summary>
        /// <param name="tafStringProvider">TAF string provider to decorate.</param>
        /// <param name="dataCache">Data cache to use.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="tafStringProvider" /> and/or <paramref name="dataCache" />
        ///     are <c>null</c>.
        /// </exception>
        public TafStringCacheSupport([NotNull] ITafStringProvider tafStringProvider,
            [NotNull] IDataCache<IcaoCode, TafString> dataCache)
        {
            NullChecking.ThrowExceptionIfNull(tafStringProvider, nameof(tafStringProvider));
            NullChecking.ThrowExceptionIfNull(dataCache, nameof(dataCache));
            _tafStringProvider = tafStringProvider;
            _tafStringCache = dataCache;
        }

        /// <summary>
        ///     Number of requests that were obtained from the cache.
        /// </summary>
        [UsedImplicitly]
        public int NumberOfRequestsToGetTafFromCache { get; private set; }

        #region Implementation of ITafStatistics

        /// <inheritdoc />
        public int NumberOfRequestsToDownloadTaf { get; private set; }

        #endregion

        #region Implementation of ITafStringProvider

        /// <summary>
        ///     Asynchronously downloads weather information of category <see cref="TafString" /> for specified ICAO codes.
        ///     Before downloading it checks whether result can be obtained from the cache.
        /// </summary>
        /// <param name="icaoCodeContainers">
        ///     Collection of classes that contain ICAO codes of airports to get weather information.
        /// </param>
        /// <returns>
        ///     Task to download the list of <see cref="List{T} " /> <see cref="TafString " /> weather information
        ///     elements according to the specified ICAO codes of airports or to obtain them (or a part of them) from the cache.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCodeContainers" /> is <c>null</c> or
        ///     contains <c>null</c>.
        /// </exception>
        public async Task<List<TafString>> DownloadSeveralTafStringsAsync(
            IReadOnlyList<IIcaoCodeContainer> icaoCodeContainers)
        {
            List<TafString> result = new List<TafString>();
            List<IIcaoCodeContainer> toDownload = new List<IIcaoCodeContainer>();
            foreach (IIcaoCodeContainer icaoCodeContainer in icaoCodeContainers)
            {
                TafString taf = _tafStringCache.Get(icaoCodeContainer.AirportIcaoCode);
                if (taf != null)
                {
                    NumberOfRequestsToGetTafFromCache++;
                    result.Add(taf);
                }
                else
                {
                    toDownload.Add(icaoCodeContainer);
                }
            }

            List<TafString> downloaded = await _tafStringProvider.DownloadSeveralTafStringsAsync(toDownload);
            result.AddRange(downloaded);
            foreach (TafString tafString in downloaded)
            {
                _tafStringCache.Add(tafString.AirportIcaoCode, tafString);
            }

            NumberOfRequestsToDownloadTaf = _tafStringProvider.NumberOfRequestsToDownloadTaf;
            return result.SortedByIcaoCodeInAlphabeticalOrder().ToList();
        }

        /// <summary>
        ///     Asynchronously downloads weather information of category <see cref="TafString" /> for specified ICAO code.
        ///     Before downloading it checks whether result can be obtained from the cache.
        /// </summary>
        /// <param name="icaoCodeContainer">
        ///     Class that contain ICAO code of airport to get weather information.
        /// </param>
        /// <returns>Task to download <see cref="TafString" /> weather information or to obtain it from the cache.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="icaoCodeContainer" /> is <c>null</c>.</exception>
        public async Task<TafString> DownloadTafStringAsync(IIcaoCodeContainer icaoCodeContainer)
        {
            return (await DownloadSeveralTafStringsAsync(new[] { icaoCodeContainer })).First();
        }

        #endregion
    }
}