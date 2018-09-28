using System;

namespace ArduinoBusiness
{
    public class ControleTemperatura
    {
        public int Id { get; set; }
        public int IdSensor { get; set; }
        public DateTime Data { get; set; }
        public decimal Temperatura { get; set; }
        public decimal Tensao { get; set; }
        public string Dados { get; set; }
    }
}
