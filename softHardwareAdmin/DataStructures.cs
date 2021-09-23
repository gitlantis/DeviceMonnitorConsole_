using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softHardwareAdmin
{

class dataStructures    // тип для настройки количества входов/выходов под логирование
    {
        public LiteDB.ObjectId Id { get; set; }
        public string typeName { get; set; }        // название типа устройства
        public int typeNo { get; set; }             // номер типа устройства
        public int AI { get; set; }                 // количество аналоговых входов
        public int AO { get; set; }                 // количество аналоговых выходов
        public int DI { get; set; }                 // количество цифровых входов
        public int DO { get; set; }                 // количество цифровых выходов
    }
}
