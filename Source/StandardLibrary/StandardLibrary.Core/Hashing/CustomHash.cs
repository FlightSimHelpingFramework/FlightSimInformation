// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

namespace StandardLibrary.Hashing
{
    /// <summary>
    ///     Static class for custom hashing and fluent interface for it.
    /// </summary>
    public static class CustomHash
    {
        /// <summary>
        ///     "Adds" <paramref name="value" /> to <paramref name="hash" /> according to current hashing rules.
        /// </summary>
        /// <param name="hash">Hash value.</param>
        /// <param name="value">Value to be added to <paramref name="hash" />.</param>
        /// <returns>New hash value with added <paramref name="value" />.</returns>
        public static int AddToHashNumber(this int hash, int value)
        {
            unchecked
            {
                return (hash * 16777619) ^ value;
            }
        }

        /// <summary>
        ///     Gives initial hash value. This should be first chain of hashing fluent interface.
        /// </summary>
        /// <returns>Initial hash value (number).</returns>
        public static int GetInitialHashNumber()
        {
            unchecked
            {
                return (int)2166136261;
            }
        }
    }
}