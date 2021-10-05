// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

#region Usings

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace TestingHelper.Strings
{
    [ExcludeFromCodeCoverage]
    public static class Strings
    {
        public static readonly object[] CollectionsWithMixedValidAndInvalidStrings =
        {
            new[] { new List<string> { "String", null } },
            new[] { new List<string> { "String", "Valid string (123456789!;)", "" } },
            new[]
            {
                new List<string>
                    { "qwertyuiop[]asdfghjkl;'zxcvbnm,./'", "1 2 3 4 5 _ 6 _ 7 _ 8 _ 9 _ 101", "Ё!№;%:?*()_+)", " " }
            },
            new[] { new List<string> { "UUErE", "EDDafF", "EDdDT", "%LOaWI", null, "", " " } }
        };

        public static readonly object[] CollectionsWithValidStrings =
        {
            new[] { new List<string> { "String" } },
            new[] { new List<string> { "String", "One more valid string (123456789!;)" } },
            new[]
            {
                new List<string>
                    { "qwertyuiop[]asdfghjkl;'zxcvbnm,./'", "1 2 3 4 5 _ 6 _ 7 _ 8 _ 9 _ 101", "Ё!№;%:?*()_+)" }
            },
            new[] { new List<string> { "UUErE", "EDDafF", "EDdDT", "%LOaWI" } }
        };

        public static readonly string[] InvalidStrings =
        {
            "",
            " ",
            "   ",
            null
        };

        public static readonly object[] ValidStrings =
        {
            "String",
            "Valid",
            "Valid string (123456789!;)",
            "Ё!№;%:?*()_+)",
            "1 2 3 4 5 _ 6 _ 7 _ 8 _ 9 _ 101",
            "1234567890-=qwertyuiop[]\asdfghjkl;'zxcvbnm,./~!@#$%^&*()_+'",
            "йцукенгшщзхъфывапролджэячсмитьбю.",
            "ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ.'",
            "qwertyuiop[]asdfghjkl;'zxcvbnm,./'",
            "QWERTYUIOP[]ASDFGHJKL;'ZXCVBNM,./'",
            "QWERTYUIOP[]ASDFGHJKL;'ZXCVBNM,./iiuoiuh5jthbk9084098650789347q34t;l'"
        };
    }
}