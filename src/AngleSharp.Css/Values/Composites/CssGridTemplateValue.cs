namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS grid template definition.
    /// </summary>
    sealed class CssGridTemplateValue : ICssCompositeValue, IEquatable<CssGridTemplateValue>
    {
        #region Fields

        private readonly ICssValue _rows;
        private readonly ICssValue _columns;
        private readonly ICssValue _areas;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS grid template definition.
        /// </summary>
        /// <param name="rows">The rows value to use.</param>
        /// <param name="columns">The columns value to use.</param>
        /// <param name="areas">The areas value to use.</param>
        public CssGridTemplateValue(ICssValue rows, ICssValue columns, ICssValue areas)
        {
            _rows = rows;
            _columns = columns;
            _areas = areas;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the template rows.
        /// </summary>
        public ICssValue TemplateRows => _rows;

        /// <summary>
        /// Gets the value for the template columns.
        /// </summary>
        public ICssValue TemplateColumns => _columns;

        /// <summary>
        /// Gets the value for the template areas.
        /// </summary>
        public ICssValue TemplateAreas => _areas;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var rows = String.Empty;
                var cols = _columns?.CssText;

                if (_areas is CssConstantValue<object> || _columns is CssConstantValue<object> || _rows is CssConstantValue<object>)
                {
                    return CssKeywords.None;
                }
                else if (_areas != null)
                {
                    var areas = (_areas as CssTupleValue)?.Items;
                    var rowItems = (_rows as CssTupleValue)?.Items;
                    var newRows = new List<ICssValue>();

                    if (rowItems != null && areas != null)
                    {
                        for (var i = 0; i < rowItems.Length; i++)
                        {
                            var area = areas.Length > i ? areas[i] : null;
                            var item = rowItems[i] as CssTupleValue;

                            if (item != null && area != null)
                            {
                                var newItems = new List<ICssValue>(item.Items);
                                newItems.Insert(1, area);
                                newRows.Add(new CssTupleValue(newItems.ToArray()));
                            }
                        }
                    }

                    rows = new CssTupleValue(newRows.ToArray()).CssText;
                }
                else if (_rows != null)
                {
                    rows = _rows.CssText;
                }

                if (!String.IsNullOrEmpty(cols))
                {
                    return String.Concat(rows, " / ", cols);
                }

                return rows;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssGridTemplateValue other)
        {
            return _areas.Equals(other._areas) && _columns.Equals(other._columns) && _rows.Equals(other._rows);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssGridTemplateValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var rows = _rows.Compute(context);
            var columns = _columns.Compute(context);
            var areas = _areas.Compute(context);
            return new CssGridTemplateValue(rows, columns, areas);
        }

        #endregion
    }
}
