// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

#endregion

namespace StandardLibrary.ArgumentChecking
{
    /// <summary>
    ///     Static class for argument <c>null</c>-checking.
    /// </summary>
    public static class NullChecking
    {
        /// <summary>
        ///     Throws <see cref="ArgumentNullException" /> if <paramref name="argument" /> is <c>null</c>.
        /// </summary>
        /// <param name="argument">Argument for <c>null</c> checking.</param>
        /// <param name="nameOfArgument">Argument name (for more informative exception throw).</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="argument" /> is <c>null</c>.
        /// </exception>
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global, to follow further checking logic.
        public static void ThrowExceptionIfNull([CanBeNull] object argument, [NotNull] string nameOfArgument)
        {
            // ReSharper disable once ConstantNullCoalescingCondition, for further "null" security.
            if (argument == null)
            {
                throw new ArgumentNullException(nameOfArgument ?? "Null parameter", "Is null!");
            }
        }

        /// <summary>
        ///     Throws <see cref="ArgumentNullException" /> if <paramref name="collection" /> is <c>null</c> or contains
        ///     <c>null</c>.
        /// </summary>
        /// <param name="collection">Collection for checking.</param>
        /// <param name="nameOfCollection">Collection name (for more informative exception throw).</param>
        /// <typeparam name="T">Type of elements in collection.</typeparam>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="collection" /> is <c>null</c> or contains <c>null</c>.
        /// </exception>
        public static void ThrowExceptionIfNullOrContainsNull<T>(
            [CanBeNull] IEnumerable<T> collection,
            [NotNull] string nameOfCollection)
        {
            ThrowExceptionIfNull(collection, nameOfCollection);
            // ReSharper disable once PossibleNullReferenceException, because null-check has already been performed.
            foreach (object item in collection)
            {
                ThrowExceptionIfNull(item, $"item in {nameOfCollection}");
            }
        }
    }
}