using System;

namespace Cake.Core.IO
{
    /// <summary>
    /// Thrown when a process argument renderer in <see cref="ProcessArgumentQuoting.NeverAndThrow"/> mode must quote an argument.
    /// </summary>
    public sealed class InvalidUnquotedArgumentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUnquotedArgumentException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidUnquotedArgumentException(string message) : base(message)
        {
        }
    }
}