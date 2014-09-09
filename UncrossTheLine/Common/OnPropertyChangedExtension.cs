namespace WpfCommon.ModelBase
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// The on property changed extension.
    /// </summary>
    public static class OnPropertyChangedExtension
    {
        #region Public Methods and Operators

        /// <summary>
        /// The notify property changed.
        /// </summary>
        /// <param name="propertyChangedBase">
        /// The property changed base.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="T">
        /// The Type.
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// The Property.
        /// </typeparam>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public static void NotifyPropertyChanged<T, TProperty>(
            this T propertyChangedBase, 
            Expression<Func<T, TProperty>> expression)
            where T : ModelBase
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
            {
                string propertyName = memberExpression.Member.Name;
                propertyChangedBase.NotifyPropertyChanged(propertyName);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
