using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreExtensions
{
    /// <inheritdoc />
    /// <summary>
    /// 	Generic exception for combining several other exceptions
    /// </summary>
    public class CombinedException : Exception
    {
        /// <summary>
        /// 	Gets the inner exceptions.
        /// </summary>
        /// <value>The inner exceptions.</value>
        public Exception[] InnerExceptions { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        /// 	Initializes a new instance of the <see cref="T:CoreExtensions.CombinedException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerExceptions">The inner exceptions.</param>
        public CombinedException(string message, Exception[] innerExceptions) : base(message)
        {
            InnerExceptions = innerExceptions;
        }

        public static Exception Combine(string message, params Exception[] innerExceptions)
        {
            if (innerExceptions.Length == 1)
                return innerExceptions[0];

            return new CombinedException(message, innerExceptions);
        }

        /// <summary>
        /// Combines the specified exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerExceptions">The inner exceptions.</param>
        /// <returns></returns>
        public static Exception Combine(string message, IEnumerable<Exception> innerExceptions)
        {
            return Combine(message, innerExceptions.ToArray());
        }
    }
}
