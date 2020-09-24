using System;
using System.Runtime.Serialization;

namespace ClothingStoreFranchise.NetCore.Common.Exceptions
{
    [Serializable]
    public abstract class CustomException : Exception
    {
        protected CustomException() { }

        protected CustomException(string message) : base(message) { }

        protected CustomException(string message, Exception inner) : base(message, inner) { }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public abstract string Description { get; }
    }
}
