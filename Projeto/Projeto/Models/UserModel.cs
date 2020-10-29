using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;

namespace Projeto.Models
{
    public class UserModel : IDisposable
    {
        private MySqlConnection connection;

        public UserModel()
        {
            string strConn = "SERVER=localhost;DATABASE=mvc_travel;User id=root;Password=;";
            connection = new MySqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public void Create(User user)
        {

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO user VALUES (@name, @email, @password)";

            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);

            cmd.ExecuteNonQuery();
        }

        public int Login(User user)
        {
            int resposta = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM user WHERE name=@name and password=@password";

            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@password", user.Password);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                user.Id = (int)reader["Id"];
                user.Name = (string)reader["name"];
                user.Email = (string)reader["email"];
                user.Password = (string)reader["password"];
                resposta = (int)reader["Id"];
            }

            connection.Close();
            return resposta;
        }

        public User Search(int id)
        {
            connection.Open();
            User user1 = new User();

            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.Connection = connection;
            cmd1.CommandText = @"SELECT * FROM user WHERE id=@id";

            cmd1.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                user1.Id = (int)reader1["Id"];
                user1.Name = (string)reader1["name"];
                user1.Email = (string)reader1["email"];
                user1.Password = (string)reader1["password"];
            }

            return user1;
        }

        public List<User> Read()
        {
            List<User> lista = new List<User>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM user";

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

        public void Update(User user, int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE user SET name=@nome, email=@email WHERE Id=@id";

            cmd.Parameters.AddWithValue("@nome", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM user WHERE Id=@id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
