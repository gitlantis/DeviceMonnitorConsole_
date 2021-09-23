using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.DBModels
{
    //public class DeviceData
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public Guid Id { get; set; }        
    //    public Guid DeviceGuid { get; set; }
    //    public decimal Param_0 { get; set; }
    //    public decimal Param_1 { get; set; }
    //    public decimal Param_2 { get; set; }
    //    public decimal Param_3 { get; set; }
    //    public decimal Param_4 { get; set; }
    //    public decimal Param_5 { get; set; }
    //    public decimal Param_6 { get; set; }
    //    public decimal Param_7 { get; set; }
    //    public decimal Param_8 { get; set; }
    //    public decimal Param_9 { get; set; }
    //    public decimal Param_10 { get; set; }
    //    public decimal Param_11 { get; set; }
    //    public decimal Param_12 { get; set; }
    //    public decimal Param_13 { get; set; }
    //    public decimal Param_14 { get; set; }
    //    public decimal Param_15 { get; set; }
    //    public decimal Param_16 { get; set; }
    //    public decimal Param_17 { get; set; }
    //    public decimal Param_18 { get; set; }
    //    public decimal Param_19 { get; set; }
    //    public decimal Param_20 { get; set; }
    //    public decimal Param_21 { get; set; }
    //    public decimal Param_22 { get; set; }
    //    public decimal Param_23 { get; set; }
    //    public decimal Param_24 { get; set; }
    //    public decimal Param_25 { get; set; }
    //    public decimal Param_26 { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public DateTime EditedDate { get; set; }
    //    public virtual Device Device { get; set; }
    //}

    public class DeviceData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid DeviceGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public virtual Device Device { get; set; }
        public virtual ICollection<DataAI> DataAIs { get; set; }
        public virtual ICollection<DataAO> DataAOs { get; set; }
        public virtual ICollection<DataDI> DataDIs { get; set; }
        public virtual ICollection<DataDO> DataDOs { get; set; }
        public virtual ICollection<DataMEATADATA> DataMetadatas { get; set; }
    }
}


//public class DeviceData
//{
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public Guid Idd { get; set; }
//    public Guid DeviceGuidd { get; set; }
//    public DateTime CreatedDatee { get; set; }
//    public DateTime EditedDatee { get; set; }
//    public virtual Device Device { get; set; }
//    public virtual ICollection<DataAI> DataAIs { get; set; }
//    public virtual ICollection<DataAO> DataAOs { get; set; }
//    public virtual ICollection<DataDI> DataDIs { get; set; }
//    public virtual ICollection<DataDO> DataDOs { get; set; }
//    public virtual ICollection<DataMEATADATA> DataMetadatas { get; set; }
//}