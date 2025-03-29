using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdmsols.XTB.AuditRecordCounterByTable.Helpers
{
    public class EntityInfo
    {
        public string EntityName { get; set; }
        public int ObjectTypeCode { get; set; }
        public int Count { get; set; }
        public bool AuditEnabled { get; set; }
    }
}
