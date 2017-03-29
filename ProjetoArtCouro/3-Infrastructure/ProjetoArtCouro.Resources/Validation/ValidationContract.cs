using System;
using System.Linq.Expressions;

namespace ProjetoArtCouro.Resources.Validation
{
    public class ValidationContract<T> where T : Notifiable
    {
        private readonly T _validatable;

        public ValidationContract(T validatable)
        {
            _validatable = validatable;
        }

        /// <summary>
        /// Given a string, add a notification if it's null or empty
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsRequired(Expression<Func<T, string>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (string.IsNullOrEmpty(val))
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message) ? $"Field {name} is required." : message);
            }

            return this;
        }

        /// <summary>
        /// Given an object, add a notification if it's null
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsNotNull(Expression<Func<T, object>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val == null)
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message) ? $"Field {name} cannot be null." : message);
            }

            return this;
        }
    }
}
