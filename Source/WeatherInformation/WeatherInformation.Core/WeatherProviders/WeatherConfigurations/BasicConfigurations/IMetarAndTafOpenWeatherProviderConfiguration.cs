// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using StandardLibrary.Codes;
using StandardLibrary.Downloader.General;
using StandardLibrary.Downloader.Specific;
using WeatherInformation.Core.WeatherProviders.WeatherConfigurations.DownloaderData;

#endregion

namespace WeatherInformation.Core.WeatherProviders.WeatherConfigurations.BasicConfigurations
{
    /// <summary>
    ///     Provides a simple (without additional authentication) configuration
    ///     for downloading METAR and TAF (together) weather reports from a weather information source.
    /// </summary>
    [UsedImplicitly]
    public interface IMetarAndTafOpenWeatherProviderConfiguration : IGetDownloaderDataToDownloadMetarAndTaf<string>
    {
        /// <summary>
        ///     Raw URL-string with fields for further interpolation.
        /// </summary>
        [NotNull]
        [UsedImplicitly]
        string RawStringToDownloadMetarAndTaf { get; }

        /// <summary>
        ///     Forms <see cref="UrlDownloadRequest" /> list by specified ICAO-codes from <paramref name="icaoCodeContainers" />.
        /// </summary>
        /// <param name="icaoCodeContainers">ICAO codes collection.</param>
        /// <returns>List of requests <see cref="UrlDownloadRequestWithIcaoCode" /> with ICAO codes.</returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCodeContainers" /> is <c>null</c> or
        ///     contains <c>null</c>.
        /// </exception>
        [UsedImplicitly]
        List<UrlDownloadRequestWithIcaoCode> FormDownloadRequestsForMetarAndTaf(
            [NotNull] [ItemNotNull] IEnumerable<IIcaoCodeContainer> icaoCodeContainers);
    }
}