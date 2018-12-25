namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS background definition.
    /// </summary>
    public sealed class Background : ICssValue
    {
        private readonly ICssValue _layers;
        private readonly ICssValue _color;

        /// <summary>
        /// Creates a new CSS background definition.
        /// </summary>
        /// <param name="layers">The used layers.</param>
        /// <param name="color">The set color.</param>
        public Background(ICssValue layers, ICssValue color)
        {
            _layers = layers;
            _color = color;
        }

        /// <summary>
        /// Gets the used color.
        /// </summary>
        public ICssValue Color
        {
            get { return _color; }
        }

        /// <summary>
        /// Gets the defined layers.
        /// </summary>
        public ICssValue Layers
        {
            get { return _layers; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var layer = _layers.CssText;

                if (_color != null)
                {
                    var sep = layer.Length > 0 ? " " : "";
                    var color = _color.CssText;
                    return String.Concat(layer, sep, color);
                }

                return layer;
            }
        }
    }
}