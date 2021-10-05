// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System;
using JetBrains.Annotations;
using StandardLibrary.Codes;

namespace WeatherInformation.Core.WeatherProviders.WeatherConfigurations.DownloaderData
{
    /// <summary>
    ///     Provides the data required to download METAR meteorological report to downloader.
    /// </summary>
    /// <typeparam name="T">Data type for downloader to work with. </typeparam>
    public interface IGetDownloaderDataToDownloadMetar<out T>
    {
        /// <summary>
        ///     Provides the data required to download METAR meteorological report of the specified ICAO code
        ///     (<paramref name="icaoCodeContainer" />) to downloader.
        /// </summary>
        /// <param name="icaoCodeContainer">
        ///     Class containing the ICAO code of the airport for which to download weather information report.
        /// </param>
        /// <returns>Data type <typeparamref name="T" /> that is used by downloader to work with.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="icaoCodeContainer" /> is <c>null</c>.</exception>
        [NotNull]
        [UsedImplicitly]
        T GetDownloaderDataToDownloadMetar([NotNull] IIcaoCodeContainer icaoCodeContainer);
    }
}