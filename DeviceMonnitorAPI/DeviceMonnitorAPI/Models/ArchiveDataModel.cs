using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class ArchiveDataModel
    {
        public Guid DeviceGuid { get; set; }
        public string Name { get; set; }
        public int DataCount { get; set; }
        public int PageCount { get; set; }
        public int ItemCount { get; set; }
        public int PageNum { get; set; }
        public int RowCount { get; set; } 
        public DateTime CreatedDate { get; set; }
        public List<ChildDataWithParam> AI { get; set; }
        public List<ChildDataWithParam> AO { get; set; }
        public List<ChildDataWithParam> DI { get; set; }
        public List<ChildDataWithParam> DO { get; set; }
        public List<ChildDataWithParam> Metadata { get; set; }
    }
}
