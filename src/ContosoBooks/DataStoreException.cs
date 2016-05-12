using System;
using System.Runtime.Serialization;

namespace ContosoBooks.Controllers
{
    internal class DataStoreException : Exception
    {
        public DataStoreException()
        {
        }

        public DataStoreException(string message) : base(message)
        {
        }

        public DataStoreException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}