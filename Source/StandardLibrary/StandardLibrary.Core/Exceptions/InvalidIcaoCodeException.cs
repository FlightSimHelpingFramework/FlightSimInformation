// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System;
using JetBrains.Annotations;

#endregion

namespace StandardLibrary.Exceptions
{
    /// <summary>
    ///     Exception class for invalid ICAO code.
    /// </summary>
    [Serializable]
    public class InvalidIcaoCodeException : Exception
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        public InvalidIcaoCodeException()
        {
        }

        /// <summary>
        ///     Constructor with a text message parameter <paramref name="message" /> for more precise exception description.
        /// </summary>
        /// <param name="message">Exception message for more precise exception information.</param>
        public InvalidIcaoCodeException([NotNull] string message) : base(message)
        {
        }

        /// <summary>
        ///     Constructor with a text message parameter <paramref name="message" /> for more precise exception description and
        ///     inner exception  and <paramref name="inner" />.
        /// </summary>
        /// <param name="message">Exception message for more precise exception information.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidIcaoCodeException([NotNull] string message, [NotNull] Exception inner) : base(message, inner)
        {
        }
    }
}