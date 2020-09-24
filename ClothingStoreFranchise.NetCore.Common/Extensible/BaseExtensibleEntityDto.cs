using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClothingStoreFranchise.NetCore.Common.Extensible
{
    public abstract class BaseExtensibleEntityDto
    {
        public abstract string ExtensibleEntityName { get; }

        public Dictionary<string, object> ExtendedData { get; set; }

        protected BaseExtensibleEntityDto()
        {
            ExtendedData = new Dictionary<string, object>();
        }
    }
}
