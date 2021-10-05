// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using StandardLibrary.DataCache.Types;
using StandardLibrary.DeepCopying;

#endregion

namespace StandardLibrary.DataCache.Factories
{
    /// <summary>
    ///     Interface of a cache factory.
    /// </summary>
    public interface IDataCacheFactory
    {
        /// <summary>
        ///     Creates a cache for storing data entries associated with keys. For cache refer to <see cref="DataCache" />.
        /// </summary>
        /// <typeparam name="TKey">Type of key to associate data with.</typeparam>
        /// <typeparam name="TData">Type of data to be associated with key.</typeparam>
        /// <returns>Cache object with required properties.</returns>
        IDataCache<TKey, TData> CreateCache<TKey, TData>() where TData : class, IDeepCopyable<TData>;
    }
}