// This is an open source non-commercial project. Dear PVS-Studio, please check it.
//  PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// 

namespace WeatherInformation.Core.Enums
{
    /// <summary>
    ///     Weather information source accessibility categories.
    /// </summary>
    public enum WeatherSourcesCategories
    {
        /// <summary>
        ///     Open - no authentication required.
        /// </summary>
        Public,

        /// <summary>
        ///     Authenticated - requires an API key or other authentication method.
        /// </summary>
        Authenticated,

        /// <summary>
        ///     A source category that cannot be explicitly categorized.
        /// </summary>
        Custom
    }
}