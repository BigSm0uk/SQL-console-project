using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Console_sqlTool_projects
{
     public class SQL_Tools
    {
        private SqlDataReader dataReader = null;
        private SqlCommand sqlcommand = null;
        public void Select(string command , SqlConnection connection)
        {
            sqlcommand = new SqlCommand(command, connection);
            dataReader = sqlcommand.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader["Id"]} {dataReader["FIO"]} {dataReader["Birthday"]} {dataReader["University"]} " +
                    $"{dataReader["Group_number"]} {dataReader["Course"]} {dataReader["AVG_score"]}");
                Console.WriteLine(new string('-', 60));
            }
            if (dataReader != null)
            {
                dataReader.Close();
            }
        }
        public void Update(string command, SqlConnection connection)
        {
            sqlcommand = new SqlCommand(command, connection);
            Console.WriteLine($"Изменено: {sqlcommand.ExecuteNonQuery()} строк(а)");
        }
        public void Delete(string command, SqlConnection connection)
        {
            sqlcommand = new SqlCommand(command, connection);
            Console.WriteLine($"Удалено: {sqlcommand.ExecuteNonQuery()} строк(а)");
        }
        public void Insert(string command, SqlConnection connection)
        {
            sqlcommand = new SqlCommand(command, connection);
            Console.WriteLine($"Добавлено: {sqlcommand.ExecuteNonQuery()} строк(а)");
        }
        public void SortBy(string content , string mode, SqlConnection connection)
        {
            sqlcommand = new SqlCommand($"Select * from students order by {content} {mode}", connection);
            dataReader = sqlcommand.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader["Id"]} {dataReader["FIO"]} {dataReader["Birthday"]} {dataReader["University"]} " +
                    $"{dataReader["Group_number"]} {dataReader["Course"]} {dataReader["AVG_score"]}");
                Console.WriteLine(new string('-', 60));
            }
            if (dataReader != null)
            {
                dataReader.Close();
            }
        }
        public void SelectAll(SqlConnection connection)
        {
            sqlcommand = new SqlCommand($"Select * from students", connection);
            dataReader = sqlcommand.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader["Id"]} {dataReader["FIO"]} {dataReader["Birthday"]} {dataReader["University"]} " +
                    $"{dataReader["Group_number"]} {dataReader["Course"]} {dataReader["AVG_score"]}");
                Console.WriteLine(new string('-', 60));
            }
            if (dataReader != null)
            {
                dataReader.Close();
            }
        }
        public void Search(string column, string content, SqlConnection connection)
        {
            if (column.Equals("fio"))
            {
                sqlcommand = new SqlCommand($"Select * from Students where fio like N'%{content}%'", connection);
            }
            else if (column.Equals("birthday"))
            {
                sqlcommand = new SqlCommand($"Select * from Students where where birthday = '{content}'" ,connection);
            }
            else ErrorShow($"Аргумент {content} не корректен!");
            try
            {
                dataReader = sqlcommand.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine($"{dataReader["Id"]} {dataReader["FIO"]} {dataReader["Birthday"]} {dataReader["University"]} " +
                        $"{dataReader["Group_number"]} {dataReader["Course"]} {dataReader["AVG_score"]}");
                    Console.WriteLine(new string('-', 60));
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
            catch(Exception ex)
            {
                ErrorShow(ex.Message);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
        }
        public void ErrorShow(string ex)
        {
            Console.Error.WriteLine(ex);
        }



    }

}
