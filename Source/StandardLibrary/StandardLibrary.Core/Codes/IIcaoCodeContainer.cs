// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using JetBrains.Annotations;

#endregion

namespace StandardLibrary.Codes
{
    /// <summary>
    ///     Interface of a class, that contains <see cref="IcaoCode" /> for representing airport
    ///     ICAO code.
    /// </summary>
    public interface IIcaoCodeContainer
    {
        /// <summary>
        ///     Airport ICAO code.
        /// </summary>
        [UsedImplicitly]
        IcaoCode AirportIcaoCode { get; }
    }
}