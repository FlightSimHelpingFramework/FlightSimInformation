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
    ///     Decorates <see cref="IMetarStringProvider" /> in order to support caching.
    /// </summary>
    public class MetarStringCacheSupport : IMetarStringProvider
    {
        /// <summary>
        ///     Cache for storing data.
        /// </summary>
        private readonly IDataCache<IcaoCode, MetarString> _metarStringCache;

        /// <summary>
        ///     Provider that is being decorated.
        /// </summary>
        private readonly IMetarStringProvider _metarStringProvider;

        /// <summary>
        ///     Decorator for adding caching support.
        /// </summary>
        /// <param name="metarStringProvider">METAR string provider to decorate.</param>
        /// <param name="dataCache">Data cache to use.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="metarStringProvider" /> and/or <paramref name="dataCache" />
        ///     are <c>null</c>.
        /// </exception>
        public MetarStringCacheSupport([NotNull] IMetarStringProvider metarStringProvider,
            [NotNull] IDataCache<IcaoCode, MetarString> dataCache)
        {
            NullChecking.ThrowExceptionIfNull(metarStringProvider, nameof(metarStringProvider));
            NullChecking.ThrowExceptionIfNull(dataCache, nameof(dataCache));
            _metarStringProvider = metarStringProvider;
            _metarStringCache = dataCache;
        }

        /// <summary>
        ///     Number of requests that were obtained from the cache.
        /// </summary>
        [UsedImplicitly]
        public int NumberOfRequestsToGetMetarFromCache { get; private set; }

        #region Implementation of IMetarStatistics

        /// <inheritdoc />
        public int NumberOfRequestsToDownloadMetar { get; private set; }

        #endregion

        #region Implementation of IMetarStringProvider

        /// <summary>
        ///     Asynchronously downloads weather information of category <see cref="MetarString" /> for specified ICAO code.
        ///     Before downloading it checks whether result can be obtained from the cache.
        /// </summary>
        /// <param name="icaoCodeContainer">
        ///     Class that contain ICAO code of airport to get weather information.
        /// </param>
        /// <returns>Task to download <see cref="MetarString" /> weather information or to obtain it from the cache.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="icaoCodeContainer" /> is <c>null</c>.</exception>
        public async Task<MetarString> DownloadMetarStringAsync(IIcaoCodeContainer icaoCodeContainer)
        {
            return (await DownloadSeveralMetarStringsAsync(new[] { icaoCodeContainer })).First();
        }

        /// <summary>
        ///     Asynchronously downloads weather information of category <see cref="MetarString" /> for specified ICAO codes.
        /// </summary>
        /// <param name="icaoCodeContainers">
        ///     Collection of classes that contain ICAO codes of airports to get weather information.
        /// </param>
        /// <returns>
        ///     Task to download the list of <see cref="List{T} " />  <see cref="MetarString " /> weather information
        ///     elements according to the specified ICAO codes of airports or to obtain them (or a part of them) from the cache.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCodeContainers" /> is <c>null</c> or
        ///     contains <c>null</c>.
        /// </exception>
        public async Task<List<MetarString>> DownloadSeveralMetarStringsAsync(
            IReadOnlyList<IIcaoCodeContainer> icaoCodeContainers)
        {
            List<MetarString> result = new List<MetarString>();
            List<IIcaoCodeContainer> toDownload = new List<IIcaoCodeContainer>();
            foreach (IIcaoCodeContainer icaoCodeContainer in icaoCodeContainers)
            {
                MetarString metar = _metarStringCache.Get(icaoCodeContainer.AirportIcaoCode);
                if (metar != null)
                {
                    NumberOfRequestsToGetMetarFromCache++;
                    result.Add(metar);
                }
                else
                {
                    toDownload.Add(icaoCodeContainer);
                }
            }

            List<MetarString> downloaded = await _metarStringProvider.DownloadSeveralMetarStringsAsync(toDownload);
            result.AddRange(downloaded);
            foreach (MetarString metarString in downloaded)
            {
                _metarStringCache.Add(metarString.AirportIcaoCode, metarString);
            }

            NumberOfRequestsToDownloadMetar = _metarStringProvider.NumberOfRequestsToDownloadMetar;
            return result.SortedByIcaoCodeInAlphabeticalOrder().ToList();
        }

        #endregion
    }
}