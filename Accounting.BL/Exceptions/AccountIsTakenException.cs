using System;

namespace Accounting.BL.Exceptions
{
    public class AccountIsTakenException : Exception
    {
        public AccountIsTakenException() : base() { }
        public AccountIsTakenException(string message) : base(message) { }
    }
}
