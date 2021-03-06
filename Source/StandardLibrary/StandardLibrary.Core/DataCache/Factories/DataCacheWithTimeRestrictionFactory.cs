// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.DataCache.Types;
using StandardLibrary.DeepCopying;

#endregion

namespace StandardLibrary.DataCache.Factories
{
    /// <summary>
    ///     Factory, that produces <see cref="DataCacheWithTimeRestriction{TKey,TData}" />.
    /// </summary>
    public class DataCacheWithTimeRestrictionFactory : IDataCacheFactory
    {
        /// <summary>
        ///     Number of seconds for which cache entry is valid.
        /// </summary>
        private readonly int _cacheTimeLimit;

        /// <summary>
        ///     Factory constructor.
        /// </summary>
        /// <param name="cacheTimeLimit">Number of seconds for which cache entry is valid (is NOT outdated).</param>
        /// <exception cref="ArgumentException">If <paramref name="cacheTimeLimit" /> is less that 0.</exception>
        public DataCacheWithTimeRestrictionFactory([NonNegativeValue] int cacheTimeLimit)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // Because of consistent logic. Check should be performed.
            if (cacheTimeLimit < 0)
            {
                throw new ArgumentException($"{nameof(cacheTimeLimit)} should be >= 0", nameof(cacheTimeLimit));
            }

            _cacheTimeLimit = cacheTimeLimit;
        }

        #region Implementation of IDataCacheFactory

        /// <inheritdoc />
        public IDataCache<TKey, TData> CreateCache<TKey, TData>() where TData : class, IDeepCopyable<TData>
        {
            return new DataCacheWithTimeRestriction<TKey, TData>(_cacheTimeLimit);
        }

        #endregion
    }
}