using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Projeto.Models
{
    public class UserModel : IDisposable
    {
        private MySqlConnection connection;

        public UserModel()
        {
            string strConn = "SERVER=localhost;DATABASE=MvcTravel;User id=root;Password=;";
            connection = new MySqlConnection(strConn);
        }

        public void Dispose()
        {
            connection.Close();
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        break;

                    case 1045:
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            connection.Close();
            return true;
        }

        public void Create(User user)
        {
            string query = @"INSERT INTO user (name, email, password) VALUES (@name, @email, @password)";

            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public List<User> Read()
        {
            List<User> lista = new List<User>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM User";

            OpenConnection();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                User user = new User();
                user.Id = (int)reader["Id"];
                user.Name = (string)reader["name"];
                user.Email = (string)reader["email"];
                user.Password = (string)reader["password"];

                lista.Add(user);
            }

            return lista;

        }

        public void Update(User user)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE User SET name=@name, Email=@email WHERE Id=@id";

            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@id", user.Id);

            OpenConnection();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM User WHERE Id=@id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
