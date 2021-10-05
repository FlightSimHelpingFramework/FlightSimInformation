// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Codes;
using StandardLibrary.DataCache.Types;
using StandardLibrary.DeepCopying;
using StandardLibrary.Sorting;
using WeatherInformation.Core.ServiceTypes;
using WeatherInformation.Core.Statistics;

#endregion

namespace WeatherInformation.Core.ForCaching.Decorators
{
    /// <summary>
    ///     Decorates <see cref="IInformationProviderOfType{T}" /> in order to support caching.
    /// </summary>
    /// <typeparam name="T">Type of information that should be loaded and later cached.</typeparam>
    public class InformationCacheSupport<T> : IInformationProviderOfType<T>
        where T : class, IDeepCopyable<T>, IIcaoCodeContainer
    {
        /// <summary>
        ///     Cache for storing data.
        /// </summary>
        private readonly IDataCache<IcaoCode, T> _cache;

        /// <summary>
        ///     Provider that is being decorated.
        /// </summary>
        private readonly IInformationProviderOfType<T> _informationProvider;

        /// <summary>
        ///     Statistics for the number of request to download information.
        /// </summary>
        private readonly InformationStatistics<T> _numberOfRequestsToDownloadInformation =
            new InformationStatistics<T>();

        /// <summary>
        ///     Decorator for adding caching support.
        /// </summary>
        /// <param name="informationProvider">Information provider to decorate.</param>
        /// <param name="dataCache">Data cache to use.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="informationProvider" /> and/or <paramref name="dataCache" />
        ///     are <c>null</c>.
        /// </exception>
        public InformationCacheSupport([NotNull] IInformationProviderOfType<T> informationProvider,
            [NotNull] IDataCache<IcaoCode, T> dataCache)
        {
            NullChecking.ThrowExceptionIfNull(informationProvider, nameof(informationProvider));
            NullChecking.ThrowExceptionIfNull(dataCache, nameof(dataCache));

            _informationProvider = informationProvider;
            _cache = dataCache;
        }

        /// <summary>
        ///     Number of requests that were obtained from the cache.
        /// </summary>
        [UsedImplicitly]
        public int NumberOfRequestsFromCache { get; private set; }

        #region Implementation of IInformationProviderOfType<T>

        /// <summary>
        ///     Asynchronously downloads information of type <typeparamref name="T" /> for specified ICAO code and caches the
        ///     result.
        ///     Before downloading it checks whether result can be obtained from cache.
        /// </summary>
        /// <param name="icaoCodeContainer">
        ///     Class that contain ICAO code of airport to get information.
        /// </param>
        /// <returns>Task to download information of type <typeparamref name="T" /> or to obtain it from the cache.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="icaoCodeContainer" /> is <c>null</c>.</exception>
        public async Task<T> DownloadInformationAsync(IIcaoCodeContainer icaoCodeContainer)
        {
            return (await DownloadSeveralPiecesOfInformationAsync(new[] { icaoCodeContainer })).First();
        }

        /// <summary>
        ///     Asynchronously downloads information of type <typeparamref name="T" /> for specified ICAO codes.
        ///     Before downloading it checks whether result can be obtained from cache.
        /// </summary>
        /// <param name="icaoCodeContainers">
        ///     Collection of classes that contain ICAO codes of airports to get information.
        /// </param>
        /// <returns>
        ///     Task to download the list of <see cref="List{T} " /> <typeparamref name="T" /> information elements
        ///     according to the specified ICAO codes of airports or to obtain them (or a part of them) from the cache.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCodeContainers" /> is <c>null</c> or
        ///     contains <c>null</c>.
        /// </exception>
        public async Task<List<T>> DownloadSeveralPiecesOfInformationAsync(
            IReadOnlyList<IIcaoCodeContainer> icaoCodeContainers)
        {
            List<T> result = new List<T>();
            List<IIcaoCodeContainer> toDownload = new List<IIcaoCodeContainer>();
            foreach (IIcaoCodeContainer icaoCodeContainer in icaoCodeContainers)
            {
                T data = _cache.Get(icaoCodeContainer.AirportIcaoCode);
                if (data != null)
                {
                    NumberOfRequestsFromCache++;
                    result.Add(data);
                }
                else
                {
                    toDownload.Add(icaoCodeContainer);
                }
            }

            List<T> downloaded = await _informationProvider.DownloadSeveralPiecesOfInformationAsync(toDownload);
            result.AddRange(downloaded);
            foreach (T data in downloaded)
            {
                _cache.Add(data.AirportIcaoCode, data);
            }

            _numberOfRequestsToDownloadInformation.NumberOfRequestsToDownloadInformation = _informationProvider
                .GetInformationProviderStatistics().NumberOfRequestsToDownloadInformation;
            return result.SortedByIcaoCodeInAlphabeticalOrder().ToList();
        }

        /// <inheritdoc />
        public InformationStatistics<T> GetInformationProviderStatistics()
        {
            return _numberOfRequestsToDownloadInformation;
        }

        #endregion
    }
}