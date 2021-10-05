// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Exceptions;
using StandardLibrary.Hashing;

#endregion

namespace StandardLibrary.Codes
{
    /// <summary>
    ///     Airport ICAO code class. Allows to work safely with airport ICAO codes.
    /// </summary>
    public class IcaoCode : IEquatable<IcaoCode>
    {
        /// <summary>
        ///     Constructor from <c>string</c>.
        /// </summary>
        /// <param name="icaoCode">String airport ICAO code.</param>
        /// <exception cref="InvalidIcaoCodeException">
        ///     If <paramref name="icaoCode" /> is invalid string ICAO code.
        /// </exception>
        public IcaoCode([NotNull] string icaoCode)
        {
            IcaoCodeChecking.ThrowExceptionIfIcaoCodeIsInvalid(icaoCode);
            Code = icaoCode;
        }

        /// <summary>
        ///     Airport ICAO code/.
        /// </summary>
        public string Code { get; }

        #region Implementation of IEquatable<IcaoCode>

        /// <summary>
        ///     Checks whether <paramref name="other" /> is equal to current object.
        /// </summary>
        /// <param name="other">
        ///     Object for checking for being equal to current object.
        /// </param>
        /// <returns>
        ///     <see langword="true" />, if <paramref name="other" /> equal to current object.
        ///     <see langword="false" /> otherwise.
        /// </returns>
        public bool Equals(IcaoCode other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Code == Code;
        }

        #endregion

        #region Overrides of Object

        /// <summary>Object string representation.</summary>
        /// <returns>String representation of airport ICAO code.</returns>
        public override string ToString()
        {
            return Code;
        }

        /// <summary>
        ///     Checks whether <paramref name="obj" /> is equal to current object.
        /// </summary>
        /// <param name="obj">
        ///     Object for checking for being equal to current object.
        /// </param>
        /// <returns>
        ///     <see langword="true" />, if <paramref name="obj" /> equal to current object.
        ///     <see langword="false" /> otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals((IcaoCode)obj);
        }

        /// <summary>Hash value of current object.</summary>
        /// <returns>Current object hash code.</returns>
        public override int GetHashCode()
        {
            return CustomHash
                .GetInitialHashNumber()
                .AddToHashNumber(Code.GetHashCode())
                .AddToHashNumber(nameof(IcaoCode).GetHashCode());
        }

        #endregion
    }
}