using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStoreFranchise.NetCore.Common.Types
{
    public interface IEntityDto<out TAppId>
    {
        TAppId Key();
    }
}
