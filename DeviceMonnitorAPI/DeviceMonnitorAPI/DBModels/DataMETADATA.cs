using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.DBModels
{
    public class DataMEATADATA
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DataGuid { get; set; }
        public string Param { get; set; }
        public virtual DeviceData DeviceData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
