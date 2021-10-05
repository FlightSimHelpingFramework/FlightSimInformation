// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using StandardLibrary.ArgumentChecking;

#endregion

namespace StandardLibrary.Json
{
    /// <summary>
    ///     Static class for deserialization JSON-strings to object.
    /// </summary>
    public static class Deserialization
    {
        /// <summary>
        ///     Deserialize <paramref name="jsonString" /> to <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Object type - result of deserialization <paramref name="jsonString" />.</typeparam>
        /// <param name="jsonString">JSON string for deserialization.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="jsonString" /> is <c>null</c>.</exception>
        /// <returns>Object of type <typeparamref name="T" /> - deserialization result of <paramref name="jsonString" />.</returns>
        [CanBeNull]
        public static T Deserialize<T>([NotNull] string jsonString)
        {
            NullChecking.ThrowExceptionIfNull(jsonString, nameof(jsonString));
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}