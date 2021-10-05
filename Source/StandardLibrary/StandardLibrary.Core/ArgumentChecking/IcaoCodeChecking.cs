// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using StandardLibrary.Exceptions;

#endregion

namespace StandardLibrary.ArgumentChecking
{
    /// <summary>
    ///     Static class for performing checks that string or group of strings are valid airport ICAO string codes.
    /// </summary>
    public static class IcaoCodeChecking
    {
        /// <summary>
        ///     Checks whether <paramref name="icaoCode" /> satisfies airport ICAO code requirements.
        /// </summary>
        /// <param name="icaoCode">String for performing airport ICAO code requirements check.</param>
        /// <returns>Is <paramref name="icaoCode" /> a valid ICAO airport code string.</returns>
        public static bool IsIcaoCodeStringValid([CanBeNull] string icaoCode)
        {
            Regex icaoRegex = new Regex(@"^[A-Z]+$");

            return icaoCode != null && StringArgumentChecking.IsStringValid(icaoCode) && icaoCode.Length == 4 &&
                   icaoRegex.IsMatch(icaoCode) && icaoCode.ToUpper(CultureInfo.InvariantCulture) == icaoCode;
        }


        /// <summary>
        ///     Throws <see cref="InvalidIcaoCodeException" />, if at least one element of <paramref name="icaoCodes" /> does not
        ///     satisfy ICAO airport code requirements.
        /// </summary>
        /// <param name="icaoCodes">Collection of strings for performing airport ICAO code requirements check.</param>
        /// <exception cref="InvalidIcaoCodeException">
        ///     If at least one element of <paramref name="icaoCodes" /> does not satisfy ICAO airport code requirements.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="icaoCodes" /> is <c>null</c>.
        /// </exception>
        public static void ThrowExceptionIfContainsInvalidIcaoCode(
            [System.Diagnostics.CodeAnalysis.NotNull]
            IEnumerable<string> icaoCodes)
        {
            NullChecking.ThrowExceptionIfNull(icaoCodes, nameof(icaoCodes));
            foreach (string code in icaoCodes)
            {
                ThrowExceptionIfIcaoCodeIsInvalid(code);
            }
        }


        /// <summary>
        ///     Throws <see cref="InvalidIcaoCodeException" /> if <paramref name="icaoCode" /> does not satisfy ICAO airport code
        ///     requirements.
        /// </summary>
        /// <param name="icaoCode">String for performing airport ICAO code requirements check.</param>
        /// <exception cref="InvalidIcaoCodeException">
        ///     If <paramref name="icaoCode" /> does not satisfy ICAO airport code requirements.
        /// </exception>
        public static void ThrowExceptionIfIcaoCodeIsInvalid([CanBeNull] string icaoCode)
        {
            if (!IsIcaoCodeStringValid(icaoCode))
            {
                throw new InvalidIcaoCodeException("ICAO code is invalid!");
            }
        }
    }
}