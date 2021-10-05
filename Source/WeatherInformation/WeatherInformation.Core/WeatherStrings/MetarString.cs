// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.Codes;
using StandardLibrary.DeepCopying;
using WeatherInformation.Core.Source;
using WeatherInformation.Core.WeatherStrings.Base;

#endregion

namespace WeatherInformation.Core.WeatherStrings
{
    /// <summary>
    ///     Class representing METAR weather string.
    /// </summary>
    public sealed class MetarString : WeatherString, IDeepCopyable<MetarString>
    {
        /// <summary>
        ///     METAR weather string constructor.
        /// </summary>
        /// <param name="icaoCode">Airport ICAO code to which this weather string belongs.</param>
        /// <param name="rawString">Raw weather string.</param>
        /// <param name="found">Was information string found.</param>
        /// <param name="source">Weather information string source of type <see cref="WeatherSource" />.</param>
        /// <exception cref="ArgumentException">
        ///     If <paramref name="rawString" /> is not a valid string but <paramref name="found" /> is <c>true</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCode" /> or <paramref name="source" /> are <c>null</c>.
        /// </exception>
        public MetarString([NotNull] IcaoCode icaoCode, string rawString, bool found, WeatherSource source) :
            base(icaoCode, Normalizer.Normalizer.GetNormalizedMetarStringFrom(found, rawString, icaoCode), found,
                source)
        {
        }

        #region Implementation of IDeepCopiable<MetarString>

        /// <inheritdoc />
        public new MetarString DeepCopy()
        {
            return new MetarString(new IcaoCode(AirportIcaoCode.Code), String, Found, Source);
        }

        #endregion
    }
}