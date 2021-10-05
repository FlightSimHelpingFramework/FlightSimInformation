// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using StandardLibrary.Codes;
using StandardLibrary.DeepCopying;
using WeatherInformation.Core.Statistics;

namespace WeatherInformation.Core.ServiceTypes
{
    /// <summary>
    ///     Interface of information provider that allows asynchronous downloading of <typeparamref name="T" />
    ///     information type.
    /// </summary>
    [UsedImplicitly]
    public interface IInformationProviderOfType<T> where T : class, IDeepCopyable<T>
    {
        /// <summary>
        ///     Asynchronously downloads information of type <typeparamref name="T" /> for specified ICAO code.
        /// </summary>
        /// <param name="icaoCodeContainer">
        ///     Class that contain ICAO code of airport to download information.
        /// </param>
        /// <returns>Task to download information of type <typeparamref name="T" />.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="icaoCodeContainer" /> is <c>null</c>.</exception>
        [NotNull]
        [UsedImplicitly]
        Task<T> DownloadInformationAsync([NotNull] IIcaoCodeContainer icaoCodeContainer);

        /// <summary>
        ///     Asynchronously downloads information of type <typeparamref name="T" /> for specified ICAO codes.
        /// </summary>
        /// <param name="icaoCodeContainers">
        ///     Collection of classes that contain ICAO codes of airports to download information.
        /// </param>
        /// <returns>
        ///     Task to download the list of <see cref="List{T} " /> <typeparamref name="T" /> information elements
        ///     according to the specified ICAO codes of airports.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCodeContainers" /> is <c>null</c> or
        ///     contains <c>null</c>.
        /// </exception>
        [NotNull]
        [UsedImplicitly]
        Task<List<T>> DownloadSeveralPiecesOfInformationAsync(
            [NotNull] [ItemNotNull] IReadOnlyList<IIcaoCodeContainer> icaoCodeContainers);

        /// <summary>
        ///     Statistic for current information provider.
        /// </summary>
        /// <returns>Statistic for current information provider.</returns>
        [UsedImplicitly]
        InformationStatistics<T> GetInformationProviderStatistics();
    }
}