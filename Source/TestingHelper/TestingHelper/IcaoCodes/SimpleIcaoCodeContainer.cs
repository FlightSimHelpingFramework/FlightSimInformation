// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System.Diagnostics.CodeAnalysis;
using StandardLibrary.Codes;

#endregion

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