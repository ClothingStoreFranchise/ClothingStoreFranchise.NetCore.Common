using System;
using System.Runtime.Serialization;

namespace ClothingStoreFranchise.NetCore.Common.Exceptions
{
    [Serializable]
    public class InvalidDataException : CustomException
    {
        public InvalidDataException() { }
        public InvalidDataException(string message) : base(message) { }
        public InvalidDataException(string message, Exception inner) : base(message, inner) { }
        protected InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string Description => "Invalid data";
    }
}
