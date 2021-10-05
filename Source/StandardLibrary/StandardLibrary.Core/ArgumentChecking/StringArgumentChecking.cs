// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

#endregion

namespace StandardLibrary.ArgumentChecking
{
    /// <summary>
    ///     Static class for checking string arguments for correctness.
    /// </summary>
    public static class StringArgumentChecking
    {
        /// <summary>
        ///     Checks whether <paramref name="argument" /> is a valid string (not <c>null</c>, not empty and not only whitespaces)
        /// </summary>
        /// <param name="argument">String for checking.</param>
        /// <returns>Whether <paramref name="argument" /> is a valid string.</returns>
        public static bool IsStringValid([CanBeNull] string argument)
        {
            return !string.IsNullOrWhiteSpace(argument);
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" />, if at least one element of <paramref name="collection" /> is not a valid
        ///     string.
        /// </summary>
        /// <param name="collection">Collection of strings for performing string checking.</param>
        /// <param name="collectionName">Collection name (for more informative exception throw).</param>
        /// <exception cref="ArgumentException">
        ///     If at least one element of <paramref name="collection" /> is not a valid string.
        /// </exception>
        private static void ThrowExceptionIfContainsInvalidString([NotNull] IEnumerable<string> collection,
            [NotNull] string collectionName)
        {
            if (collection.Any(item => !IsStringValid(item)))
            {
                throw new ArgumentException("At least one element is null, empty or whitespace string",
                    collectionName);
            }
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> if at least one element of <paramref name="collection" /> is not a valid
        ///     string.
        ///     Throws <see cref="ArgumentNullException" /> if <paramref name="collection" /> is <c>null</c>.
        /// </summary>
        /// <param name="collection">Collection of strings for performing string checking.</param>
        /// <param name="collectionName">Collection name (for more informative exception throw).</param>
        /// <exception cref="ArgumentException">
        ///     If at least one element of <paramref name="collection" /> is not a valid string.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="collection" /> is <c>null</c>.
        /// </exception>
        public static void ThrowExceptionIfNullOrContainsInvalidString([NotNull] IEnumerable<string> collection,
            [NotNull] string collectionName)
        {
            NullChecking.ThrowExceptionIfNull(collection, collectionName);
            ThrowExceptionIfContainsInvalidString(collection, collectionName);
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> if <paramref name="argument" /> is not a valid string.
        /// </summary>
        /// <param name="argument">String for checking.</param>
        /// <param name="argumentName">String argument name (for more informative exception throw).</param>
        /// <exception cref="ArgumentException">
        ///     If <paramref name="argument" /> is not a valid string.
        /// </exception>
        public static void ThrowExceptionIfStringIsInvalid(string argument, [NotNull] string argumentName)
        {
            if (!IsStringValid(argument))
            {
                throw new ArgumentException("Argument is null or empty string or whitespace string",
                    // ReSharper disable once ConstantNullCoalescingCondition, for null-security.
                    argumentName ?? "Null argumentName");
            }
        }
    }
}