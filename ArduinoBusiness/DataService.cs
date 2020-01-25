using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ArduinoBusiness
{
    public class DataService
    {
        private const string stringConnection = @"Data Source=serverdbft.database.windows.net;Initial Catalog = FTDB1; Integrated Security = False; User Id = fausto;Password=f168700t!;Encrypt=True;TrustServerCertificate=False;MultipleActiveResultSets=True";

        public bool Insert(ControleTemperatura control)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(stringConnection))
                {
                    string sqlQuery = "INSERT INTO ControleTemperatura values (@IdSensor, @Data, @Temperatura, @Tensao, @Dados)";
                    int rowsAffected = db.Execute(sqlQuery, control);
                    return (rowsAffected > 0);
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Erro ao conectar Banco.");
                //                throw;
                
            }

            return false;
        }

        public bool InsertMysql(ControleTemperatura control)
        {
            string stringConnectionMysql = @"Server=192.168.137.131;Database=test;Uid=root;Pwd=Password1;";
            try
            {
                using (IDbConnection db = new MySqlConnection(stringConnectionMysql))
                {
                    string sqlQuery = "INSERT INTO ControleTemperatura (idsensor,data,temperatura,tensao,dados) values (@IdSensor, @Data, @Temperatura, @Tensao, @Dados)";
                    int rowsAffected = db.Execute(sqlQuery, control);
                    return (rowsAffected > 0);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Erro ao conectar Banco." + ex.Message);
                //                throw;

            }

            return false;
        }

    }
}
