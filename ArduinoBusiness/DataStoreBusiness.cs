using System;

namespace ArduinoBusiness
{
    public class DataStoreBusiness
    {
        private decimal temp = 0;
        private int countDataReceived = 0;
        private int tamEntrada = 25;

        public DataStoreBusiness(int tmpInsert)
        {
            tamEntrada = tmpInsert;
        }

        public void ReceivedData(string data)
        {
            decimal tmpReceived = 0;
            string[] splitedData = data.Split(' ');

            if ((splitedData.Length > 10) && decimal.TryParse(splitedData[1], out tmpReceived))
            {
                temp = (tmpReceived/100);
                countDataReceived++;
            }

            if (countDataReceived >= tamEntrada)
            {
                countDataReceived = 0;
                //temp = temp;// / tamEntrada;

                var dt = new DataService();
                dt.Insert(new ControleTemperatura()
                {
                    Data = DateTime.Now,
                    IdSensor = 1,
                    Temperatura = Math.Round(temp, 2),
                    Tensao = 0,
                    Dados = splitedData[7]
                });

            }
        }
    }
}
