using MySqlConnector;
using UserApi.Models.Entity;

namespace UserApi.Dal
{
    public class SqLiteDataAccessLayer
    {
        MySqlConnection _conn;
        string _tableName;
        private string _connectionString;
        public SqLiteDataAccessLayer(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _conn = new MySqlConnection(connectionString);
            _tableName = tableName;
        }

        public int Update(UserEntity entity)
        {
            int res = 0;
            string query = $"UPDATE {_tableName} SET " +
                   "firstName = @firstName" +
                   "lastName = @lastName" +
                   "accountStatus = @accountStatus" +
                   "WHERE email = @email";

            MySqlCommand command = new MySqlCommand(query, _conn);
            command.Parameters.AddWithValue("@tableName", _tableName);
            command.Parameters.AddWithValue("@firstName", entity.FirstName);
            command.Parameters.AddWithValue("@lastName", entity.LastName);
            command.Parameters.AddWithValue("@email", entity.Email);
            command.Parameters.AddWithValue("@accountStatus", entity.AccountStatus);

            try
            {
                _conn.Open();
                res = command.ExecuteNonQuery();
                Console.WriteLine("Update réussi !");
            }
            catch (Exception ex)
            {
                res = 0;
                Console.WriteLine("Erreur lors de l'update : " + ex.Message);
            }
            finally
            {
                _conn.Close();
            }

            return res;
        }

        public int Insert(UserEntity entity)
        {
            int res = 0;
            string query = $"INSERT INTO {_tableName} (firstName, lastName, email, accountStatus, urlApproveProfile) VALUES (@firstName, @lastName, @email, @accountStatus, @urlApproveProfile)";

            MySqlCommand command = new MySqlCommand(query, _conn);
            command.Parameters.AddWithValue("@tableName", _tableName);
            command.Parameters.AddWithValue("@firstName", entity.FirstName);
            command.Parameters.AddWithValue("@lastName", entity.LastName);
            command.Parameters.AddWithValue("@email", entity.Email);
            command.Parameters.AddWithValue("@accountStatus", entity.AccountStatus);
            command.Parameters.AddWithValue("@urlApproveProfile", entity.UrlApproveProfile);

            try
            {
                _conn.Open();
                res = command.ExecuteNonQuery();
                Console.WriteLine("Insert réussi !");
            }
            catch (Exception ex)
            {
                res = 0;
                Console.WriteLine("Erreur lors de l'insert : " + ex.Message);
            }
            finally
            {
                _conn.Close();
            }

            return res;
        }

        public UserEntity? GetOne(string email)
        {
            UserEntity result = null;
            string query = $"SELECT * FROM {_tableName} WHERE email=@email";

            MySqlCommand command = new MySqlCommand(query, _conn);
            command.Parameters.AddWithValue("@tableName", _tableName);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                _conn.Open();

                using MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                result = new UserEntity
                {
                    FirstName = reader.GetString(0),
                    LastName = reader.GetString(1),
                    Email = reader.GetString(2),
                    AccountStatus = reader.GetBoolean(3),
                    UrlApproveProfile = reader.GetString(4),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de connexion avec la conncetion string {_connectionString} : " + ex.Message);
            }
            finally
            {
                _conn.Close();
            }

            return result;

        }


        public void CloseConnection()
        {
            _conn.Close();
        }
    }
}
