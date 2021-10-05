// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Codes;
using StandardLibrary.DeepCopying;
using StandardLibrary.Hashing;

#endregion

namespace WeatherInformation.Core.WeatherStrings
{
    /// <summary>
    ///     Class representing METAR and TAF strings together.
    /// </summary>
    public sealed class MetarAndTafString : IEquatable<MetarAndTafString>, IDeepCopyable<MetarAndTafString>,
        IIcaoCodeContainer
    {
        /// <summary>
        ///     Constructor from METAR and TAF weather information.
        /// </summary>
        /// <param name="metarString">METAR weather information string.</param>
        /// <param name="tafString">TAF weather information string.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="metarString" /> or <paramref name="tafString" /> are <c>null</c>.
        /// </exception>
        public MetarAndTafString([NotNull] MetarString metarString, [NotNull] TafString tafString)
        {
            NullChecking.ThrowExceptionIfNull(metarString, nameof(metarString));
            NullChecking.ThrowExceptionIfNull(tafString, nameof(tafString));

            MetarString = metarString;
            TafString = tafString;

            AirportIcaoCode = MetarString.AirportIcaoCode;
        }

        /// <summary>
        ///     METAR weather information string.
        /// </summary>
        [NotNull]
        public MetarString MetarString { get; }

        /// <summary>
        ///     TAF weather information string.
        /// </summary>
        [NotNull]
        public TafString TafString { get; }

        /// <summary>
        ///     Explicit conversion operator from <see cref="MetarAndTafString" /> to <see cref="WeatherStrings.MetarString" />.
        /// </summary>
        /// <param name="metarAndTafString">
        ///     <see cref="MetarAndTafString" /> object for converting to
        ///     <see cref="WeatherStrings.MetarString" />
        /// </param>
        public static explicit operator MetarString(MetarAndTafString metarAndTafString)
        {
            return metarAndTafString.MetarString;
        }

        /// <summary>
        ///     Explicit conversion operator from <see cref="MetarAndTafString" /> to <see cref="WeatherStrings.TafString" />.
        /// </summary>
        /// <param name="metarAndTafString">
        ///     <see cref="MetarAndTafString" /> object for converting to
        ///     <see cref="WeatherStrings.TafString" />
        /// </param>
        public static explicit operator TafString(MetarAndTafString metarAndTafString)
        {
            return metarAndTafString.TafString;
        }

        #region Implementation of IDeepCopiable<MetarAndTafString>

        /// <inheritdoc />
        public MetarAndTafString DeepCopy()
        {
            return new MetarAndTafString(MetarString.DeepCopy(), TafString.DeepCopy());
        }

        #endregion

        #region Implementation of IEquatable<MetarAndTafString>

        /// <inheritdoc />
        public bool Equals(MetarAndTafString other)
        {
            return other != null
                   && MetarString.Equals(other.MetarString)
                   && TafString.Equals(other.TafString);
        }

        #endregion

        #region Implementation of IIcaoCodeContainer

        /// <inheritdoc />
        [NotNull]
        public IcaoCode AirportIcaoCode { get; }

        #endregion

        #region Overrides of Object

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as MetarAndTafString);
        }

        /// <summary>
        ///     Custom hash function.
        /// </summary>
        /// <returns>Hash code of current object.</returns>
        public override int GetHashCode()
        {
            return CustomHash.GetInitialHashNumber()
                .AddToHashNumber(MetarString.GetHashCode())
                .AddToHashNumber(TafString.GetHashCode())
                .AddToHashNumber(nameof(MetarAndTafString).GetHashCode());
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{MetarString}\n{TafString}";
        }

        #endregion
    }
}