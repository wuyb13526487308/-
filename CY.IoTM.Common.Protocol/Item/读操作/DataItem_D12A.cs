﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 历史计量数据11
    /// </summary>
    public class DataItem_D12A : DataItem_D120
    {
        protected override void Set单元标识符()
        {
            this.dataUnits[0] = 0xD1;
            this.dataUnits[1] = 0x2A;
        }
    }
}
