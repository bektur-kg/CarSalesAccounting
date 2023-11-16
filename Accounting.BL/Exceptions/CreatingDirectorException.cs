using System;

namespace Accounting.BL.Exceptions
{
    public class CreatingDirectorException : Exception
    {
        public CreatingDirectorException() : base() { }
        public CreatingDirectorException(string message) : base(message) { }
    }
}
