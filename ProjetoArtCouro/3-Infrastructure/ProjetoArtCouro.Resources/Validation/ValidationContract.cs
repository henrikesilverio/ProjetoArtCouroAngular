using System;
using System.Linq.Expressions;
using ProjetoArtCouro.Resources.Resources;

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

            if (string.IsNullOrWhiteSpace(val))
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldIsRequired, name)
                    : message);
            }

            return this;
        }

        /// <summary>
        /// Given a bool, add a notification if it's null or empty
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsRequired(Expression<Func<T, bool>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (string.IsNullOrEmpty(val.ToString()))
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldIsRequired, name)
                    : message);
            }

            return this;
        }

        /// <summary>
        /// Given a decimal, add a notification if it's null or empty
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsRequired(Expression<Func<T, decimal>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (string.IsNullOrEmpty(val.ToString()))
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldIsRequired, name)
                    : message);
            }

            return this;
        }

        /// <summary>
        /// Given a int, add a notification if it's null or empty
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsRequired(Expression<Func<T, int>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (string.IsNullOrEmpty(val.ToString()))
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldIsRequired, name)
                    : message);
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
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldCannotBeNull, name)
                    : message);
            }

            return this;
        }

        /// <summary>
        /// Given an decimal, add a notification if it's null
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsNotZero(Expression<Func<T, int>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val == 0.0M)
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldCannotBeZero, name)
                    : message);
            }

            return this;
        }

        /// <summary>
        /// Given an decimal, add a notification if it's null
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsNotZero(Expression<Func<T, decimal>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val == 0.0M)
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message) 
                    ? string.Format(Erros.FieldCannotBeZero, name)
                    : message);
            }

            return this;
        }

        /// <summary>
        /// Given an DateTime, add a notification if it's equals
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="value">Value compare</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsNotEquals(Expression<Func<T, DateTime>> selector, object value, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = (selector.Body as MemberExpression)?.Member.Name ??
                       ((MemberExpression)((UnaryExpression)selector.Body).Operand).Member.Name;

            if (val.Equals(value))
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldCannotBe, name, (DateTime)value)
                    : message);
            }

            return this;
        }

        /// <summary>
        /// Given an Enum, add a notification if it's equals
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="value">Value compare</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsNotEquals(Expression<Func<T, Enum>> selector, object value, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression) ((UnaryExpression) selector.Body).Operand).Member.Name;

            if (val.ToString() == value.ToString())
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldCannotBe, name, value.ToString())
                    : message);
            }

            return this;
        }

        /// <summary>
        /// Given a string, add a notification if it's length is greater than max parameter
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="max">Maximum Length</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> HasMaxLenght(Expression<Func<T, string>> selector, int max, string message = "")
        {
            var val = selector.Compile().Invoke(_validatable);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (!string.IsNullOrEmpty(val) && val.Length > max)
            {
                _validatable.AddNotification(name, string.IsNullOrEmpty(message)
                    ? string.Format(Erros.FieldMustHaveMaxCharacters, name, max)
                    : message);
            }

            return this;
        }
    }
}
