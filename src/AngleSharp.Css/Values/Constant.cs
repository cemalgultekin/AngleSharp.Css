namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a selected CSS enum value.
    /// </summary>
    public sealed class Constant<T> : ICssValue
    {
        private readonly String _key;
        private readonly T _data;

        /// <summary>
        /// Creates a new selected CSS enum value.
        /// </summary>
        /// <param name="key">The key representation.</param>
        /// <param name="data">The associated data.</param>
        public Constant(String key, T data)
        {
            _key = key;
            _data = data;
        }

        /// <summary>
        /// Gets the associated value.
        /// </summary>
        public T Value
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return _key; }
        }
    }
}