// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#region Usings

using System;
using JetBrains.Annotations;
using StandardLibrary.ArgumentChecking;
using StandardLibrary.DeepCopying;
using StandardLibrary.Hashing;
using WeatherInformation.Core.Enums;

#endregion

namespace WeatherInformation.Core.Source
{
    /// <summary>
    ///     Class that represents basic weather source information.
    /// </summary>
    public class WeatherSource : IEquatable<WeatherSource>, IDeepCopyable<WeatherSource>
    {
        /// <summary>
        ///     Constructor of a class that provides basic information about weather source.
        /// </summary>
        /// <param name="name">Weather information source name.</param>
        /// <param name="category">Weather information source category.</param>
        /// <param name="description">Weather information source description.</param>
        /// <exception cref="ArgumentException">
        ///     If <paramref name="name" /> or <paramref name="description" /> are incorrect strings.
        /// </exception>
        public WeatherSource([NotNull] string name, WeatherSourcesCategories category, [NotNull] string description)
        {
            StringArgumentChecking.ThrowExceptionIfStringIsInvalid(name, nameof(name));
            StringArgumentChecking.ThrowExceptionIfStringIsInvalid(description, nameof(description));

            Name = name;
            Category = category;
            Description = description;
        }

        /// <summary>
        ///     Weather information source category.
        /// </summary>
        public WeatherSourcesCategories Category { get; }

        /// <summary>
        ///     Weather information source description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Weather information source name.
        /// </summary>
        public string Name { get; }

        #region Implementation of IDeepCopiable<WeatherSource>

        /// <inheritdoc />
        public WeatherSource DeepCopy()
        {
            return new WeatherSource(Name, Category, Description);
        }

        #endregion

        #region Implementation of IEquatable<WeatherSource>

        /// <inheritdoc />
        public bool Equals(WeatherSource other)
        {
            if (other == null)
            {
                return false;
            }

            return Category == other.Category
                   && Name == other.Name
                   && Description == other.Description;
        }

        #endregion

        #region Overrides of Object

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as WeatherSource);
        }

        /// <summary>
        ///     Custom hash function.
        /// </summary>
        /// <returns>Hash code of current object.</returns>
        public override int GetHashCode()
        {
            return CustomHash
                .GetInitialHashNumber()
                .AddToHashNumber(Category.GetHashCode())
                .AddToHashNumber(Name.GetHashCode())
                .AddToHashNumber(Description.GetHashCode())
                .AddToHashNumber(nameof(WeatherSource).GetHashCode());
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"WeatherSource:\n Name {Name} \n Category {Category}\n Description {Description}.";
        }

        #endregion
    }
}