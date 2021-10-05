// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using StandardLibrary.Codes;
using WeatherInformation.Core.Statistics;
using WeatherInformation.Core.WeatherStrings;

#endregion

namespace WeatherInformation.Core.ServiceTypes
{
    /// <summary>
    ///     Weather provider that supports asynchronous downloading of <see cref="MetarString" /> weather
    ///     information.
    /// </summary>
    [UsedImplicitly]
    public interface IMetarStringProvider : IMetarStatistics
    {
        /// <summary>
        ///     Asynchronously downloads weather information of category <see cref="MetarString" /> for specified ICAO code.
        /// </summary>
        /// <param name="icaoCodeContainer">
        ///     Class that contain ICAO code of airport to download weather information.
        /// </param>
        /// <returns>Task to download <see cref="MetarString" /> weather information.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="icaoCodeContainer" /> is <c>null</c>.</exception>
        [NotNull]
        [UsedImplicitly]
        Task<MetarString> DownloadMetarStringAsync([NotNull] IIcaoCodeContainer icaoCodeContainer);

        /// <summary>
        ///     Asynchronously downloads weather information of category <see cref="MetarString" /> for specified ICAO codes.
        /// </summary>
        /// <param name="icaoCodeContainers">
        ///     Collection of classes that contain ICAO codes of airports to download weather information.
        /// </param>
        /// <returns>
        ///     Task to download the list of <see cref="List{T} " />  <see cref="MetarString " /> weather information
        ///     elements according to the specified ICAO codes of airports.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCodeContainers" /> is <c>null</c> or
        ///     contains <c>null</c>.
        /// </exception>
        [NotNull]
        [UsedImplicitly]
        Task<List<MetarString>> DownloadSeveralMetarStringsAsync(
            [NotNull] [ItemNotNull] IReadOnlyList<IIcaoCodeContainer> icaoCodeContainers);
    }
}