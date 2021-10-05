// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Downloader.General;
using StandardLibrary.Downloader.Specific;
using WeatherInformation.Core.Source;

namespace WeatherInformation.Core.WeatherProviders.Base
{
    /// <summary>
    ///     Base class for all weather sources that do not require additional authentication and/or authorization.
    /// </summary>
    public class BaseOpenWeatherProvider
    {
        /// <summary>
        ///     Constructor of class providing access to weather information without additional authentication.
        /// </summary>
        /// <param name="downloader">Weather information downloader.</param>
        /// <param name="source">Weather source.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="downloader" /> or <paramref name="source" /> are <c>null</c>.
        /// </exception>
        public BaseOpenWeatherProvider([NotNull] WeatherSource source,
            [NotNull] IDownloader<DownloadResultWithIcaoCode<string>, UrlDownloadRequestWithIcaoCode> downloader)
        {
            NullChecking.ThrowExceptionIfNull(source, nameof(source));
            NullChecking.ThrowExceptionIfNull(downloader, nameof(downloader));
            Source = source;
            Downloader = downloader;
        }

        /// <summary>
        ///     Weather information downloader interface.
        /// </summary>
        [NotNull]
        public IDownloader<DownloadResultWithIcaoCode<string>, UrlDownloadRequestWithIcaoCode> Downloader { get; }

        /// <summary>
        ///     Weather information source.
        /// </summary>
        [NotNull]
        public WeatherSource Source { get; }
    }
}