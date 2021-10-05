// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

using System.Diagnostics.CodeAnalysis;
using StandardLibrary.Codes;

namespace TestingHelper.IcaoCodes
{
    [ExcludeFromCodeCoverage]
    public class SimpleIcaoCodeContainer : IIcaoCodeContainer
    {
        public SimpleIcaoCodeContainer(IcaoCode icaoCode)
        {
            AirportIcaoCode = icaoCode;
        }

        #region Implementation of IIcaoCodeContainer

        /// <inheritdoc />
        public IcaoCode AirportIcaoCode { get; }

        #endregion
    }
}