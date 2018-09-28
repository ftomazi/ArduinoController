using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ArduinoBusiness
{
    public class DataService
    {
        private const string stringConnection = @"Data Source=serverdbft.database.windows.net;Initial Catalog = FTDB1; Integrated Security = False; User Id = fausto;Password=f168700t!;Encrypt=True;TrustServerCertificate=False;MultipleActiveResultSets=True";

        public bool Insert(ControleTemperatura control)
        {
            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                string sqlQuery = "INSERT INTO ControleTemperatura values (@IdSensor, @Data, @Temperatura, @Tensao, @Dados)";
                int rowsAffected = db.Execute(sqlQuery, control);
                return (rowsAffected > 0);
            }
        }

    }
}
