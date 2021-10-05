// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System;
using System.Collections.Generic;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.DeepCopying;

namespace StandardLibrary.DataCache.Types
{
    /// <summary>
    ///     Cache that stores data for fixed time. After that time outdated items are removed.
    /// </summary>
    /// <typeparam name="TKey">Type of key to associate with data items.</typeparam>
    /// <typeparam name="TData">Type of data to be associated with key.</typeparam>
    public class DataCacheWithTimeRestriction<TKey, TData> : IDataCache<TKey, TData>
        where TData : class, IDeepCopyable<TData>
    {
        /// <summary>
        ///     Number of second to store data in cache entry.
        /// </summary>
        private readonly int _secondsLimit;

        /// <summary>
        ///     Dictionary for storing key and associated pair of data and timestamp, when it was formed.
        /// </summary>
        private readonly Dictionary<TKey, (DateTime, TData)> _storage = new Dictionary<TKey, (DateTime, TData)>();

        /// <summary>
        ///     Construct data cache with time restriction.
        /// </summary>
        /// <param name="secondsLimit">
        ///     Number of seconds for cache entry to be valid (time before data in cache entry becomes
        ///     outdated).
        /// </param>
        public DataCacheWithTimeRestriction(int secondsLimit = 60 * 3)
        {
            _secondsLimit = secondsLimit;
        }

        /// <summary>
        ///     Removes all outdated elements from cache.
        /// </summary>
        private void RemoveOldElements()
        {
            DateTime current = DateTime.Now;
            foreach ((TKey key, (DateTime, TData) value) in _storage)
            {
                if ((current - value.Item1).Seconds > _secondsLimit)
                {
                    _storage.Remove(key);
                }
            }
        }

        #region Implementation of IDataCache<in TKey,TData>

        /// <inheritdoc />
        public TData Get(TKey key)
        {
            NullChecking.ThrowExceptionIfNull(key, nameof(key));
            RemoveOldElements();
            return _storage.ContainsKey(key) ? _storage[key].Item2.DeepCopy() : null;
        }

        /// <inheritdoc />
        public void Add(TKey key, TData data)
        {
            NullChecking.ThrowExceptionIfNull(key, nameof(key));
            NullChecking.ThrowExceptionIfNull(data, nameof(data));
            RemoveOldElements();
            _storage.Add(key, (DateTime.Now, data.DeepCopy()));
        }

        #endregion
    }
}