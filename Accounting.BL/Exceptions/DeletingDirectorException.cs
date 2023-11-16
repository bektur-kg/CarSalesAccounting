using System;

namespace Accounting.BL.Exceptions
{
    public class DeletingDirectorException : Exception
    {
        public DeletingDirectorException() : base() { }
        public DeletingDirectorException(string message) : base(message) { }
    }
}
