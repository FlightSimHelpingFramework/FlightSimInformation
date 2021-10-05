// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.Codes;

#endregion

namespace StandardLibrary.Sorting
{
    /// <summary>
    ///     Static class for sorting by ICAO codes.
    /// </summary>
    public static class SortByIcaoCode
    {
        /// <summary>
        ///     Performs sorting, based on alphabetical order of ICAO codes in <paramref name="list" />>.
        /// </summary>
        /// <param name="list">List to sort.</param>
        /// <typeparam name="T">Type of objects in <paramref name="list" />. Must implement <see cref="IIcaoCodeContainer" />.</typeparam>
        /// <returns><see cref="IOrderedEnumerable{T}" /> of sorted items.</returns>
        [NotNull]
        [ItemNotNull]
        public static IOrderedEnumerable<T> SortedByIcaoCodeInAlphabeticalOrder<T>(
            [NotNull] [ItemNotNull] this IReadOnlyList<T> list) where T : IIcaoCodeContainer
        {
            NullChecking.ThrowExceptionIfNullOrContainsNull(list, nameof(list));
            return list.OrderBy(icaoCodeContainer => icaoCodeContainer.AirportIcaoCode.Code);
        }
    }
}