// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;

#endregion

namespace WeatherInformation.Core.Statistics
{
    /// <summary>
    ///     Class that supports calculation of number of requests for
    ///     getting specific kind of information (of type <typeparamref name="T" />).
    /// </summary>
    public class InformationStatistics<T>
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public InformationStatistics()
        {
            StatisticsForType = typeof(T);
        }

        /// <summary>
        ///     Number of requests for getting specific kind of information (of type <typeparamref name="T" />).
        /// </summary>
        /// <remarks>
        ///     Only valid request cases are considered, i.e. cases with valid-formatted ICAO codes.
        ///     Notice, that not all valid-formatted ICAO codes represent real airports. ICAO code might be valid-formatted,
        ///     but airport with such code may not exists. However, request with such ICAO codes will be counted.
        /// </remarks>
        public int NumberOfRequestsToDownloadInformation { get; set; }

        /// <summary>
        ///     Type of information for which this statistic is calculated.
        /// </summary>
        public Type StatisticsForType { get; }
    }
}