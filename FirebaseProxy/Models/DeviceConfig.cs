using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirebaseProxy.Models
{
    public class DeviceConfig
    {
        public Guid? ConfGuid { get; set; }
        public Guid DeviceGuid { get; set; }

        //Кучланиншнинг максимал киймати (В)
        [Range(1, 500)]
        public decimal UMax { get; set; }
        //Кучланишнинг минимал киймати (В)
        [Range(1, 500)]
        public decimal UMin { get; set; }

        //Токнинг максимал киймати (А)
        //[Range(1, 5000)]
        //public decimal Cup { get; set; }
        //Токнинг номинал киймати (А)
        [Range(1, 4000)]
        public decimal Calm { get; set; }
        //Токнинг минимал киймати (А)
        //[Range(1, 4000)]
        //public decimal Cadw { get; set; }
        //Сувнинг юкори сатхи (%)
        [Range(1, 100)]
        public decimal Wup { get; set; }
        ////Сувнинг хозирги сатхи (%)
        //[Range(1, 100)]
        //public decimal? Wl { get; set; }
        //Сувнинг минимал сатхи (%)
        [Range(1, 100)]
        public decimal Wdw { get; set; }

        //Екиб учиришлар орасидаги вакт (мин)
        //[Range(1, 60)]
        //public int Ontime { get; set; }
        //Хатолик юз бергандан кейин екиш учун оралик вакт (мин)
        //[Range(1, 60)]
        //public int Ertime { get; set; }

        //Ток максимал киймат вакти (сек)
        [Range(1, 999)]
        public int Overtime { get; set; }
        //Токнинг минимал киймат вакти (сек)
        [Range(1, 999)]
        public int DownTime { get; set; }
        //Кучланишнинг максимал вакти (сек)
        [Range(1, 999)]
        public int OverVtime { get; set; }
        //Кучланишнинг минимал вакти (сек)
        [Range(1, 999)]
        public int LowVtime { get; set; }

        //[Range(1, 20)]
        //public int EMode { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }

    }
}
