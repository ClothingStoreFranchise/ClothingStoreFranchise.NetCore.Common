using System;
using System.Runtime.Serialization;

namespace ClothingStoreFranchise.NetCore.Common.Exceptions
{
    [Serializable]
    public class EntityAlreadyExistsException : CustomException
    {
        public EntityAlreadyExistsException() { }
        public EntityAlreadyExistsException(string message) : base(message) { }
        public EntityAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override string Description => "Entity already exists";
    }
}
