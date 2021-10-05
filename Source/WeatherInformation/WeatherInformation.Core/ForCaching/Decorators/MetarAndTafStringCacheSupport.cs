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
    ///     Decorates <see cref="IMetarAndTafStringProvider" /> in order to support caching.
    /// </summary>
    public class MetarAndTafStringCacheSupport : IMetarAndTafStringProvider
    {
        /// <summary>
        ///     Cache for storing data.
        /// </summary>
        private readonly IDataCache<IcaoCode, MetarAndTafString> _metarAndTafStringCache;

        /// <summary>
        ///     Provider that is being decorated.
        /// </summary>
        private readonly IMetarAndTafStringProvider _metarAndTafStringProvider;

        /// <summary>
        ///     Decorator for adding caching support.
        /// </summary>
        /// <param name="metarAndTafStringProvider">METAR and TAF string provider to decorate.</param>
        /// <param name="dataCache">Data cache to use.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="metarAndTafStringProvider" /> and/or
        ///     <paramref name="dataCache" /> are <c>null</c>.
        /// </exception>
        public MetarAndTafStringCacheSupport([NotNull] IMetarAndTafStringProvider metarAndTafStringProvider,
            [NotNull] IDataCache<IcaoCode, MetarAndTafString> dataCache)
        {
            NullChecking.ThrowExceptionIfNull(metarAndTafStringProvider, nameof(metarAndTafStringProvider));
            NullChecking.ThrowExceptionIfNull(dataCache, nameof(dataCache));
            _metarAndTafStringProvider = metarAndTafStringProvider;
            _metarAndTafStringCache = dataCache;
        }

        /// <summary>
        ///     Number of requests that were obtained from the cache.
        /// </summary>
        [UsedImplicitly]
        public int NumberOfRequestsToGetMetarAndTafFromCache { get; private set; }

        #region Implementation of IMetarStatistics

        /// <inheritdoc />
        public int NumberOfRequestsToDownloadMetar { get; private set; }

        #endregion

        #region Implementation of ITafStatistics

        /// <inheritdoc />
        public int NumberOfRequestsToDownloadTaf { get; private set; }

        #endregion

        #region Implementation of IMetarAndTafStringProvider

        /// <summary>
        ///     Asynchronously downloads weather information of category <see cref="MetarAndTafString" /> for specified ICAO code.
        ///     Before downloading it checks whether result can be obtained from the cache.
        /// </summary>
        /// <param name="icaoCodeContainer">
        ///     Class that contain ICAO code of airport to get weather information.
        /// </param>
        /// <returns>Task to download <see cref="MetarAndTafString" /> weather information or to obtain it from the cache.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="icaoCodeContainer" /> is <c>null</c>.</exception>
        public async Task<MetarAndTafString> DownloadMetarAndTafStringAsync(IIcaoCodeContainer icaoCodeContainer)
        {
            return (await DownloadSeveralMetarAndTafStringsAsync(new[] { icaoCodeContainer })).First();
        }

        /// <summary>
        ///     Asynchronously downloads weather information of category <see cref="MetarAndTafString" /> for specified ICAO codes.
        ///     Before downloading it checks whether result can be obtained from the cache.
        /// </summary>
        /// <param name="icaoCodeContainers">
        ///     Collection of classes that contain ICAO codes of airports to get weather information.
        /// </param>
        /// <returns>
        ///     Task to download the list of <see cref="List{T} " /> <see cref="MetarAndTafString " /> weather
        ///     information elements according to specified ICAO codes of airports or to obtain them (or a part of them) from the
        ///     cache.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCodeContainers" /> is <c>null</c> or
        ///     contains <c>null</c>.
        /// </exception>
        public async Task<List<MetarAndTafString>> DownloadSeveralMetarAndTafStringsAsync(
            IReadOnlyList<IIcaoCodeContainer> icaoCodeContainers)
        {
            List<MetarAndTafString> result = new List<MetarAndTafString>();
            List<IIcaoCodeContainer> toDownload = new List<IIcaoCodeContainer>();
            foreach (IIcaoCodeContainer icaoCodeContainer in icaoCodeContainers)
            {
                MetarAndTafString metarAndTafString = _metarAndTafStringCache.Get(icaoCodeContainer.AirportIcaoCode);
                if (metarAndTafString != null)
                {
                    NumberOfRequestsToGetMetarAndTafFromCache++;
                    result.Add(metarAndTafString);
                }
                else
                {
                    toDownload.Add(icaoCodeContainer);
                }
            }

            List<MetarAndTafString> downloaded =
                await _metarAndTafStringProvider.DownloadSeveralMetarAndTafStringsAsync(toDownload);
            result.AddRange(downloaded);
            foreach (MetarAndTafString metarAndTafString in downloaded)
            {
                _metarAndTafStringCache.Add(metarAndTafString.MetarString.AirportIcaoCode, metarAndTafString);
            }

            NumberOfRequestsToDownloadMetar = _metarAndTafStringProvider.NumberOfRequestsToDownloadMetar;
            NumberOfRequestsToDownloadTaf = _metarAndTafStringProvider.NumberOfRequestsToDownloadTaf;
            return result.SortedByIcaoCodeInAlphabeticalOrder().ToList();
        }

        #endregion
    }
}