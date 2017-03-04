using System;

namespace Sisyphus.Core
{
    public class SisyphusException : Exception
    {
        public SisyphusException(string message) : base(message)
        { }
    }
}
