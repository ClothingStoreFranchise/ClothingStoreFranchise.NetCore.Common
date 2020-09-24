using System;
using System.Runtime.Serialization;

namespace ClothingStoreFranchise.NetCore.Common.Exceptions
{
    [Serializable]
    public class EntityHasDependenciesException : CustomException
    {
        public EntityHasDependenciesException() { }
        public EntityHasDependenciesException(string message) : base(message) { }
        public EntityHasDependenciesException(string message, Exception inner) : base(message, inner) { }
        protected EntityHasDependenciesException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override string Description => "Entity has dependencies";
    }
}
