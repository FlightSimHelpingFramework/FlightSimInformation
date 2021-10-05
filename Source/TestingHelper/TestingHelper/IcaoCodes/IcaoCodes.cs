// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using StandardLibrary.Codes;

#endregion

namespace TestingHelper.IcaoCodes
{
    [ExcludeFromCodeCoverage]
    public static class IcaoCodes
    {
        public static readonly object[] CollectionsWithInvalidStringIcaoCodes =
        {
            new[] { new List<string> { "uUEE" } },
            new[] { new List<string> { "", " ", "E" } },
            new[] { new List<string> { "UUE_E", "1EDDF", "M2UHA" } },
            new[] { new List<string> { "UUErE", "EDDafF", "EDdDT", "%LOaWI" } }
        };

        public static readonly object[] CollectionsWithMixedValidAndInvalidStringIcaoCodes =
        {
            new[] { new List<string> { "UUEE", null } },
            new[] { new List<string> { "UMKK", "UUEe" } },
            new[] { new List<string> { "UUEE", "EDDF", "EDDF_" } },
            new[] { new List<string> { "UUE3", "EDDF12", " " } }
        };

        public static readonly object[] CollectionsWithValidStringIcaoCodes =
        {
            new[] { new List<string> { "UUEE" } },
            new[] { new List<string> { "UMKK", "UEEE", "EGLL" } },
            new[] { new List<string> { "UUEE", "EDDF", "MUHA" } },
            new[] { new List<string> { "UUEE", "EDDF", "EDDT", "LOWI" } }
        };

        public static readonly string[] InvalidStringIcaoCodesCollection =
        {
            null, "", " ", "KLaX", "_2340g", "UUEЁ", "%$#*", " UUEE"
        };

        public static readonly string[] ValidStringIcaoCodesCollection =
        {
            "EDDF", "EGLL", "KPHX", "KSFO"
        };

        public static readonly string[] NonExistingStringIcaoCodesCollection =
        {
            "ABCD", "AAAA", "YXZY"
        };

        public static readonly object[] CollectionsWithMixedValidAndInvalidIcaoCodeContainers =
        {
            new List<SimpleIcaoCodeContainer> { null, new SimpleIcaoCodeContainer(new IcaoCode("EDDF")) },
            new List<SimpleIcaoCodeContainer> { new SimpleIcaoCodeContainer(new IcaoCode("EDDF")), null },
            new List<SimpleIcaoCodeContainer>
            {
                new SimpleIcaoCodeContainer(new IcaoCode("KLAX")),
                new SimpleIcaoCodeContainer(new IcaoCode("KMIA")), null,
                new SimpleIcaoCodeContainer(new IcaoCode("KLAX"))
            }
        };

        public static readonly object[] CollectionsWithValidAndRealIcaoCodesContainers =
        {
            new List<SimpleIcaoCodeContainer> { new SimpleIcaoCodeContainer(new IcaoCode("EDDF")) },
            new List<SimpleIcaoCodeContainer>
            {
                new SimpleIcaoCodeContainer(new IcaoCode("EDDF")), new SimpleIcaoCodeContainer(new IcaoCode("EGLL"))
            },
            new List<SimpleIcaoCodeContainer>
            {
                new SimpleIcaoCodeContainer(new IcaoCode("EDDF")), new SimpleIcaoCodeContainer(new IcaoCode("KPHX")),
                new SimpleIcaoCodeContainer(new IcaoCode("KSFO"))
            }
        };
    }
}