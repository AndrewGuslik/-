using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Зоомагаз
{
    internal class Database
    {
        string nameCl;
        NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres; Password=11111; Database=Zoomagaz");

        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed) 
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public NpgsqlConnection getConnection()
        {
            return connection;
        }

        public List<Categories> addCateg()
        {
            List<Categories> categs = new List<Categories>();

            openConnection();

            NpgsqlCommand getCat = new NpgsqlCommand("SELECT animal FROM goods ORDER BY id ASC", getConnection());
            NpgsqlDataReader reader = getCat.ExecuteReader();

            while (reader.Read()) 
            {
                Categories categories = new Categories(reader.GetString(0));
                categs.Add(categories);
            }

            closeConnection();

            return categs;
        }
    }
}
