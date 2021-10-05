// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.DeepCopying;

#endregion

namespace StandardLibrary.DataCache.Types
{
    /// <summary>
    ///     Interface of a cache object for working with cached data. Works with reference (class) objects that support
    ///     <see cref="IDeepCopyable{T}" />.
    /// </summary>
    /// <typeparam name="TKey">Type of key. Key is used to get an associated cache item.</typeparam>
    /// <typeparam name="TData">Type of data.Represents the actual type of data that is stored in a cache.</typeparam>
    public interface IDataCache<in TKey, TData> where TData : class, IDeepCopyable<TData>
    {
        /// <summary>
        ///     Adds deep copy of
        ///     <paramref name="data" /> that is associated with <paramref name="key" /> to cache.
        /// </summary>
        /// <param name="key">Key to associate with <paramref name="data" />.</param>
        /// <param name="data">Data item, associated with <paramref name="key" />.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="key" /> and/or <paramref name="data" /> are <c>null</c>.</exception>
        void Add([NotNull] TKey key, [NotNull] TData data);

        /// <summary>
        ///     Get data item associated with <paramref name="key" /> from cache.
        /// </summary>
        /// <param name="key">Key associated with data item.</param>
        /// <returns>
        ///     Deep copy of a data item associated with <paramref name="key" /> from cache or <c>null</c> if
        ///     cache has no associated with <paramref name="key" /> item.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="key" /> is <c>null</c>.</exception>
        [CanBeNull]
        TData Get([NotNull] TKey key);
    }
}