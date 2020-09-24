using System;
using System.Runtime.Serialization;

namespace ClothingStoreFranchise.NetCore.Common.Exceptions
{
    [Serializable]
    public class EntityDoesNotExistException : CustomException
    {
        public EntityDoesNotExistException() { }
        public EntityDoesNotExistException(string message) : base(message) { }
        public EntityDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected EntityDoesNotExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override string Description => "Entity does not exist";
    }
}
