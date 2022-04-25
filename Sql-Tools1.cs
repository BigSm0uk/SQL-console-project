using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

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
        public void Min(SqlConnection connection)
        {
            sqlcommand = new SqlCommand($"Select fio, avg_score from students where avg_score =(Select Min(AVG_score) from students)", connection);
            dataReader = sqlcommand.ExecuteReader();
            while (dataReader.Read())
                Console.WriteLine($"Минимальный средний балл: {dataReader["avg_score"]} у {dataReader["FIO"]}");
            if (dataReader != null)
            {
                dataReader.Close();
            }
        }
        public void Max(SqlConnection connection)
        {
            sqlcommand = new SqlCommand($"Select fio, avg_score from students where avg_score = (select max(avg_score) from students)", connection);
            dataReader = sqlcommand.ExecuteReader();
            while (dataReader.Read())
                Console.WriteLine($"Максимальный средний балл: {dataReader["avg_score"]} у {dataReader["FIO"]}");
            if (dataReader != null)
            {
                dataReader.Close();
            }
        }
        public void AVG(SqlConnection connection)
        {
            sqlcommand = new SqlCommand($"Select AVG(AVG_score) from students", connection);
            Console.WriteLine($"Средний балл: {sqlcommand.ExecuteScalar()}");
        }
        public void Sum(SqlConnection connection)
        {
            sqlcommand = new SqlCommand($"Select sum(AVG_score) from students", connection);
            Console.WriteLine($"Cумма средних баллов: {sqlcommand.ExecuteScalar()}");
        }
        public void FSelectAll(SqlConnection connection)
        {
            sqlcommand = new SqlCommand($"Select * from students", connection);
            dataReader = sqlcommand.ExecuteReader();
            string content = string.Empty;
            while (dataReader.Read())
            {
                content += $"{dataReader["Id"]} {dataReader["FIO"]} {dataReader["Birthday"]} {dataReader["University"]} " +
                    $"{dataReader["Group_number"]} {dataReader["Course"]} {dataReader["AVG_score"]}\n";
            }
            using (StreamWriter sw = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}/{"DB_content"}_{DateTime.Now.ToString().Replace(':', '-')}.txt", true, Encoding.UTF8))
            {
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(content);
            }

            if (dataReader != null)
            {
                dataReader.Close();
            }
            SelectAll( connection);
        }
        public void ErrorShow(string ex)
        {
            Console.Error.WriteLine(ex);
        }
    }

}
