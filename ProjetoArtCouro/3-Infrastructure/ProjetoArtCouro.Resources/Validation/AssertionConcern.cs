using System;
using System.Text.RegularExpressions;

namespace ProjetoArtCouro.Resource.Validation
{
    public class AssertionConcern<T> where T : Exception, new()
    {
        public static void AssertArgumentEquals(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentFalse(bool value, string message)
        {
            if (value)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentTrue(bool value, string message)
        {
            if (!value)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentLength(string stringValue, int maximum, string message)
        {
            var length = stringValue.Trim().Length;
            if (length > maximum)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            if (string.IsNullOrEmpty(stringValue))
                stringValue = String.Empty;

            var length = stringValue.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentMatches(string pattern, string stringValue, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(stringValue))
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentNotEquals(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentNull(object value, string message)
        {
            if (value != null)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentNotNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentRange(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentRange(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentRange(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        public static void AssertArgumentRange(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        protected AssertionConcern()
        {
        }

        protected void SelfAssertArgumentEquals(object object1, object object2, string message)
        {
            AssertArgumentEquals(object1, object2, message);
        }

        protected void SelfAssertArgumentFalse(bool boolValue, string message)
        {
            AssertArgumentFalse(boolValue, message);
        }

        protected void SelfAssertArgumentLength(string stringValue, int maximum, string message)
        {
            AssertArgumentLength(stringValue, maximum, message);
        }

        protected void SelfAssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            AssertArgumentLength(stringValue, minimum, maximum, message);
        }

        protected void SelfAssertArgumentMatches(string pattern, string stringValue, string message)
        {
            AssertArgumentMatches(pattern, stringValue, message);
        }

        protected void SelfAssertArgumentNotEmpty(string stringValue, string message)
        {
            AssertArgumentNotEmpty(stringValue, message);
        }

        protected void SelfAssertArgumentNotEquals(object object1, object object2, string message)
        {
            AssertArgumentNotEquals(object1, object2, message);
        }

        protected void SelfAssertArgumentNotNull(object object1, string message)
        {
            AssertArgumentNotNull(object1, message);
        }

        protected void SelfAssertArgumentRange(double value, double minimum, double maximum, string message)
        {
            AssertArgumentRange(value, minimum, maximum, message);
        }

        protected void SelfAssertArgumentRange(float value, float minimum, float maximum, string message)
        {
            AssertArgumentRange(value, minimum, maximum, message);
        }

        protected void SelfAssertArgumentRange(int value, int minimum, int maximum, string message)
        {
            AssertArgumentRange(value, minimum, maximum, message);
        }

        protected void SelfAssertArgumentRange(long value, long minimum, long maximum, string message)
        {
            AssertArgumentRange(value, minimum, maximum, message);
        }

        protected void SelfAssertArgumentTrue(bool boolValue, string message)
        {
            AssertArgumentTrue(boolValue, message);
        }
    }
}
