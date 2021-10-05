// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Codes;
using StandardLibrary.DeepCopying;
using StandardLibrary.Hashing;
using WeatherInformation.Core.Source;

#endregion

namespace WeatherInformation.Core.WeatherStrings.Base
{
    /// <summary>
    ///     Base class for weather information strings (for example <see cref="MetarString" />,  <see cref="TafString" />).
    /// </summary>
    public class WeatherString : IIcaoCodeContainer, IEquatable<WeatherString>, IDeepCopyable<WeatherString>
    {
        /// <summary>
        ///     Weather string constructor.
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
        protected WeatherString([NotNull] IcaoCode icaoCode, string rawString, bool found,
            [NotNull] WeatherSource source)
        {
            NullChecking.ThrowExceptionIfNull(icaoCode, nameof(icaoCode));
            NullChecking.ThrowExceptionIfNull(source, nameof(source));
            if (found)
            {
                StringArgumentChecking.ThrowExceptionIfStringIsInvalid(rawString, nameof(rawString));
                String = StringArgumentChecking.IsStringValid(rawString)
                    ? rawString
                    : $"Not found for airport with ICAO code {icaoCode.Code}";
            }
            else
            {
                String = $"Not found for airport with ICAO code {icaoCode.Code}";
            }

            AirportIcaoCode = icaoCode;
            Found = found;
            Source = source;
        }

        /// <summary>
        ///     Was information found.
        /// </summary>
        public bool Found { get; }

        /// <summary>
        ///     Weather information string source of type <see cref="WeatherSource" />.
        /// </summary>
        public WeatherSource Source { get; }

        /// <summary>
        ///     Weather string in normalized (standard) representation.
        /// </summary>
        [NotNull]
        public string String { get; }

        #region Implementation of IDeepCopiable<WeatherString>

        /// <inheritdoc />
        public WeatherString DeepCopy()
        {
            return new WeatherString(new IcaoCode(AirportIcaoCode.Code), String, Found, Source);
        }

        #endregion

        #region Implementation of IEquatable<WeatherString>

        /// <inheritdoc />
        public bool Equals(WeatherString other)
        {
            return other != null
                   && other.Found == Found
                   && other.Source.Equals(Source)
                   && other.AirportIcaoCode.Equals(AirportIcaoCode)
                   && other.String == String;
        }

        #endregion

        /// <summary>
        ///     Airport ICAO code to which this weather string belongs.
        /// </summary>
        [NotNull]
        public IcaoCode AirportIcaoCode { get; }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Source.Name} => {String}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as WeatherString);
        }

        /// <summary>
        ///     Custom hash function.
        /// </summary>
        /// <returns>Hash code of current object.</returns>
        public override int GetHashCode()
        {
            return CustomHash
                .GetInitialHashNumber()
                .AddToHashNumber(String.GetHashCode())
                .AddToHashNumber(Found.GetHashCode())
                .AddToHashNumber(Source.GetHashCode())
                .AddToHashNumber(AirportIcaoCode.GetHashCode())
                .AddToHashNumber(nameof(WeatherString).GetHashCode());
        }

        #endregion
    }
}