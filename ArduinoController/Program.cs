using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArduinoBusiness;

namespace ArduinoController
{
    class Program
    {
        public static SerialPort sp;
        public static DataStoreBusiness dataStore;

        static void Main(string[] args)
        {
            int tmpInsert = Convert.ToInt16(ConfigurationSettings.AppSettings["TimeInsert"]);
            dataStore = new DataStoreBusiness(tmpInsert); 

            SerialPort porta = new SerialPort();

            porta.BaudRate = 9600;
            //porta.CtsHolding = false;
            porta.DtrEnable = false;
            porta.Parity = Parity.None;
            //porta.PortName = "COM11";
            // porta.PortName = "COM7";
            String port = ConfigurationSettings.AppSettings["Port"];
            porta.PortName = port; //"COM4";

            porta.Open();
            sp = porta;

            Thread th = new Thread(ler);
            th.Start();

            string x = "";
            while (x != "q")
            {
                Console.Write("Send:");
                x = Console.ReadLine();

                porta.WriteLine(x);

            }
            sp.Close();
        }

        static void ler()
        {
            while (true)
            {
                string x = sp.ReadLine();
                if (x != "\r")
                {
                    var data = x.ToString();
                    dataStore.ReceivedDataV2(data);
                    Console.WriteLine("Received:" + data);
                    Console.Write("Send:");
                }
            }
        }
    }
}
