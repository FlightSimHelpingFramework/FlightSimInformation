// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

namespace WeatherInformation.Core.Statistics
{
    /// <summary>
    ///     Interface of a class (weather information source mainly) that supports calculation of number of requests for
    ///     getting TAF weather information.
    /// </summary>
    public interface ITafStatistics
    {
        /// <summary>
        ///     number of requests for getting TAF weather information.
        /// </summary>
        /// <remarks>
        ///     Only valid request cases are considered, i.e. cases with valid-formatted ICAO codes.
        ///     Notice, that not all valid-formatted ICAO codes represent real airports. ICAO code might be valid-formatted,
        ///     but airport with such code may not exists. However, request with such ICAO codes will be counted.
        /// </remarks>
        int NumberOfRequestsToDownloadTaf { get; }
    }
}