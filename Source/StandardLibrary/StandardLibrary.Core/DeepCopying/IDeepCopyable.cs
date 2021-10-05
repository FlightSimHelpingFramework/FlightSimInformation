// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using JetBrains.Annotations;

#endregion

namespace StandardLibrary.DeepCopying
{
    /// <summary>
    ///     Interface of an object, that support deep copying.
    /// </summary>
    /// <typeparam name="T">Type of copyable object.</typeparam>
    public interface IDeepCopyable<out T>
    {
        /// <summary>
        ///     Creates deep copy of an object.
        /// </summary>
        /// <returns>Deep copy.</returns>
        [NotNull]
        [UsedImplicitly]
        T DeepCopy();
    }
}