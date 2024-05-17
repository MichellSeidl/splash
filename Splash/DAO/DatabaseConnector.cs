using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Splash._Utilities;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Splash.DAO
{
    public class DatabaseConnector
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnector(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Busca o usuário no banco de dados e valida se o hash da senha digitada bate com o hash salvo no banco de dados.
        /// </summary>
        /// <param name="query">Query a ser executada</param>
        /// <param name="username">Parametro a ser inserido na query (avoid SQL injection)</param>
        /// <param name="hashedPassword">Senha digitada em hash sha256</param>
        /// <returns>Retorna 'true' caso a senha esteja correta</returns>
        /// <returns>Retorna 'false' caso o usuário não exista na base de dados ou a senha esteja incorreta</returns>
        public Boolean ValidateLogin(string query, string? username, string? hashedPassword)
        {
            string? azureConnectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario", username);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            //while (reader.Read())
                            //{
                            string senha_db = reader.GetString(reader.GetOrdinal("senha"));
                            if (senha_db != hashedPassword)
                            {
                                connection.Close();
                                return false;
                            }
                            //}
                        }
                        else
                        {
                            connection.Close();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
                connection.Close();
            }
            return true;
        }

        /// <summary>
        /// Busca os dados dos sensores
        /// </summary>
        /// <param name="query">Query a ser executada</param>
        /// <returns>Retorna os dados dos sensores</returns>
        public string GetSensorData(string query)
        {
            query = "select * from aparelho a inner join indicador_ph i on a.id = i.id_aparelho";
            string? azureConnectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            var sensores = new List<Object>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string ativo = reader.GetBoolean(reader.GetOrdinal("ativo")).ToString();
                            string qtde_equalisador = reader.GetDouble(reader.GetOrdinal("qtde_equalisador")).ToString();
                            string valor_ph = reader.GetDouble(reader.GetOrdinal("valor_ph")).ToString();
                            sensores.Add(ativo + "|" + qtde_equalisador + "|" + valor_ph);
                        }
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        Console.WriteLine(ex.Message);
                    }
                }
                connection.Close();
            }
            return JsonConvert.SerializeObject(sensores);
        }
    }
}

