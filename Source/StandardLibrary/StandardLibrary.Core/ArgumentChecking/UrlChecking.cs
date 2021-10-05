// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;

#endregion

namespace StandardLibrary.ArgumentChecking
{
    /// <summary>
    ///     Static class for checking URL addresses for correctness.
    /// </summary>
    public static class UrlChecking
    {
        /// <summary>
        ///     Checks whether <paramref name="stringUrl" /> is a correct URL address (absolute).
        /// </summary>
        /// <param name="stringUrl">URL string for checking.</param>
        /// <returns>Whether <paramref name="stringUrl" /> is a correct URL address (absolute).</returns>
        public static bool IsUrlValid(string stringUrl)
        {
            return Uri.IsWellFormedUriString(stringUrl, UriKind.Absolute);
        }
    }
}