using Newtonsoft.Json;
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

            if ((splitedData.Length > 11) && decimal.TryParse(splitedData[7], out tmpReceived))
            {
                temp = (tmpReceived/100);
                countDataReceived++;
            }

            if (countDataReceived >= tamEntrada)
            {
                countDataReceived = 0;
                //temp = temp;// / tamEntrada;

                var dt = new DataService();
                decimal tensao = 0;
                decimal.TryParse(splitedData[12], out tensao);

                dt.InsertMysql(new ControleTemperatura()
                {
                    Data = DateTime.Now,
                    IdSensor = 1,
                    Temperatura = Math.Round(temp, 2),
                    Tensao = tensao/100,
                    Dados = splitedData[5]
                });

            }
        }


        public void ReceivedDataV2(string jSonData)
        {
            var dt = new DataService();

            if (jSonData.StartsWith("{") && jSonData.EndsWith("}"))
            {
                DataModel data = JsonConvert.DeserializeObject<DataModel>(jSonData);

                dt.InsertMysql(new ControleTemperatura()
                {
                    Data = DateTime.Now,
                    IdSensor = 1,
                    Temperatura = Math.Round((decimal)data.LastTemp, 2),
                    Tensao = (decimal)data.Vin,
                    Dados = $"Servo:{data.ServoPos}, TempAmb:{data.TempAmbient}, Hall:{data.HallStatus}"
                });
            }
        }




    }
}
